using System.Windows;
using SodaCL.Core.Minecraft;
namespace SodaCL.Pages
{
    /// <summary>
    /// MSLogin.xaml 的交互逻辑
    /// </summary>
    public partial class MSLogin : Window
    {
        public MSLogin()
        {
            InitializeComponent();
        }

        private void login_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
            MClogin._clientID = this.login.Source.ToString();
            //TODO:错误处理
            MessageBox.Show(MClogin._clientID);
            this.Close();
        }
    }
}

