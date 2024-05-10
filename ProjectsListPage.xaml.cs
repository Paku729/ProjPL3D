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
    /// Логика взаимодействия для ProjectsListPage.xaml
    /// </summary>
    public partial class ProjectsListPage : Page
    {
        public ProjectsListPage()
        {
            InitializeComponent();
            LoadProjects(); 
        }

        private void LoadProjects()
        {
            // Загрузка списка проектов
            // Здесь вы можете добавить код для загрузки списка проектов из базы данных или хранилища
            // Пример:
            projectsListBox.Items.Add("Project 1");
            projectsListBox.Items.Add("Project 2");
            projectsListBox.Items.Add("Project 3");
            // и т.д.
        }
        private void CreateProject_Click(object sender, RoutedEventArgs e)
        {
            // Показать или скрыть панель
            if (panelGrid.Visibility == Visibility.Collapsed)
                panelGrid.Visibility = Visibility.Visible;
            else
                panelGrid.Visibility = Visibility.Collapsed;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string projectName = projectNameTextBox.Text;
            if (!string.IsNullOrWhiteSpace(projectName))
            {
                // Добавление проекта в список
                projectsListBox.Items.Add(projectName);
                // Очистить текстовое поле и скрыть панель
                projectNameTextBox.Clear();
                panelGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Please enter a project name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        private void projectsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (projectsListBox.SelectedItem != null)
            {
                // Открываем новое окно на весь экран
                Window newWindow = new ProjectWindow();
                newWindow.WindowState = WindowState.Maximized; // Открываем на весь экран
                newWindow.Title = "ProjectPL3D   " + projectsListBox.SelectedItem.ToString();
                newWindow.Show(); 

                // Закрываем текущее окно
                Window parentWindow = Window.GetWindow(this);
                parentWindow.Close();
            }

        }

        private void DeleteProject_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string projectName = btn.Tag as string;
            if (!string.IsNullOrEmpty(projectName))
            {
                projectsListBox.Items.Remove(projectName);
            }
        }

    }
}

