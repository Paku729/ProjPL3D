using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTextBox.Text;
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;
            string confirmPassword = confirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                ShowErrorMessage("Invalid email format.");
                return;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                ShowErrorMessage("Username cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowErrorMessage("Password cannot be empty.");
                return;
            }

            if (password != confirmPassword)
            {
                ShowErrorMessage("Passwords do not match.");
                return;
            }

            // Регистрация пользователя
            // Здесь вы можете добавить логику для регистрации пользователя в системе

            //MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            ClearFields();
        }

        private void ShowErrorMessage(string message)
        {
            errorMessageTextBlock.Text = message;
        }

        private void ClearFields()
        {
            emailTextBox.Clear();
            usernameTextBox.Clear();
            passwordBox.Clear();
            confirmPasswordBox.Clear();
            errorMessageTextBlock.Text = string.Empty;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Навигация на страницу авторизации
            Page currentPage = NavigationService?.Content as Page;

            // Создаем новую страницу (RegistrationPage)
            LoginPage logintPage = new LoginPage();

            // Устанавливаем новую страницу в качестве содержимого
            NavigationService.Navigate(logintPage);
            NavigationService?.RemoveBackEntry();

            // Если предыдущая страница была получена успешно и ее контент не равен null
            if (currentPage != null && currentPage.Content != null)
            {
                // Очищаем контент предыдущей страницы
                currentPage.Content = null;

                // Удаляем навигацию со страницы, чтобы освободить ресурсы
                currentPage = null;
            }
        }

        private bool IsValidEmail(string email)
        {
            // Простая проверка формата email с использованием регулярного выражения
            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
