using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using static SodaCL.Toolkits.DataTool;
using static SodaCL.Toolkits.GetResources;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Controls {

	/// <summary>
	/// SvgButton.xaml 的交互逻辑
	/// </summary>
	public partial class SodaIconButton : UserControl {
		private CubicEase ce = new() { EasingMode = EasingMode.EaseOut };
		private bool isMouseDown;
		private static SodaIconButton btn;

		public SodaIconButton() {
			InitializeComponent();
			btn = this;
			IconBtn_Border.Background = Brushes.Transparent;
		}

		#region 依赖属性

		public double IconWidth {
			get { return (double)GetValue(IconWidthProperty); }
			set { SetValue(IconWidthProperty, value); }
		}

		public static readonly DependencyProperty IconWidthProperty =
			DependencyProperty.Register("IconWidth", typeof(double), typeof(SodaIconButton), new PropertyMetadata(new PropertyChangedCallback((sender, e) => {
				if (btn != null) {
					btn.IconBtn_Img.Width = (double)e.NewValue;
				}
			})));

		public double IconHeight {
			get { return (double)GetValue(IconHeightProperty); }
			set { SetValue(IconHeightProperty, value); }
		}

		public static readonly DependencyProperty IconHeightProperty =
			DependencyProperty.Register("IconHeight", typeof(double), typeof(SodaIconButton), new PropertyMetadata(new PropertyChangedCallback((sender, e) => {
				if (btn != null) {
					btn.IconBtn_Img.Height = (double)e.NewValue;
				}
			})));

		public ImageSource IconSrc {
			get { return (ImageSource)GetValue(IconSrcProperty); }
			set { SetValue(IconSrcProperty, value); }
		}

		public static readonly DependencyProperty IconSrcProperty =
			DependencyProperty.Register("IconSrc", typeof(ImageSource), typeof(SodaIconButton), new PropertyMetadata(new PropertyChangedCallback((sender, e) => {
				if (btn != null) {
					btn.IconBtn_Img.Source
					= (ImageSource)e.NewValue;
				}
			})));

		#endregion 依赖属性

		#region 事件

		public event RoutedEventHandler Click;

		private void IconBtn_ChangeColor(object sender = null, MouseEventArgs e = null) {
			if (IsMouseOver) {
				var sb = new Storyboard();
				var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Normal_Hover")), new Duration(TimeSpan.FromSeconds(0.2)));
				ca.EasingFunction = new CubicEase {
					EasingMode = EasingMode.EaseOut
				};
				Storyboard.SetTarget(ca, IconBtn_Border);
				Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
				sb.Children.Add(ca);
				sb.Begin();
			}
			else {
				var sb = new Storyboard();
				var ca = new ColorAnimation(Colors.Transparent, new Duration(TimeSpan.FromSeconds(0.2)));
				ca.EasingFunction = new CubicEase {
					EasingMode = EasingMode.EaseOut
				};
				Storyboard.SetTarget(ca, IconBtn_Border);
				Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
				sb.Children.Add(ca);
				sb.Begin();
			}
		}

		private void IconBtn_Border_MouseDown(object sender, MouseButtonEventArgs e) {
			isMouseDown = true;
			var scX = new DoubleAnimation(0.95, TimeSpan.FromSeconds(0.1));
			scX.EasingFunction = ce;
			IconBtn_Border_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);
			var scY = new DoubleAnimation(0.95, TimeSpan.FromSeconds(0.1));
			scY.EasingFunction = ce; IconBtn_Border_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
			var caN = new ColorAnimation(BrushToColor(GetBrush("Brush_Normal_Press")), new Duration(TimeSpan.FromSeconds(0.1)));
			caN.EasingFunction = ce;
			IconBtn_Border.Background.BeginAnimation(SolidColorBrush.ColorProperty, caN);
		}

		private void IconBtn_Border_MouseLeave(object sender, MouseEventArgs e) {
			IconBtn_ChangeColor();
			var scX = new DoubleAnimation(1, TimeSpan.FromSeconds(0.1));
			scX.EasingFunction = ce;
			IconBtn_Border_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);
			var scY = new DoubleAnimation(1, TimeSpan.FromSeconds(0.11));
			scY.EasingFunction = ce;
			IconBtn_Border_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
		}

		private void IconBtn_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			if (isMouseDown) {
				if (Name != null)
					Log(false, ModuleList.Control, LogInfo.Info, $"按下图标按钮 \"{Name}\"");
				Click.Invoke(sender, e);
			}
		}

		private void IconBtn_Border_MouseUp(object sender, MouseButtonEventArgs e) {
			if (isMouseDown) {
				isMouseDown = false;
				var scX = new DoubleAnimation(1, TimeSpan.FromSeconds(0.1));
				scX.EasingFunction = ce;
				IconBtn_Border_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);
				var scY = new DoubleAnimation(1, TimeSpan.FromSeconds(0.1));
				scY.EasingFunction = ce;
				IconBtn_Border_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
			}
		}

		#endregion 事件
	}
}