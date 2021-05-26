using InventoryManager.Models;
using InventoryManager.Reports;
using InventoryManager.Commands;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class ReportsMasterViewModel : ViewModelBase, IReportsMasterViewModel
	{
		private string _selectedReportType;

		public ReportsMasterViewModel()
		{
			ReportTypes = new List<string> {
				"Отчёт об устройствах",
				"Отчёт о пользователях",
				"Отчёт о сертификатах"
			};

			SelectedReportType = ReportTypes[0];

			MakeReportCommand = RegisterCommandAction(
				(obj) =>
				{
					if (SelectedReportType == ReportTypes[0])
					{
						var allDevices = (ResolveDependency<IDevicesListViewModel>() as DevicesListViewModel).
							AllDevices;

						var report = new PdfReporter<Device>(
							allDevices,
							new PropertyDisplayInfo[] {
								new PropertyDisplayInfo("InventoryNumber", "Инвентарный номер"),
								new PropertyDisplayInfo("NetworkName", "Сетевое имя"),
								new PropertyDisplayInfo("DeviceType", "Тип")
							},
							"Устройства в распоряжении"
						);

						report.GenerateReport("reportDevices.pdf");

						MessageToUser = "Отчёт сгенерирован";
					}
					else if (SelectedReportType == ReportTypes[1])
					{
						var allUsers = (ResolveDependency<IUserViewModel>() as UserViewModel).
							UsersToShow;

						var report = new PdfReporter<User>(
							allUsers,
							new PropertyDisplayInfo[] {
								new PropertyDisplayInfo("FullName", "ФИО"),
								new PropertyDisplayInfo("Login", "Логин"),
								new PropertyDisplayInfo("Password", "Пароль"),
								new PropertyDisplayInfo("UserGroup", "Группа")
							},
							"Все пользователи"
						);

						report.GenerateReport("reportUsers.pdf");

						MessageToUser = "Отчёт сгенерирован";
					}
					else if (SelectedReportType == ReportTypes[2])
					{
						var allCertifiacates = (ResolveDependency<ICertificateViewModel>() as CertificateViewModel).
							CertificatesToShow;

						var report = new PdfReporter<Certificate>(
							allCertifiacates,
							new PropertyDisplayInfo[] {
								new PropertyDisplayInfo("Name", "Наименование"),
								new PropertyDisplayInfo("SerialNumber", "Серийный номер"),
								new PropertyDisplayInfo("ExpirationTime", "Конец срока"),
							},
							"Электронные сертификаты"
						);

						report.GenerateReport("reportCertifiactes.pdf");

						MessageToUser = "Отчёт сгенерирован";
					}
				}
			);
		}

		public List<string> ReportTypes { get; }

		public string SelectedReportType
		{
			get => _selectedReportType;
			set
			{
				_selectedReportType = value;
				OnPropertyChanged(nameof(SelectedReportType));
			}
		}

		public string Path { get; set; }

		public Command MakeReportCommand { get; }
	}
}
