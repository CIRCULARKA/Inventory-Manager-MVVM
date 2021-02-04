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
					// Just for debugging
					MessageBox.Show(
						SelectedDates.ToString()
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

		public SelectedDatesCollection SelectedDates { get; set; }
	}
}
