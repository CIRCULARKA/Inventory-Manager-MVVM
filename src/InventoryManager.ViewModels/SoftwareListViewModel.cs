using InventoryManager.Models;
using InventoryManager.Views;
using InventoryManager.Events;
using InventoryManager.Commands;
using InventoryManager.Extensions;
using System.Linq;
using System.Collections.ObjectModel;

namespace InventoryManager.ViewModels
{
	public class SoftwareListViewModel : ViewModelBase, ISoftwareListViewModel
	{
		private ObservableCollection<Software> _allDeviceSoftware;

		public SoftwareListViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			ShowAddSoftwareViewCommand = RegisterCommandAction(
				(obj) =>
				{
					var _addSoftwareView = new AddSoftwareView();
					_addSoftwareView.DataContext = ResolveDependency<IAddSoftwareViewModel>()
						as AddSoftwareViewModel;
					_addSoftwareView.ShowDialog();

				},
				(obj) => SelectedDevice != null
			);

			ShowEditSoftwareViewCommand = RegisterCommandAction(
				(obj) =>
				{
					var _editSoftwareView = new EditSoftwareInfoView();
					_editSoftwareView.DataContext = ResolveDependency<IEditSoftwareInfoViewModel>()
						as EditSoftwareInfoViewModel;
					_editSoftwareView.ShowDialog();
				},
				(obj) => SelectedDevice != null
			);

			RemoveSoftwareCommand = RegisterCommandAction(
				(obj) =>
				{
					Repository.RemoveSoftware(SelectedSoftware);
					Repository.SaveChanges();
					SelectedDeviceSoftware.Remove(SelectedSoftware);
				},
				(obj) => SelectedSoftware != null
			);

			DeviceEvents.OnDeviceSelectionChanged += (device) =>
			{
				if (device != null)
				{
					SelectedDeviceSoftware = Repository.GetAllDeviceSoftware(
						SelectedDevice
					).ToObservableCollection();
				}
			};

			DeviceEvents.OnSoftwareAdded += (software) =>
				SelectedDeviceSoftware.Add(software);
		}

		private IDeviceRelatedRepository Repository { get; }

		public Device SelectedDevice =>
			(ResolveDependency<IDevicesListViewModel>() as DevicesListViewModel).
				SelectedDevice;

		public ObservableCollection<Software> SelectedDeviceSoftware
		{
			get => _allDeviceSoftware;
			set
			{
				_allDeviceSoftware = value;
				OnPropertyChanged(nameof(SelectedDeviceSoftware));
			}
		}

		public Software SelectedSoftware { get; set; }

		public Command ShowAddSoftwareViewCommand { get; }

		public Command ShowEditSoftwareViewCommand { get; }

		public Command RemoveSoftwareCommand { get; }
	}
}
