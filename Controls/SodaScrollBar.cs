using System;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using System.Text;
using System.Threading.Tasks;

namespace SodaCL.Controls
{
	public class SodaScrollBar : ScrollBar

	{
		public void ScrollBar_ChangeColor()
		{
			try
			{
				string newColor;
				double newOpacity, aniTime;
				if (IsMouseCaptureWithin)
				{
					newOpacity = 1;
					newColor = "Brush_Main_Press";
					aniTime = 0.05;
				}
				else if (IsMouseOver)
				{
					newOpacity = 0.9;
					newColor = "Brush_Main_Hover";
					aniTime = 0.05;
				}
				else
				{
					newOpacity = 0.5;
					newColor = "Brush_Main";
					aniTime = 0.18;
				}
			}
			catch (Exception ex)
			{
			}
		}
	}
}