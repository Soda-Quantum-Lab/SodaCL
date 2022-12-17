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

        private void loginSourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
            //MClogin.clientID = this.login.Source.ToString();
            //TODO:错误处理
            //MessageBox.Show(MClogin.clientID);
            this.Close();
        }
    }
}

