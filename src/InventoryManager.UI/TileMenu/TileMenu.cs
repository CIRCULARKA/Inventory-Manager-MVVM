using System.Windows;
using System.Windows.Media;

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

		public static readonly DependencyProperty TitleForegroundProperty =
			DependencyProperty.Register(
				"TitleForeground",
				typeof(Brush),
				typeof(TileMenu),
				new FrameworkPropertyMetadata(Brushes.Black)
			);

		public static readonly DependencyProperty MenuBackgroundProperty =
			DependencyProperty.Register(
				"MenuBackground",
				typeof(Brush),
				typeof(TileMenu),
				new FrameworkPropertyMetadata(Brushes.White)
			);

		public TileMenu() =>
			InitializeComponent();


		public string Title
		{
			get => GetValue(TitleProperty) as string;
			set => SetValue(TitleProperty, value);
		}

		public Brush TitleForeground
		{
			get => GetValue(TitleForegroundProperty) as Brush;
			set => SetValue(TitleForegroundProperty, value);
		}

		public Brush MenuBackground
		{
			get => GetValue(MenuBackgroundProperty) as Brush;
			set => SetValue(MenuBackgroundProperty, value);
		}
	}
}
