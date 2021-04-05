using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Extensions;
using InventoryManager.Infrastructure;
using System;
using System.Collections.ObjectModel;

namespace InventoryManager.ViewModels
{
	public class CertificateViewModel : ViewModelBase
	{
		private string _inputtedSubject;

		private ObservableCollection<Certificate> _allCertificates;

		public CertificateViewModel(ICertificateRelatedRepository repo)
		{
			Repository = repo;

			_allCertificates = Repository.AllCertificates.ToObservableCollection();

			ShowAddCertificateViewCommand = RegisterCommandAction(
				(obj) =>
				{
					var addCertificateView = new AddCertificateView();
					addCertificateView.DataContext = this;
					addCertificateView.ShowDialog();
				},
				// Can't add new Windows Certificates rn
				(obj) => false &&
					AuthorizedUser.IsAllowedTo(UserActions.AddCertificate)
			);

			AddCertificateCommand = RegisterCommandAction(
				(obj) =>
				{
					var newCertificate = new Certificate
					{
						SerialNumber = InputtedSerialNumber,
						Subject = InputtedSubject,
						Issuer = InputtedIssuer,
						ExpirationDate = SelectedExpirationDate
					};

					Repository.AddCertificate(newCertificate);
					Repository.SaveChanges();

					CertificatesToShow.Add(newCertificate);

					InputtedSubject = "";

					MessageToUser = "Сертификат добавлен";
				},
				// (obj) => !string.IsNullOrWhiteSpace(InputtedSubject)
				// Can't add new Windows Certificate rn
				(obj) => false &&
					AuthorizedUser.IsAllowedTo(UserActions.RemoveCerificate)
			);

			RemoveCertificateCommand = RegisterCommandAction(
				(obj) =>
				{
					Repository.RemoveCertificate(SelectedCertificate);
					Repository.SaveChanges();
					CertificatesToShow.Remove(SelectedCertificate);
				},
				// (obj) => SelectedCertificate != null
				// Same here
				(obj) => false
			);
		}

		private ICertificateRelatedRepository Repository { get; }

		public string InputtedSubject
		{
			get => _inputtedSubject;
			set
			{
				_inputtedSubject = value;
				OnPropertyChanged("InputtedSubject");
			}
		}

		public DateTime SelectedExpirationDate { get; set; }

		public string InputtedSerialNumber { get; set; }

		public string InputtedIssuer { get; set; }

		public ObservableCollection<Certificate> CertificatesToShow => _allCertificates;

		public Certificate SelectedCertificate { get; set; }

		public Command ShowAddCertificateViewCommand { get; }

		public Command AddCertificateCommand { get; }

		public Command RemoveCertificateCommand { get; }
	}
}
