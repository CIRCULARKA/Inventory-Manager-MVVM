using System.Windows.Controls;
using InventoryManager.Models;
using InventoryManager.ViewModels;

namespace InventoryManager.Views
{
	public partial class CertificatesManagementView : UserControl
	{
		public CertificatesManagementView()
		{
			InitializeComponent();

			DataContext = new CertificateViewModel(new DefaultCertificateRelatedRepository());
		}
	}
}
