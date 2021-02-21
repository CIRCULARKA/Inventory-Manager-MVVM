using System;
using System.Collections.ObjectModel;
using InventoryManager.Commands;
using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Extensions;
using System.Collections.Generic;

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

			ShowAddCertificateViewCommand = new ButtonCommand(
				(o) =>
				{
					var addCertificateView = new AddCertificateView();
					addCertificateView.DataContext = this;
					addCertificateView.ShowDialog();
				}
			);

			AddCertificateCommand = new ButtonCommand(
				(o) =>
				{
					var newCertificate = new Certificate
					{
						Subject = InputtedSubject,
						ValidFrom = SelectedValidFromDate,
						ValidTo = SelectedValidUntilDate
					};

					Repository.AddCertificate(newCertificate);
					Repository.SaveChanges();

					CertificatesToShow.Add(newCertificate);

					InputtedSubject = "";

					MessageToUser = "Сертификат добавлен";
				},
				(o) => !string.IsNullOrWhiteSpace(InputtedSubject)
			);

			RemoveCertificateCommand = new ButtonCommand(
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

		public ObservableCollection<Certificate> CertificatesToShow => _allCertificates;

		public IEnumerable<Certificate> AllCertificates =>
			Repository.AllCertificates;

		public Certificate SelectedCertificate { get; set; }

		public ButtonCommand ShowAddCertificateViewCommand { get; }

		public ButtonCommand AddCertificateCommand { get; }

		public ButtonCommand RemoveCertificateCommand { get; }

		public DateTime SelectedValidFromDate { get; set; }

		public DateTime SelectedValidUntilDate { get; set; }
	}
}
