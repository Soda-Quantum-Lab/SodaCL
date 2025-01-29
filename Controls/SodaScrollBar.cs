using SodaCL.Toolkits;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using static SodaCL.Toolkits.Logger;

namespace SodaCL.Controls {

	public class SodaScrollBar : ScrollBar {

		public SodaScrollBar() {
			IsEnabledChanged += (sender, e) => { ScrollBar_ChangeColor(); };
			GotMouseCapture += (sender, e) => { ScrollBar_ChangeColor(); };
			LostMouseCapture += (sender, e) => { ScrollBar_ChangeColor(); };
			MouseEnter += (sender, e) => { ScrollBar_ChangeColor(); };
			MouseLeave += (sender, e) => { ScrollBar_ChangeColor(); };
			IsVisibleChanged += (sender, e) => { ScrollBar_ChangeColor(); };
		}

		public void ScrollBar_ChangeColor() {
			try {
				string newColor;
				double newOpacity, aniTime;
				if (IsMouseCaptureWithin) {
					newOpacity = 1;
					newColor = "Brush_Main_Press";
					aniTime = 0.05;
				}
				else if (IsMouseOver) {
					newOpacity = 0.9;
					newColor = "Brush_Main_Hover";
					aniTime = 0.05;
				}
				else {
					newOpacity = 0.5;
					newColor = "Brush_Main";
					aniTime = 0.18;
				}
				if (IsLoaded) {
					var scrollBarSb = new Storyboard();

					var scrollBarColorAni = new ColorAnimation(DataTool.BrushToColor(GetResources.GetBrush(newColor)), TimeSpan.FromSeconds(aniTime));
					Storyboard.SetTarget(scrollBarColorAni, this);
					Storyboard.SetTargetProperty(scrollBarColorAni, new PropertyPath("ForegroundProperty"));

					var scrollBarOpacAni = new DoubleAnimation(newOpacity, TimeSpan.FromSeconds(aniTime));
					Storyboard.SetTarget(scrollBarOpacAni, this);
					Storyboard.SetTargetProperty(scrollBarOpacAni, new PropertyPath("Opacity"));

					scrollBarSb.Begin();
				}
			}
			catch (Exception ex) {
				Log(false, ModuleList.Control, LogInfo.Warning, "SodaCL 滚动条颜色改变出错", ex);
			}
		}
	}
}