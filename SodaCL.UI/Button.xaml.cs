using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SodaCL.UI
{
    /// <summary>
    /// Button.xaml 的交互逻辑
    /// </summary>
    public partial class Button : UserControl
    {
        public ButtonTypes ButtonType { get; set; }
        public string Text
        {
            get
            {
                return this.ButtonTxb.Text;
            }
            set { this.ButtonTxb.Text = value; }
        }
        public enum ButtonTypes
        {
            Normal = 0,
            Main = 1,
        }

        public Button()
        {
            InitializeComponent();
        }

        private void ButtonBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (ButtonType)
            {
                case ButtonTypes.Normal:
                    ButtonBorder.Background = (Brush)Application.Current.FindResource("Color6");
                    break;
                case ButtonTypes.Main:
                    ButtonBorder.Background = (Brush)Application.Current.FindResource("Color1");
                    break;
            }
        }
    }
}
