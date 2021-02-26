using System;
using System.Collections.ObjectModel;
using InventoryManager.Commands;
using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Extensions;

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
				(o) =>
				{
					var addCertificateView = new AddCertificateView();
					addCertificateView.DataContext = this;
					addCertificateView.ShowDialog();
				}
			);

			AddCertificateCommand = RegisterCommandAction(
				(o) =>
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
				(o) => !string.IsNullOrWhiteSpace(InputtedSubject)
			);

			RemoveCertificateCommand = RegisterCommandAction(
				(o) =>
				{
					Repository.RemoveCertificate(SelectedCertificate);
					Repository.SaveChanges();
					CertificatesToShow.Remove(SelectedCertificate);
				},
				(obj) => SelectedCertificate != null
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
