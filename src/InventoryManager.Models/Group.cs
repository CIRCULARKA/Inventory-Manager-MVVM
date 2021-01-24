using System.Collections.Generic;

namespace InventoryManager.Models
{
	public class Group : NotifyingModel
	{
		private int _id;

		private string _name;

		public int ID
		{
			get => _id;
			set
			{
				_id = value;
				base.OnPropertyChanged("ID");
			}
		}

		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				base.OnPropertyChanged("Name");
			}
		}

		public List<User> Users;
	}
}
