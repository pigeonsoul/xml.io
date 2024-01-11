using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1;

namespace WpfApp
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            // Проверка, соответствует ли введенный логин и пароль значению "admin"
            if (username == "admin" && password == "admin")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Закрытие окна входа
                Close();
            }
            else
            {
                // Вывод сообщения об ошибке в случае неверного логина или пароля
                errorMessageLabel.Content = "Invalid login or password.";
            }
        }
    }
}