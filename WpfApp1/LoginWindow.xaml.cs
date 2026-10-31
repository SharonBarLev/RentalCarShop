using System;
using System.Windows;

namespace WpfApp1
{
    public partial class LoginWindow : Window
    {
        private ServiceReference1.Service1Client ws =
            new ServiceReference1.Service1Client("BasicHttpBinding_IService1");

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            try
            {
                if (ws.IsExistUserAndPass(username, password))
                {
                    if (ws.IsUserAdmin(username))
                    {
                        MainWindow mainWindow = new MainWindow(username);
                        Application.Current.MainWindow = mainWindow;
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        ErrorText.Text = "You must be an admin.";
                        ErrorText.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    ErrorText.Text = "Invalid username or password.";
                    ErrorText.Visibility = Visibility.Visible;
                }
            }
            catch
            {

                ErrorText.Text = "Unknown Error.";
                ErrorText.Visibility = Visibility.Visible;
            }
        }
    }
}