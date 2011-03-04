namespace SharpBag.Network
{
    /// <summary>
    /// Packet-event arguments.
    /// </summary>
    public class PacketEventArgs
    {
        /// <summary>
        /// The connection handler.
        /// </summary>
        public ConnectionHandler Connection { get; private set; }
        /// <summary>
        /// The ID of the event.
        /// </summary>
        public int EventID { get; private set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="connection">The connection handler.</param>
        public PacketEventArgs(ConnectionPacket packet, ConnectionHandler connection)
        {
            this.EventID = packet.EventID;
            this.Connection = connection;
        }
    }
}