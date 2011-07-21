using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Networking.Services
{
	public class ServerNameService : INetworkServerService, IEnumerable<KeyValuePair<int, string>>
	{
		public event Action<string, int> OnNameAnnounced;

		public event Action<string, string, int> OnNameChanged;

		public event Action<string, int> OnNameLeft;

		private Dictionary<int, string> Names;

		private string _Name;

		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				string old = this._Name;
				this._Name = value;
				this.ChangeName(old, value, 0);
			}
		}

		public ServerNameService()
		{
			this.Names = new Dictionary<int, string>();
		}

		public string this[int id]
		{
			get
			{
				string name = null;
				this.Names.TryGetValue(id, out name);
				return name;
			}
		}

		public override void Open()
		{
			this.Server.OnConnectionConnected += new Action<NetworkServer, TcpNetworkConnection>(Server_OnConnectionConnected);
			this.Server.OnConnectionDisconnected += new Action<NetworkServer, TcpNetworkConnection>(Server_OnConnectionDisconnected);
		}

		private void Server_OnConnectionConnected(NetworkServer arg1, TcpNetworkConnection arg2)
		{
			if (this.Names.Count == 0) return;
			NetworkPacket packet = new NetworkPacket { Targets = new int[] { arg2.ID } };

			packet.DataWriter.Write(this.Names.Count);

			foreach (var name in this.Names)
			{
				packet.DataWriter.Write(name.Key);
				packet.DataWriter.Write(name.Value);
			}

			this.Send(packet);
		}

		private void Server_OnConnectionDisconnected(NetworkServer arg1, TcpNetworkConnection arg2)
		{
			string name;
			if (this.Names.TryGetValue(arg2.ID, out name))
			{
				this.ChangeName(name, null, arg2.ID);
			}
		}

		public override void Receive(NetworkPacket packet)
		{
			string oldName = null, newName = packet.DataReader.ReadString();
			this.Names.TryGetValue(packet.Sender, out oldName);
			this.ChangeName(oldName, newName, packet.Sender);
		}

		private void ChangeName(string oldName, string newName, int id)
		{
			if (newName == oldName) return;

			if (newName == null)
			{
				this.Names.Remove(id);
				this.SendAll("", -id);
				if (this.OnNameLeft != null) this.OnNameLeft(oldName, id);
			}
			else if (oldName == null)
			{
				this.Names.Add(id, newName);
				this.SendAll(newName, id);
				if (this.OnNameAnnounced != null) this.OnNameAnnounced(newName, id);
			}
			else
			{
				this.Names[id] = newName;
				this.SendAll(newName, id);
				if (this.OnNameChanged != null) this.OnNameChanged(oldName, newName, id);
			}
		}

		private void SendAll(string name, int id)
		{
			NetworkPacket packet = new NetworkPacket();
			packet.AllButTargets = true;

			packet.DataWriter.Write(1);
			packet.DataWriter.Write(id);
			packet.DataWriter.Write(name);

			this.Send(packet);
		}

		public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
		{
			foreach (var item in this.Names)
			{
				yield return item;
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}