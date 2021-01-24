namespace InventoryManager.Models
{
	public class User : NotifyingModel
	{
		private int _id;

		private string _lastName;

		private string _firstName;

		private string _middleName;

		private string _login;

		private string _password;

		private Group _userGroup;

		public int ID
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
				OnPropertyChanged("ID");
			}
		}

		public string LastName
		{
			get
			{
				return _lastName;
			}
			set
			{
				_lastName = value;
				OnPropertyChanged("LastName");
			}
		}

		public string FirstName
		{
			get
			{
				return _firstName;
			}
			set
			{
				_firstName = value;
				OnPropertyChanged("FirstName");
			}
		}

		public string MiddleName
		{
			get
			{
				return _middleName;
			}
			set
			{
				_middleName = value;
				OnPropertyChanged("MiddleName");
			}
		}

		public string Login
		{
			get
			{
				return _login;
			}
			set
			{
				_login = value;
				OnPropertyChanged("Login");
			}
		}

		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
				OnPropertyChanged("Password");
			}
		}

		public int UserGroupID { get; set; }

		public Group UserGroup
		{
			get
			{
				return _userGroup;
			}
			set
			{
				_userGroup = value;
				OnPropertyChanged("UserGroup");
			}
		}
	}
}
