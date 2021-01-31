using System.Windows;

namespace InventoryManager.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private UIElement _partialElement;

		public UIElement PartialView
		{
			get => _partialElement;
			set
			{
				_partialElement = value;
				OnPropertyChanged("PartialView");
			}
		}
	}
}
