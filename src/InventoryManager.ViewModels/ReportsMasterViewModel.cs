using InventoryManager.Models;
using InventoryManager.Reports;
using InventoryManager.Commands;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class ReportsMasterViewModel : ViewModelBase, IReportsMasterViewModel
	{
		public ReportsMasterViewModel()
		{
			ReportTypes = new List<string> {
				"Отчёт об устройствах",
				"Отчёт о пользователях",
				"Отчёт о сертификатах"
			};

			MakeReportCommand = RegisterCommandAction(
				(obj) =>
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

					report.GenerateReport("report.pdf");
				}
			);
		}

		public List<string> ReportTypes { get; }

		public string SelectedReportType { get; set; }

		public string Path { get; set; }

		public Command MakeReportCommand { get; }
	}
}
