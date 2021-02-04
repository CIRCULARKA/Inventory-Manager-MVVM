using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using InventoryManager.Commands;
using InventoryManager.Views;

namespace InventoryManager.ViewModels
{
	public class CertificateViewModel : ViewModelBase
	{
		private string _inputtedSubject;

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

			AddCertificateCommand = new ButtonCommand(
				(o) =>
				{
					MessageBox.Show(
						SelectedDates[0] + " " + SelectedDates[SelectedDates.Count - 1]
					);
				}
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

		public ButtonCommand ShowAddCertificateViewCommand { get; }

		public ButtonCommand AddCertificateCommand { get; }

		public List<DateTime> SelectedDates { get; set; }
	}
}
