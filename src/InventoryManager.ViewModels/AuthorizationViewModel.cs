using InventoryManager.Commands;
using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Data;

namespace InventoryManager.ViewModels
{
	public class AuthorizationViewModel : NotifyingModel
	{
		private string _messageToUser;

		private ButtonCommand _loginCommand;

		public AuthorizationViewModel(InventoryManagerDbContext data, ViewBase view)
		{
			Data = data;
			AuthorizationView = view;
		}

		private InventoryManagerDbContext Data { get; }

		private ViewBase AuthorizationView { get; }

		private ButtonCommand LoginCommand
		{
			get
			{
				return _loginCommand ??
					new ButtonCommand(
						(user) =>
						{
							var findedUser = Data.Users.Find(InputtedLogin, InputtedPassword);
							if (findedUser != null && findedUser.Password == InputtedPassword)
							{
								var userView = new UserView();
								userView.Show();
								AuthorizationView.Close();
							}
							else MessageToUser = "Логин или пароль введён неверно";
						}
					);
			}
		}

		public string InputtedLogin { get; set; }


		public string InputtedPassword { get; set; }

		public string MessageToUser
		{
			get => _messageToUser;
			set
			{
				_messageToUser = value;
				OnPropertyChanged("MessageToUser");
			}
		}
	}
}
