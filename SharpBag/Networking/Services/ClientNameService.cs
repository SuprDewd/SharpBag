using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Networking.Services
{
	public class ClientNameService : INetworkClientService, IEnumerable<KeyValuePair<int, string>>
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
				// string old = this._Name;
				this._Name = value;
				// this.ChangeName(old, value, this.Client.ID);
				NetworkPacket packet = new NetworkPacket();
				packet.DataWriter.Write(value);
				this.Send(packet);
			}
		}

		public ClientNameService()
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

		public override void Receive(NetworkPacket packet)
		{
			int count = packet.DataReader.ReadInt32();

			for (int i = 0; i < count; i++)
			{
				int id = packet.DataReader.ReadInt32();
				string newName = packet.DataReader.ReadString(),
					   oldName = null;

				if (id < 0)
				{
					id = -id;
					newName = null;
				}

				this.Names.TryGetValue(id, out oldName);
				this.ChangeName(oldName, newName, id);
			}
		}

		private void ChangeName(string oldName, string newName, int id)
		{
			if (newName == oldName) return;

			if (newName == null)
			{
				this.Names.Remove(id);
				if (this.OnNameLeft != null) this.OnNameLeft(oldName, id);
			}
			else if (oldName == null)
			{
				this.Names.Add(id, newName);
				if (this.OnNameAnnounced != null) this.OnNameAnnounced(newName, id);
			}
			else
			{
				this.Names[id] = newName;
				if (this.OnNameChanged != null) this.OnNameChanged(oldName, newName, id);
			}
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