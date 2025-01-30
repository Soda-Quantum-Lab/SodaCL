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
		private bool isEnabled = false;

		#region 枚举

		#endregion 枚举

		#region 事件

		public event RoutedEventHandler Click;

		private void Btn_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (isMouseDown)
			{
				Log(false, ModuleList.Control, LogInfo.Info, $"切换复选框 \"{Text}\"");
				Click?.Invoke(sender, e);
			}
		}

		private void Border_Loaded(object sender, RoutedEventArgs e)
		{
			// CheckBox_Border.Background = (SolidColorBrush)GetBrush("Brush_Main");

			CheckBox_Border.Background = new SolidColorBrush(BrushToColor(GetBrush("Brush_Main")));
		}

		public void Btn_MouseEnter(object sender, MouseEventArgs e)
		{

		}

		public void Button_ChangeColor(object sender = null, MouseEventArgs e = null)
		{
			//if (IsMouseOver)
			//{
			//	var sb = new Storyboard();
			//	var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Main_Hover")), new Duration(TimeSpan.FromSeconds(0.2)));
			//	ca.EasingFunction = ce;
			//	Storyboard.SetTarget(ca, CheckBox_Border);
			//	Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
			//	sb.Children.Add(ca);
			//	sb.Begin();
			//}
			//else
			//{
			//	var sb = new Storyboard();
			//	var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Main")), new Duration(TimeSpan.FromSeconds(0.2)));
			//	ca.EasingFunction = ce;
			//	Storyboard.SetTarget(ca, CheckBox_Border);
			//	Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
			//	sb.Children.Add(ca);
			//	sb.Begin();
			//}
		}

		private void Btn_Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			isMouseDown = true;
			var scX = new DoubleAnimation(0.95, TimeSpan.FromSeconds(0.1));
			scX.EasingFunction = ce;
			CheckBox_Border_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);
			// CheckBox_BgBorder_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);
			var scY = new DoubleAnimation(0.95, TimeSpan.FromSeconds(0.1));
			scY.EasingFunction = ce; 
			CheckBox_Border_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
			// CheckBox_BgBorder_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);

			if (isEnabled)
			{
				var caM = new ColorAnimation(BrushToColor(GetBrush("Brush_Main")), new Duration(TimeSpan.FromSeconds(0.3)));
				caM.EasingFunction = ce;
				CheckBox_BgBorder.Background.BeginAnimation(SolidColorBrush.ColorProperty, caM);
				// CheckBox_BgBorder.Background.BeginAnimation(SolidColorBrush.ColorProperty, caM);

				var daM = new DoubleAnimation();
				daM.From = 105;
				daM.To = 125;
				daM.Duration = TimeSpan.FromSeconds(0.1);
				CheckBox_Border.BeginAnimation(WidthProperty, daM);
				// CheckBox_BgBorder.Background.BeginAnimation(WidthProperty, daM);
				// CheckBox_Border.Background = (SolidColorBrush)GetBrush("Brush_Main");
				isEnabled = false;
			}
			else
			{

				var caM = new ColorAnimation(BrushToColor(GetBrush("Brush_Normal")), new Duration(TimeSpan.FromSeconds(0.3)));
				caM.EasingFunction = ce;
				CheckBox_BgBorder.Background.BeginAnimation(SolidColorBrush.ColorProperty, caM);
				// CheckBox_BgBorder.Background.BeginAnimation(SolidColorBrush.ColorProperty, caM);

				var daM = new DoubleAnimation();
				daM.From = 125;
				daM.To = 105;
				daM.Duration = TimeSpan.FromSeconds(0.1);
				CheckBox_Border.BeginAnimation(WidthProperty, daM);
				// CheckBox_BgBorder.BeginAnimation(WidthProperty, daM);
				// CheckBox_Border.Background = (SolidColorBrush)GetBrush("Brush_Normal");
				isEnabled = true;
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
				// CheckBox_BgBorder_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);
				var scY = new DoubleAnimation(1, TimeSpan.FromSeconds(0.1));
				scY.EasingFunction = ce; 
				CheckBox_Border_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
				// CheckBox_BgBorder_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
			}
		}

		private void Btn_Border_MouseLeave(object sender, MouseEventArgs e)
		{
			// Button_ChangeColor();
			var scX = new DoubleAnimation(1, TimeSpan.FromSeconds(0.1));
			scX.EasingFunction = ce;
			CheckBox_Border_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);
			// CheckBox_BgBorder_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);
			var scY = new DoubleAnimation(1, TimeSpan.FromSeconds(0.11));
			scY.EasingFunction = ce; 
			CheckBox_Border_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
			// CheckBox_BgBorder_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
		}

		#endregion 事件

		#region 属性


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
