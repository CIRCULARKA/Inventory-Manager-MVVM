using System;
using System.Linq;
using System.Collections.Generic;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;

namespace InventoryManager.Reports
{
	public class TablePdfReport
	{
		private PdfDocumentRenderer _renderer;

		private Document _pdfDocument;

		private Table _dataTable;

		private PropertyDisplayInfo[] _headerCells;

		private string _tableHeader;

		public TablePdfReport(PropertyDisplayInfo[] headerCells, string tableHeader)
		{
			_tableHeader = tableHeader;

			_headerCells = headerCells;

			_pdfDocument = new Document();
			_pdfDocument.AddSection();
			AddTableHeader();
			CreateTable();

			_renderer = new PdfDocumentRenderer(true);
			_renderer.Document = _pdfDocument;
		}

		public void SaveDocument(string path)
		{
			_renderer.RenderDocument();
			_renderer.PdfDocument.Save(path);
		}

		public void AddRow(IEnumerable<string> values)
		{
			var listOfValues = values.ToList();

			if (listOfValues.Count != _headerCells.Length)
				throw new Exception(
					"Number of values should be equal " +
					$"to number of cells in the row ({_headerCells.Length})"
				);

			var row = _dataTable.AddRow();
			for (int i = 0; i < _dataTable.Columns.Count; i++)
				row.Cells[i].AddParagraph(listOfValues[i]);
		}

		private void CreateTable()
		{
			_dataTable = _pdfDocument.Sections[0].AddTable();
			_dataTable.Style = "Table";
			_dataTable.Borders.Color = Colors.Black;
			_dataTable.Borders.Width = 0.25;
			_dataTable.Borders.Left.Width = 0.5;
			_dataTable.Borders.Right.Width = 0.5;
			_dataTable.Rows.Alignment = RowAlignment.Center;

			for (int i = 0; i < _headerCells.Length; i++)
			{
				var column = _dataTable.AddColumn("4cm");
				column.Format.Alignment = ParagraphAlignment.Center;
			}

			var row = _dataTable.AddRow();
			for (int i = 0; i < _dataTable.Columns.Count; i++)
			{
				row.Cells[i].AddParagraph(_headerCells[i].DisplayName);
				row.Cells[i].Format.Font.Bold = true;
			}
		}

		private void AddTableHeader()
		{
			var logo = _pdfDocument.Sections[0].AddImage("Assets/logo.png");
			logo.LockAspectRatio = true;
			logo.Height = "2.5cm";
			logo.Left = ShapePosition.Center;
			logo.WrapFormat.DistanceBottom = 30;

			var header = _pdfDocument.Sections[0].AddParagraph(_tableHeader);
			header.Format.Alignment = ParagraphAlignment.Center;
			header.Format.Font.Bold = true;
			header.Format.Font.Size = 20;
			header.Format.SpaceAfter = 10;
		}
	}
}
