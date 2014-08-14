using System;
using System.Collections.Generic;
using System.Text;
using iAnywhere.RfidNet.Rfid.Multiprotocol;
using iAnywhere.RfidNet.Rfid;
using BModule;

namespace RFIDView
{
    public delegate void AddEventHandler(object sender, object data);
    
    public class DataManager<T> : Dictionary<long, T>
    {
        private AddEventHandler addEvent;
        
        public DataManager(IDictionary<long, T> list) : base()
        {
            foreach (KeyValuePair<long, T> kvp in list)
            {
                this.Add(kvp.Value);
            }
        }

        public DataManager() : base()
        {
        }

        public void Add(T item)
        {
            this.Add(item.GetHashCode(), item);
        }

        public void Update(T oldItem, T newItem)
        {
            if (this.ContainsKey(oldItem.GetHashCode()))
                this[oldItem.GetHashCode()] = newItem;
            else throw new Exception(string.Format("Cannot update.\n{0} was not found in datasource.", oldItem));
        }

        public new void Add(long key, T value)
        {
            if(this.AddEvent != null)
                this.AddEvent(this, value);
            else if (this.ContainsKey(key))
            {
                this[key] = value;
            }
            else
            {
                base.Add(key, value);
            }
        }

        public AddEventHandler AddEvent
        {
            get { return this.addEvent; }
            set { this.addEvent = value; }
        }
    }


    //public class RFIDEventData : EventInfo
    //{
    //    private int read_count;

    //    public RFIDEventData(RfidMPEventArgs arg) : base(arg)
    //    {
    //        this.read_count = 1;
    //    }

    //    public RFIDEventData() 
    //    {
    //        this.read_count = 1;
    //    }


    //    [DataGridProperty(PropertyState.Viewable)]
    //    public DateTime LastSeen
    //    {
    //        get { return LastSeenDate[read_count - 1]; }
    //    }


    //    [DataGridProperty( PropertyState.Viewable)]
    //    public int Reads
    //    {
    //        get { return this.read_count; }
    //        set { this.read_count = value; }
    //    }


    //    public static void AddHandler(object sender, object eventData)
    //    {
    //        Dictionary<long, RFIDEventData> hash = sender as Dictionary<long, RFIDEventData>;
    //        RFIDEventData data = eventData as RFIDEventData;
    //        if (hash != null && data != null)
    //        {
    //            if (hash.ContainsKey(data.GetHashCode()))
    //            {
    //                hash[data.GetHashCode()].Reads++;
    //                hash[data.GetHashCode()].LastSeenDate.Add(data.LastSeen);
    //            }
    //            else
    //            {
    //                hash.Add(eventData.GetHashCode(), data);
    //            }
    //        }
    //    }
    //}
}
