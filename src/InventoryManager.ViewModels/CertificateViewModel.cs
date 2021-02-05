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

		private readonly Certificate _certificateModel;

		private ObservableCollection<Certificate> _allCertificates;

		public CertificateViewModel()
		{
			_certificateModel = new Certificate();
			_allCertificates = _certificateModel.All().ToObservableCollection();

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

					_certificateModel.Add(newCertificate);
					_certificateModel.SaveChanges();

					CertificatesToShow.Add(newCertificate);

					InputtedSubject = "";

					MessageToUser = "Сертификат добавлен";
				},
				(o) => !string.IsNullOrWhiteSpace(InputtedSubject)
			);

			RemoveCertificateCommand = new ButtonCommand(
				(o) =>
				{
					_certificateModel.Remove(SelectedCertificate);
					_certificateModel.SaveChanges();
					CertificatesToShow.Remove(SelectedCertificate);
				},
				(o) => SelectedCertificate != null
			);
		}

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

		public Certificate SelectedCertificate { get; set; }

		public ButtonCommand ShowAddCertificateViewCommand { get; }

		public ButtonCommand AddCertificateCommand { get; }

		public ButtonCommand RemoveCertificateCommand { get; }

		public DateTime SelectedValidFromDate { get; set; }

		public DateTime SelectedValidUntilDate { get; set; }
	}
}
