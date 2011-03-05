using System;
using System.Linq;
using System.Runtime.Serialization;

namespace SharpBag.Networking
{
    /// <summary>
    /// A packet.
    /// </summary>
    [Serializable]
    public class ConnectionPacket
    {
        /// <summary>
        /// The ID of an event.
        /// </summary>
        public int EventID { get; set; }
        private object _Data;
        /// <summary>
        /// The data.
        /// </summary>
        public object Data
        {
            get { return this._Data; }
            set
            {
                if (!value.GetType().GetCustomAttributes(typeof(SerializableAttribute), true).Any()) throw new SerializationException("Object is not Serializable");
                this._Data = value;
            }
        }
    }
}