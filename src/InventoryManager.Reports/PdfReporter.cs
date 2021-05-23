using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace InventoryManager.Reports
{
	public class PdfReporter<TModel>
	{
		private IEnumerable<TModel> _items;

		private PropertyDisplayInfo[] _propsToDisplay;

		private PropertyInfo[] _genericProps;

		private TablePdfReport _report;

		private string _reportHeader;

		public PdfReporter(IEnumerable<TModel> itemsSource, PropertyDisplayInfo[] propsToDisplay, string reportHeader)
		{
			_reportHeader = reportHeader;

			_report = new TablePdfReport(propsToDisplay, reportHeader);

			_items = itemsSource;
			_propsToDisplay = propsToDisplay;

			_genericProps = typeof(TModel).GetProperties();
		}

		public void GenerateReport(string path)
		{
			FillTable();

			_report.SaveDocument(path);
		}

		private void FillTable()
		{
			var list = _items.ToList();

			foreach (var item in list)
			{
				var rowValues = new List<string>(_propsToDisplay.Length);
				foreach (var prop in _propsToDisplay)
					rowValues.Add(GetValueFromProperty(item, prop));
				_report.AddRow(rowValues);
			}
		}

		private string GetValueFromProperty(TModel entity, PropertyDisplayInfo targetProperty)
		{
			foreach (var prop in _genericProps)
				if (prop.Name == targetProperty.PropertyName)
					return prop.GetValue(entity).ToString();
			throw new Exception("Can't find specified property in type");
		}
	}
}
