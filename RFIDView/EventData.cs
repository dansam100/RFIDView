using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections;

namespace RFIDView
{
    public enum FilterType
    {
        None,
        Equals,
        WildCard
    }


    public interface IBindingView
    {
        Type UnderlyingType { get; }
    }

    public interface IFilterSpec
    {
        string Subject { get; }
        string Value { get; }
        FilterType FilterType { get; }
    }
    
    
    public class BindingListView<T> : BindingList<T>, IBindingListView, IBindingView
    {
        string filter = string.Empty;
        FilterSpecCollection filterCollection;
        DataManager<T> manager;
        List<T> data = null;

        public BindingListView()
            : base()
        {
            this.data = new List<T>();
            manager = new DataManager<T>();
            this.filterCollection = new FilterSpecCollection();
        }

        public BindingListView(IList<T> data)
            : base(data)
        {
            this.data = new List<T>();
            this.manager = new DataManager<T>();
            this.filterCollection = new FilterSpecCollection();
            foreach (T obj in data)
            {
                data.Add(obj);
                manager.Add(obj);
            }
        }


        #region Semi-Overrides
        public new void Add(T value)
        {
            lock (this)
            {
                this.data.Add(value);
                this.manager.Add(value);
                
                //FilterSpec spec = new FilterSpec(filter);

                //if(Belongs(value, spec))
                //{
                //    int index = this.Contains(value);
                //    if (index >= 0)
                //    {
                //        base[index] = value;
                //    }
                //    else
                //    {
                //        base.Add(value);
                //    }
                //}

                this.Refresh();
            }
        }


        public void Update(T oldData, T newData)
        {
            lock (this)
            {
                if (data.Contains(oldData))
                {
                    int index = data.IndexOf(oldData);
                    if (data.Remove(oldData))
                        data.Insert(index, newData);
                    else throw new Exception(string.Format("Cannot update.\n{0} was not found in datasource.", oldData));
                }
                manager.Update(oldData, newData);
            }
            this.Refresh();
        }

        public new void Clear()
        {
            base.Clear();
            this.data.Clear();
            this.manager.Clear();
        }


        public new void Remove(T item)
        {
            if (item != null)
            {
                base.Remove(item);
                this.data.Remove(item);
                this.manager.Remove(item.GetHashCode());
            }
        }
        
        public Type UnderlyingType
        {
            get { return typeof(T); }
        }


        private new int Contains(T value)
        {
            for(int i = 0; i < this.Count; i++)
            {
                if (value.Equals(this[i]))
                    return i;
            }
            return -1;
        }
        #endregion


        #region Filtering...
        public void RemoveFilter()
        {
            filter = string.Empty;
            this.ApplyFilter();
        }


        private void ApplyFilter()
        {
            if (string.IsNullOrEmpty(this.filter))
            {
                return;
            }
            FilterSpec spec = new FilterSpec(filter);

            if (string.IsNullOrEmpty(spec.Subject) || string.IsNullOrEmpty(spec.Value) ||
                spec.FilterType == FilterType.None)
            {                
                if(this.Count == this.manager.Count)
                    return;
            }
            this.Refresh();
        }


        private void Refresh()
        {

            FilterSpec fSpec = new FilterSpec(filter);
            //count = this.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    PropertyInfo prop = typeof(T).GetProperty(spec.Subject);
            //    while (i < count && Belongs(this[i], spec))
            //    {
            //        //bad
            //        this.Remove(this[i]);
            //        count = this.Count;
            //    }
            //}
            //for (int i = 0; i < this.data.Count; i++)
            //{
            //    if (!this.Belongs(this.data[i], spec))
            //        base.Add(this.data[i]);
            //}
            lock (this)
            {
                List<T> filteredItems = new List<T>();
                try
                {
                    FilterSpecCollection specs = new FilterSpecCollection(this.filterCollection);
                    specs.Add(fSpec);
                    int counter = 0;
                    foreach (FilterSpec spec in specs)
                    {
                        if (counter == 0)
                        {
                            foreach (KeyValuePair<long, T> kvp in this.manager)
                            {
                                if (this.Belongs(kvp.Value, spec))
                                    filteredItems.Add(kvp.Value);
                            }
                        }
                        else
                        {
                            for(int i = 0; i < filteredItems.Count; i++)
                            {
                                if (!this.Belongs(filteredItems[i], spec))
                                {
                                    filteredItems.RemoveAt(i--);
                                }
                            }
                        }
                        counter++;
                    }

                    this.ClearItems();
                    filteredItems.ForEach(delegate(T item)
                        {
                            base.Add(item);
                        });
                }
                catch (Exception)
                {
                    throw;
                }                
            }
        }


        /// <summary>
        /// Ensures that a parameter given matches the filter description
        /// </summary>
        /// <param name="obj">the parameter to check</param>
        /// <param name="spec">the filter descriptions</param>
        /// <returns>true if parameter matches filter description</returns>
        private bool Belongs(T obj, FilterSpec spec)
        {
            if (spec.FilterType == FilterType.Equals)
            {
                PropertyInfo prop = typeof(T).GetProperty(spec.Subject);
                object value = prop.GetValue(obj, null);
                if (value == null) value = string.Empty;
                if (prop != null && value != null)
                {
                    //bad
                    return (string.Compare(value.ToString(), spec.Value, true) == 0);
                }
            }
            else if (spec.FilterType == FilterType.WildCard)
            {
                PropertyInfo prop = typeof(T).GetProperty(spec.Subject);
                object value = prop.GetValue(obj, null);
                if (value is DateTime)
                {
                    string time = spec.Value.Replace("%", "");
                    try
                    {
                        DateTime dt = new DateTime();
                        DateTime dtValue = (DateTime)value;
                        if (DateTime.TryParse(time, out dt))
                        {
                            if (time.Contains("/"))
                            {
                                return (dt.Date.CompareTo(dtValue) == 0);
                            }
                            else
                            {
                                return ((dt.Hour % 12) == (dtValue.Hour % 12) && dt.Minute == dtValue.Minute);
                            }
                        }
                    }
                    catch { }
                }
                else
                {
                    string sfilter = spec.Value.Replace("%", ".*");
                    Regex regex = new Regex(sfilter);

                    if (value == null) value = string.Empty;
                    if (prop != null)
                    {
                        //bad
                        return regex.IsMatch(prop.GetValue(obj, null).ToString());
                    }
                }
            }
            return true;
        }
        #endregion

        #region Sorting...
        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public ListSortDescriptionCollection SortDescriptions
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public bool SupportsAdvancedSorting
        {
            get { return false; }
        }

        public bool SupportsFiltering
        {
            get { return true; }
        }
        #endregion

        #region Properties...
        public DataManager<T> Manager
        {
            get { return this.manager; }
        }

        public string Filter
        {
            get{ return this.filter; }
            set
            {
                this.filter = value;
                this.ApplyFilter();
            }
        }

        public FilterSpecCollection FilterCollection
        {
            get { return this.filterCollection; }
            set { this.filterCollection = value; }
        }
        #endregion
    }


    #region Filter Specification Class

    public class FilterSpecCollection : List<FilterSpec>
    {
        public FilterSpecCollection(){}
        public FilterSpecCollection(FilterSpecCollection filterlist)
            : base(filterlist)
        {
        }
    }

    /// <summary>
    /// Filter specification class
    /// </summary>
    public class FilterSpec : IFilterSpec
    {
        private FilterType filtertype;
        private string subject;
        private string value;

        public FilterSpec(string filter)
        {
            this.filtertype = FilterType.None;
            this.TryParse(filter);
        }

        private void TryParse(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return;
            }
            filter = filter.Trim();
            try
            {
                string[] values = filter.Split('@');
                if (values.Length > 1 && filter.Length > 1)
                {
                    this.subject = values[0].Trim();
                    bool equals = values[1].Trim()[0] == '=';

                    bool iswildcard = filter.Contains("%") | filter.Contains("*");
                    if ( (equals && values[1].Length > 1) || !iswildcard)
                    {
                        this.filtertype = FilterType.Equals;
                        filter = (equals) ? values[1].Substring(1).Trim() : values[1].Trim();
                        this.value = filter;
                    }
                    else
                    {
                        this.filtertype = FilterType.WildCard;
                        this.value = values[1].Trim();
                    }
                }
            }
            catch { }
        }

        public FilterType FilterType
        {
            get { return this.filtertype; }
        }

        public String Subject
        {
            get { return this.subject; }
        }


        public String Value
        {
            get { return this.value; }
        }
    }
    #endregion
}
