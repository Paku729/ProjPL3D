using Microsoft.Win32;
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
    /// Логика взаимодействия для ProjectWindow.xaml
    /// </summary>
    public partial class ProjectWindow : Window
    {
        public ProjectWindow()
        {
            InitializeComponent();
        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Window newWindow = new MainWindow();
           
            
            newWindow.Show();

            // Закрываем текущее окно
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void AddSceneButton_Click(object sender, RoutedEventArgs e)
        {
            // Показать или скрыть панель
            if (panelGrid.Visibility == Visibility.Collapsed)
                panelGrid.Visibility = Visibility.Visible;
            else
                panelGrid.Visibility = Visibility.Collapsed;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string sceneName = sceneNameTextBox.Text;
            if (!string.IsNullOrWhiteSpace(sceneName))
            {
                // Добавление сцены в список
                sceneListBox.Items.Add(sceneName);
                // Очистить текстовое поле и скрыть панель
                sceneNameTextBox.Clear();
                panelGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите название сцены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void SceneListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Открытие выбранной сцены
            string selectedScene = (sender as ListBox).SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedScene))
            {
                // Здесь можно открыть выбранную сцену в центре экрана
                MessageBox.Show($"Открыта сцена: {selectedScene}");
            }
        }
    }   

}
