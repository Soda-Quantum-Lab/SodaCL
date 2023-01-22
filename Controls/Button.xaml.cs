using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
			this.MouseEnter += Button_ChangeColor;
			this.MouseDown += (sender, e) => { isMouseDown = true; };
			this.MouseUp += (sender, e) => { isMouseDown = false; };
			this.MouseLeave += Button_ChangeColor;
			this.MouseUp += (sender, e) =>
			{
				if (isMouseDown)
				{
					Log(false, ModuleList.Control, LogInfo.Info, $"按下按钮 {Text}");
					Click.Invoke(sender, e);
				}
			};
		}

		#region 枚举

		public enum ButtenTypes
		{
			Normal,
			Important,
			//Disable
		}

		#endregion 枚举

		#region 事件

		public event RoutedEventHandler Click;

		#endregion 事件

		#region 属性

		private ButtenTypes _ButtonType = ButtenTypes.Normal;

		public ButtenTypes ButtonType
		{
			get { return _ButtonType; }
			set { _ButtonType = value; Button_ChangeColor(); }
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

		public void Button_ChangeColor(object sender = null, MouseEventArgs e = null)
		{
			switch (this.ButtonType)
			{
				case ButtenTypes.Normal:
					if (IsMouseOver)
					{
						var ca = new ColorAnimation(BrushToColor(GetBrush("Color2")), TimeSpan.FromSeconds(0.3));
						this.Btn_Border.BeginAnimation(BorderBrushProperty, ca);
					}
					else
					{
						var ca = new ColorAnimation(BrushToColor(GetBrush("Color1")), TimeSpan.FromSeconds(0.3));
						this.Btn_Border.BeginAnimation(BorderBrushProperty, ca);
					}
					break;

				case ButtenTypes.Important:
					if (IsMouseOver)
					{
						var ca = new ColorAnimation(BrushToColor(GetBrush("Color12")), TimeSpan.FromSeconds(0.3));
						this.Btn_Border.BeginAnimation(BorderBrushProperty, ca);
					}
					else
					{
						//var ca = new ColorAnimation(BrushToColor(GetBrush("Color11")), TimeSpan.FromSeconds(0.3));
						//this.Btn_Border.BeginAnimation(prop, ca);
					}
					break;
			}
		}
	}
}