using SodaCL.Toolkits;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SodaCL.Controls.Dialogs {

	/// <summary>
	/// SodaDialog.xaml 的交互逻辑
	/// </summary>
	public partial class SodaDialog : UserControl {

		public delegate CloseEventDelegate CloseEventDelegate();

		public event CloseEventDelegate CloseEvent;

		public enum DialogType {
			Info,
			Warning,
			Error
		}

		#region 字段

		private DialogType Type { get; } = DialogType.Info;

		private TimeSpan DialogAniSpeed { get; } = TimeSpan.FromSeconds(0.25);
		private CubicEase EasingFunc { get; set; } = new CubicEase { EasingMode = EasingMode.EaseInOut };
		private TimeSpan OpacAniSpeed { get; } = TimeSpan.FromSeconds(0.20);

		#endregion 字段

		public SodaDialog(DialogType type, string errorMessage) {
			var dialogData = new DialogData();
			InitializeComponent();
			switch (type) {
				case DialogType.Info:
					dialogData.ThemeColor = GetResources.GetBrush("Brush_Main");
					dialogData.BackgroundColor = GetResources.GetBrush("Brush_Dialog_Info_Background");
					dialogData.Icon = GetResources.GetSvg("Svg_Information");
					Button.ButtonType = SodaButton.ButtonTypes.Normal;
					break;

				case DialogType.Warning:
					dialogData.ThemeColor = GetResources.GetBrush("Brush_Warning");
					dialogData.BackgroundColor = GetResources.GetBrush("Brush_Dialog_Error_Background");
					dialogData.Icon = GetResources.GetSvg("Svg_Error");
					Button.ButtonType = SodaButton.ButtonTypes.Warning;
					break;

				case DialogType.Error:
					dialogData.ThemeColor = GetResources.GetBrush("Brush_Error");
					dialogData.BackgroundColor = GetResources.GetBrush("Brush_Dialog_Error_Background");
					dialogData.Icon = GetResources.GetSvg("Svg_Error");
					Button.ButtonType = SodaButton.ButtonTypes.Error;
					break;
			}

			this.DataContext = dialogData;
			Open(errorMessage);
		}

		public void Open(string errorMessage) {
			GlobalVariable.IsDialogOpen = true;
			Txb_ErrorMessage.Text = errorMessage;
			MainWindow.mainWindow.TitleBar_SettingsBtn.IsEnabled = false;

			var rectOpacAni = new DoubleAnimation(0, 0.15, OpacAniSpeed);
			Rec_Background.BeginAnimation(OpacityProperty, rectOpacAni);

			var DialogOpacAni = new DoubleAnimation(0, 1, OpacAniSpeed);
			Border_Dialog.BeginAnimation(OpacityProperty, DialogOpacAni);

			var scX = new DoubleAnimation(0.9, 1, DialogAniSpeed);
			scX.EasingFunction = EasingFunc;
			Dialog_Border_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);

			var scY = new DoubleAnimation(0.9, 1, DialogAniSpeed);
			scY.EasingFunction = EasingFunc; Dialog_Border_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
			Button.Click += (sender, e) => {
				Close();
			};
			MainWindow.mainWindow.Grid_DialogArea.Children.Add(this);
		}

		public void Close() {
			MainWindow.mainWindow.TitleBar_SettingsBtn.IsEnabled = true;
			var scX = new DoubleAnimation(1, 0.9, DialogAniSpeed);
			scX.EasingFunction = EasingFunc;
			Dialog_Border_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);

			var scY = new DoubleAnimation(1, 0.9, DialogAniSpeed);
			scY.EasingFunction = EasingFunc;

			Dialog_Border_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
			var rectOpacAni = new DoubleAnimation(0.15, 0, OpacAniSpeed);
			Rec_Background.BeginAnimation(OpacityProperty, rectOpacAni);

			var DialogOpacAni = new DoubleAnimation(1, 0, OpacAniSpeed);
			DialogOpacAni.Completed += (sender, e) => {
				Visibility = System.Windows.Visibility.Collapsed;
				MainWindow.mainWindow.Grid_DialogArea.Children.Clear();
			};
			Border_Dialog.BeginAnimation(OpacityProperty, DialogOpacAni);

			GlobalVariable.IsDialogOpen = false;

			CloseEvent?.Invoke();
		}
	}

	public class DialogData {
		public Brush ThemeColor { get; set; }
		public Brush BackgroundColor { get; set; }
		public DrawingImage Icon { get; set; }
		public SodaButton.ButtonTypes ButtonType { get; set; }
	}
}