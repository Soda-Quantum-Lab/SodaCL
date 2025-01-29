using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SodaCL.Controls.Dialogs {

	/// <summary>
	/// SodaLauncherErrorDialog.xaml 的交互逻辑
	/// </summary>
	public partial class SodaLauncherErrorDialog : UserControl {

		#region 字段

		private TimeSpan DialogAniSpeed { get; } = TimeSpan.FromSeconds(0.5);
		private CubicEase EasingFunc { get; set; } = new CubicEase { EasingMode = EasingMode.EaseInOut };
		private TimeSpan OpacAniSpeed { get; } = TimeSpan.FromSeconds(0.5);

		#endregion 字段

		public SodaLauncherErrorDialog(string errorMessage) {
			InitializeComponent();
			Open(errorMessage);
		}

		public void Open(string errorMessage) {
			GlobalVariable.IsDialogOpen = true;
			Txb_ErrorMessage.Text = errorMessage;
			MainWindow.mainWindow.TitleBar_SettingsBtn.IsEnabled = false;

			var rectOpacAni = new DoubleAnimation(0, 0.5, OpacAniSpeed);
			Rec_Background.BeginAnimation(OpacityProperty, rectOpacAni);

			var DialogOpacAni = new DoubleAnimation(0, 1, OpacAniSpeed);
			Border_Dialog.BeginAnimation(OpacityProperty, DialogOpacAni);

			var scX = new DoubleAnimation(0.9, 1, DialogAniSpeed);
			scX.EasingFunction = EasingFunc;
			Dialog_Border_Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scX);

			var scY = new DoubleAnimation(0.9, 1, DialogAniSpeed);
			scY.EasingFunction = EasingFunc; Dialog_Border_Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scY);
			Button_Close.Click += (sender, e) => {
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
			var rectOpacAni = new DoubleAnimation(0.5, 0, OpacAniSpeed);
			Rec_Background.BeginAnimation(OpacityProperty, rectOpacAni);

			var DialogOpacAni = new DoubleAnimation(1, 0, OpacAniSpeed);
			DialogOpacAni.Completed += (sender, e) => {
				Visibility = System.Windows.Visibility.Collapsed;
				MainWindow.mainWindow.Grid_DialogArea.Children.Clear();
			};
			Border_Dialog.BeginAnimation(OpacityProperty, DialogOpacAni);

			GlobalVariable.IsDialogOpen = false;
		}
	}
}