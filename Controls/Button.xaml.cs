using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using static SodaCL.Toolkits.DataTool;
using static SodaCL.Toolkits.GetResources;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Controls
{
	/// <summary>
	/// Button.xaml 的交互逻辑
	/// </summary>
	public partial class Button : UserControl
	{
		private static Button btn;
		private bool isMouseDown;

		public Button()
		{
			InitializeComponent();
			btn = this;
			Init();
		}

		#region 枚举

		public enum ButtenTypes
		{
			Main,
			Normal,
			Warning,
		}

		#endregion 枚举

		#region 事件

		public event RoutedEventHandler Click;

		#endregion 事件

		#region 属性

		[DefaultValue(ButtenTypes.Normal)]
		private ButtenTypes _ButtonType;

		public ButtenTypes ButtonType
		{
			get { return _ButtonType; }
			set { _ButtonType = value; }
		}

		#endregion 属性

		#region 依赖属性

		public new static readonly DependencyProperty PaddingProperty =
			DependencyProperty.Register("Padding", typeof(Thickness), typeof(Button), new PropertyMetadata(new PropertyChangedCallback((d, e) =>
			{
				if (btn != null)
					btn.Btn_Txb.Padding = (Thickness)e.NewValue;
			})));

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof(string), typeof(Button), new PropertyMetadata(new PropertyChangedCallback((d, e) =>
			{
				if (btn != null)
					btn.Btn_Txb.Text = (string)e.NewValue;
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

		public void Init()
		{
			switch (ButtonType)
			{
				case ButtenTypes.Main:
					Btn_Border.Background = (SolidColorBrush)GetBrush("Brush_Main");
					break;

				case ButtenTypes.Normal:
					Btn_Border.Background = (SolidColorBrush)GetBrush("Brush_Normal");
					Btn_Txb.Foreground = Brushes.Black;
					break;

				case ButtenTypes.Warning:
					Btn_Border.Background = (SolidColorBrush)GetBrush("Brush_Warning");
					break;
			}

			Btn_Border.MouseEnter += Button_ChangeColor;
			Btn_Border.MouseDown += (sender, e) => { isMouseDown = true; };
			Btn_Border.MouseUp += (sender, e) => { isMouseDown = false; };
			Btn_Border.MouseLeave += Button_ChangeColor;
			Btn_Border.MouseLeftButtonDown += (sender, e) =>
			{
				Log(false, ModuleList.Control, LogInfo.Info, $"按下按钮 {Text}");
				Click.Invoke(sender, e);
			};
		}

		public void Button_ChangeColor(object sender = null, MouseEventArgs e = null)
		{
			switch (this.ButtonType)
			{
				case ButtenTypes.Main:
					if (IsMouseOver)
					{
						var sb = new Storyboard();
						var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Main_Hover")), new Duration(TimeSpan.FromSeconds(0.2)));
						ca.EasingFunction = new CubicEase
						{
							EasingMode = EasingMode.EaseOut
						};
						Storyboard.SetTarget(ca, Btn_Border);
						Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
						sb.Children.Add(ca);
						sb.Begin();
					}
					else
					{
						var sb = new Storyboard();
						var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Main")), new Duration(TimeSpan.FromSeconds(0.2)));
						ca.EasingFunction = new CubicEase
						{
							EasingMode = EasingMode.EaseOut
						};
						Storyboard.SetTarget(ca, Btn_Border);
						Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
						sb.Children.Add(ca);
						sb.Begin();
					}
					break;

				case ButtenTypes.Normal:
					if (IsMouseOver)
					{
						var sb = new Storyboard();
						var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Normal_Hover")), new Duration(TimeSpan.FromSeconds(0.2)));
						ca.EasingFunction = new CubicEase
						{
							EasingMode = EasingMode.EaseOut
						};
						Storyboard.SetTarget(ca, Btn_Border);
						Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
						sb.Children.Add(ca);
						sb.Begin();
					}
					else
					{
						var sb = new Storyboard();
						var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Normal")), new Duration(TimeSpan.FromSeconds(0.2)));
						ca.EasingFunction = new CubicEase
						{
							EasingMode = EasingMode.EaseOut
						};
						Storyboard.SetTarget(ca, Btn_Border);
						Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
						sb.Children.Add(ca);
						sb.Begin();
					}
					break;

				case ButtenTypes.Warning:
					if (IsMouseOver)
					{
						var sb = new Storyboard();
						var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Warning_Hover")), new Duration(TimeSpan.FromSeconds(0.2)));
						ca.EasingFunction = new CubicEase
						{
							EasingMode = EasingMode.EaseOut
						};
						Storyboard.SetTarget(ca, Btn_Border);
						Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
						sb.Children.Add(ca);
						sb.Begin();
					}
					else
					{
						var sb = new Storyboard();
						var ca = new ColorAnimation(BrushToColor(GetBrush("Brush_Warning")), new Duration(TimeSpan.FromSeconds(0.2)));
						ca.EasingFunction = new CubicEase
						{
							EasingMode = EasingMode.EaseOut
						};
						Storyboard.SetTarget(ca, Btn_Border);
						Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
						sb.Children.Add(ca);
						sb.Begin();
					}
					break;
			}
		}
	}
}