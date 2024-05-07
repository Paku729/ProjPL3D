using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjPL3D
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string emailOrUsername = emailOrUsernameTextBox.Text;
            string password = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(emailOrUsername))
            {
                ShowErrorMessage("Email or username cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowErrorMessage("Password cannot be empty.");
                return;
            }

            // Проверка логина и пароля
            // Здесь вы можете добавить логику для проверки логина и пароля в вашей системе
            // Например, сверка с базой данных или хранилищем пользователей

            // Проверка успешного входа (в данном примере всегда успешно)
            bool loginSuccessful = true;

            if (loginSuccessful)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
                // Закрыть окно авторизации и открыть главное окно или другую часть вашего приложения
                
            }
            else
            {
                ShowErrorMessage("Invalid email/username or password.");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Навигация на страницу регистрации
            Frame frame = new Frame();
            frame.NavigationService.Navigate(new RegistrationPage());
            this.Content = frame;
        }

        private void ShowErrorMessage(string message)
        {
            errorMessageTextBlock.Text = message;
        }

        private void ClearFields()
        {
            emailOrUsernameTextBox.Clear();
            passwordBox.Clear();
            errorMessageTextBlock.Text = string.Empty;
        }
    }
}

