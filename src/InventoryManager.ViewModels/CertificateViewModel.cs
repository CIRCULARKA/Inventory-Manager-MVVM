using InventoryManager.Commands;
using InventoryManager.Views;

namespace InventoryManager.ViewModels
{
	public class CertificateViewModel : ViewModelBase
	{
		public CertificateViewModel()
		{
			ShowAddCertificateViewCommand = new ButtonCommand(
				(o) =>
				{
					var addCertificateView = new AddCertificateView();
					addCertificateView.DataContext = this;
					addCertificateView.ShowDialog();
				}
			);
		}

		public ButtonCommand ShowAddCertificateViewCommand { get; }
	}
}
