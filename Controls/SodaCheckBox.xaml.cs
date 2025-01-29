using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SodaCL.Toolkits.Logger;
using static SodaCL.Toolkits.DataTool;
using static SodaCL.Toolkits.GetResources;

namespace SodaCL.Controls
{
	/// <summary>
	/// SodaCheckBox.xaml 的交互逻辑
	/// </summary>

	public partial class SodaCheckBox : UserControl
	{
		private static SodaCheckBox checkBox;
		private CubicEase ce = new() { EasingMode = EasingMode.EaseOut };
		private bool isMouseDown;

		#region 枚举

		public enum ButtonTypes
		{
			Main,
			Normal,
			Warning,
		}

		#endregion 枚举

		#region 事件

		public event RoutedEventHandler Click;

		private void Btn_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (isMouseDown)
			{
				if (IsEnabled)
				{
				}
				Log(false, ModuleList.Control, LogInfo.Info, $"切换复选框 \"{Text}\"");
				Click?.Invoke(sender, e);
			}
		}

		private void Border_Loaded(object sender, RoutedEventArgs e)
		{
			switch (ButtonType)
			{
				case ButtonTypes.Main:
					CheckBox_Border.Background = (SolidColorBrush)GetBrush("Brush_Main");
					break;

				case ButtonTypes.Normal:
					CheckBox_Border.Background = (SolidColorBrush)GetBrush("Brush_Normal");
					// Btn_Txb.Foreground = Brushes.Black;
					break;

				case ButtonTypes.Warning:
					CheckBox_Border.Background = (SolidColorBrush)GetBrush("Brush_Warning");
					break;
			}
		}

		public void Button_ChangeColor(object sender = null, MouseEventArgs e = null)
		{
			switch (this.ButtonType)
			{
				case ButtonTypes.Main:
					if (IsMouseOver)
					{
						var sb = new Storyboard();
						var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Main_Hover")), new Duration(TimeSpan.FromSeconds(0.2)));
						ca.EasingFunction = ce;
						Storyboard.SetTarget(ca, CheckBox_Border);
						Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
						sb.Children.Add(ca);
						sb.Begin();
					}
					else
					{
						var sb = new Storyboard();
						var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Main")), new Duration(TimeSpan.FromSeconds(0.2)));
						ca.EasingFunction = ce;
						Storyboard.SetTarget(ca, CheckBox_Border);
						Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
						sb.Children.Add(ca);
						sb.Begin();
					}
					break;

				case ButtonTypes.Normal:
					if (IsMouseOver)
					{
						var sb = new Storyboard();
						var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Normal_Hover")), new Duration(TimeSpan.FromSeconds(0.2)));
						ca.EasingFunction = ce;
						Storyboard.SetTarget(ca, CheckBox_Border);
						Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
						sb.Children.Add(ca);
						sb.Begin();
					}
					else
					{
						var sb = new Storyboard();
						var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Normal")), new Duration(TimeSpan.FromSeconds(0.2)));
						ca.EasingFunction = ce;
						Storyboard.SetTarget(ca, CheckBox_Border);
						Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
						sb.Children.Add(ca);
						sb.Begin();
					}
					break;

				case ButtonTypes.Warning:
					if (IsMouseOver)
					{
						var sb = new Storyboard();
						var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Warning_Hover")), new Duration(TimeSpan.FromSeconds(0.2)));
						ca.EasingFunction = ce;
						Storyboard.SetTarget(ca, CheckBox_Border);
						Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
						sb.Children.Add(ca);
						sb.Begin();
					}
					else
					{
						var sb = new Storyboard();
						var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Warning")), new Duration(TimeSpan.FromSeconds(0.2)));
						ca.EasingFunction = ce;
						Storyboard.SetTarget(ca, CheckBox_Border);
						Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
						sb.Children.Add(ca);
						sb.Begin();
					}
					break;
			}
		}

		private void Btn_Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			isMouseDown = true;
			var scX = new DoubleAnimation(0.95, TimeSpan.FromSeconds(0.1));
			scX.EasingFunction = ce;
			CheckBox_Border_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);
			var scY = new DoubleAnimation(0.95, TimeSpan.FromSeconds(0.1));
			scY.EasingFunction = ce; CheckBox_Border_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
			switch (ButtonType)
			{
				case ButtonTypes.Main:
					var caM = new ColorAnimation(BrushToColor(GetBrush("Brush_Main_Press")), new Duration(TimeSpan.FromSeconds(0.1)));
					caM.EasingFunction = ce;
					CheckBox_Border.Background.BeginAnimation(SolidColorBrush.ColorProperty, caM);
					break;

				case ButtonTypes.Normal:
					var caN = new ColorAnimation(BrushToColor(GetBrush("Brush_Normal_Press")), new Duration(TimeSpan.FromSeconds(0.1)));
					caN.EasingFunction = ce;
					CheckBox_Border.Background.BeginAnimation(SolidColorBrush.ColorProperty, caN);
					break;

				case ButtonTypes.Warning:
					var caW = new ColorAnimation(BrushToColor(GetBrush("Brush_Warning_Press")), new Duration(TimeSpan.FromSeconds(0.1)));
					caW.EasingFunction = ce;
					CheckBox_Border.Background.BeginAnimation(SolidColorBrush.ColorProperty, caW);
					break;
			}
		}

		private void Btn_Border_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (isMouseDown)
			{
				isMouseDown = false;
				var scX = new DoubleAnimation(1, TimeSpan.FromSeconds(0.1));
				scX.EasingFunction = ce;
				CheckBox_Border_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);
				var scY = new DoubleAnimation(1, TimeSpan.FromSeconds(0.1));
				scY.EasingFunction = ce; CheckBox_Border_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
			}
		}

		private void Btn_Border_MouseLeave(object sender, MouseEventArgs e)
		{
			Button_ChangeColor();
			var scX = new DoubleAnimation(1, TimeSpan.FromSeconds(0.1));
			scX.EasingFunction = ce;
			CheckBox_Border_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);
			var scY = new DoubleAnimation(1, TimeSpan.FromSeconds(0.11));
			scY.EasingFunction = ce; CheckBox_Border_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
		}

		#endregion 事件

		#region 属性

		public ButtonTypes ButtonType { get; set; } = ButtonTypes.Normal;

		#endregion 属性

		#region 依赖属性

		public new static readonly DependencyProperty PaddingProperty =
			DependencyProperty.Register("Padding", typeof(Thickness), typeof(SodaCheckBox), new PropertyMetadata(new PropertyChangedCallback((d, e) =>
			{
				// if (btn != null)
					// btn.Btn_Txb.Padding = (Thickness)e.NewValue;
			})));

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof(string), typeof(SodaCheckBox), new PropertyMetadata(new PropertyChangedCallback((d, e) =>
			{
				// if (btn != null)
					// btn.Btn_Txb.Text = (string)e.NewValue;
			})));

		public new Thickness Padding
		{
			get { return (Thickness)GetValue(PaddingProperty); }
			set { SetValue(PaddingProperty, value); }
		}

		public string Text

		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		#endregion 依赖属性

		public SodaCheckBox()
		{
			InitializeComponent();
			checkBox = this;
		}
	}
}
