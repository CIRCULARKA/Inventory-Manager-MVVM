using System.Windows;
using System.Windows.Controls;

namespace InventoryManager.UI
{
	public partial class TileMenu : TileMenuElement
	{
		public static readonly DependencyProperty TitleProperty =
			DependencyProperty.Register(
				"Title",
				typeof(string),
				typeof(TileMenu),
				new FrameworkPropertyMetadata("<Title>")
			);

		public TileMenu() =>
			InitializeComponent();


		public string Title
		{
			get => GetValue(TitleProperty) as string;
			set => SetValue(TitleProperty, value);
		}
	}
}
