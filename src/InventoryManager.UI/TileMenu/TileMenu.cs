using System.Windows;
using System.Windows.Controls;

namespace InventoryManager.UI
{
	public partial class TileMenu : UserControl
	{
		public static readonly DependencyProperty _titleProperty;

		static TileMenu() =>
			_titleProperty = DependencyProperty.Register(
				"Title",
				typeof(string),
				typeof(TileMenu),
				new FrameworkPropertyMetadata(
					"<Title>",
					FrameworkPropertyMetadataOptions.AffectsRender |
					FrameworkPropertyMetadataOptions.AffectsMeasure
				)
			);

		public TileMenu() =>
			InitializeComponent();


		public string Title
		{
			get => GetValue(_titleProperty) as string;
			set
			{
				SetValue(_titleProperty, value);
				tileTitle.Text = value;
			}
		}
	}
}
