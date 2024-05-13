using HelixToolkit.Wpf;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ProjPL3D
{
    /// <summary>
    /// Логика взаимодействия для ProjectWindow.xaml
    /// </summary>
    /// 

    public partial class ProjectWindow : Window
    {

        private bool _isMouseCaptured;
        private Point _lastMousePos;

        private PerspectiveCamera _camera;

    
        private ModelVisual3D selectedModel;
      
        private List<ArrowVisual3D> axisArrows = new List<ArrowVisual3D>();

        public ProjectWindow()
        {
            InitializeComponent();
            _camera = new PerspectiveCamera(new Point3D(0, 0, 5), new Vector3D(0, 0, -1), new Vector3D(0, 1, 0), 45);
            viewport.Camera = _camera;
            viewport.MouseDown += ViewportMouseDown;

        }


        private void viewport_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                _lastMousePos = e.GetPosition(viewport);
                _isMouseCaptured = true;
                Mouse.OverrideCursor = Cursors.SizeAll;
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) && e.LeftButton == MouseButtonState.Pressed)
            {
                _lastMousePos = e.GetPosition(viewport);
                _isMouseCaptured = true;
                Mouse.OverrideCursor = Cursors.Hand;
            }
        }

        private void viewport_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseCaptured)
            {
                Point currentPos = e.GetPosition(viewport);
                Vector moveVector = currentPos - _lastMousePos;

                if (e.RightButton == MouseButtonState.Pressed)
                {
                    // Вращение камеры вокруг сцены (перемещение мыши с нажатой ПКМ)
                    RotateCamera(moveVector.X, moveVector.Y);
                }
                else if (e.LeftButton == MouseButtonState.Pressed && Keyboard.IsKeyDown(Key.LeftAlt))
                {
                    // Перемещение камеры вдоль экрана (перемещение мыши с нажатой левой кнопкой и Alt)
                    MoveCamera(moveVector.X, moveVector.Y);
                }

                _lastMousePos = currentPos;

            }
        }


        private void viewport_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseCaptured = false;
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void viewport_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Масштабирование сцены (используя колесо мыши)
            double scaleFactor = e.Delta > 0 ? 1.1 : 0.9;
            ScaleCamera(scaleFactor);
        }


        private void MoveCamera(double deltaX, double deltaY)
        {
            // Получаем текущее направление обзора камеры
            Vector3D cameraDirection = (Vector3D)(_camera.LookDirection - _camera.Position);

            // Нормализуем вектор направления камеры
            cameraDirection.Normalize();

            // Определяем вектор вверх относительно камеры
            Vector3D cameraUp = Vector3D.CrossProduct(cameraDirection, new Vector3D(1, 0, 0));

            // Нормализуем вектор вверх
            cameraUp.Normalize();

            // Двигаем камеру вдоль плоскости, перпендикулярной направлению камеры
            _camera.Position += cameraUp * deltaY;
        }


        private void RotateCamera(double deltaX, double deltaY)
        {
            // Получаем текущее направление обзора камеры
            Vector3D cameraDirection = (Vector3D)(_camera.LookDirection - _camera.Position);

            // Нормализуем вектор направления камеры
            cameraDirection.Normalize();

            // Определяем вектор вправо относительно камеры (путем векторного произведения с вектором "вверх")
            Vector3D cameraRight = Vector3D.CrossProduct(cameraDirection, new Vector3D(0, 1, 0));

            // Нормализуем вектор вправо
            cameraRight.Normalize();

            // Двигаем камеру по контуру сферы (движение вправо)
            _camera.Position += cameraRight * deltaX;

            // Определяем вектор вверх относительно камеры
            Vector3D cameraUp = Vector3D.CrossProduct(cameraRight, cameraDirection);

            // Нормализуем вектор вверх
            cameraUp.Normalize();

            // Двигаем камеру вверх или вниз (движение вверх)
            _camera.Position += cameraUp * deltaY;
        }
        private void ScaleCamera(double scaleFactor)
        {
            // Масштабирование камеры
            double minDistance = 1;
            double maxDistance = 100;

            // Определяем направление обзора камеры
            Vector3D cameraDirection = (Vector3D)(_camera.LookDirection - _camera.Position);

            double currentDistance = cameraDirection.Length;

            double newDistance = currentDistance * scaleFactor;

            // Ограничиваем новое расстояние
            newDistance = Math.Max(minDistance, Math.Min(maxDistance, newDistance));

            // Нормализуем направляющий вектор камеры и умножаем его на новое расстояние
            Vector3D normalizedDirection = cameraDirection;
            normalizedDirection.Normalize();
            Vector3D offset = normalizedDirection * (newDistance - currentDistance);

            // Добавляем смещение к позиции камеры
            _camera.Position += offset;
        }


        private void ViewportMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var source = (HelixViewport3D)sender;
                var point = e.GetPosition(source);
                var hitResult = VisualTreeHelper.HitTest(source, point);

                if (hitResult.VisualHit is ModelVisual3D model)
                {
                    SelectModel(model);
                }
                else
                {
                    DeselectModel();
                }
            }
        }

   

        // Выделение модели
        private void SelectModel(ModelVisual3D model)
        {
            DeselectModel(); // Снимаем выделение с предыдущего объекта

            // Отображаем осевые стрелки
            ShowAxisArrows(model);

            // Меняем цвет модели
            var brush = new SolidColorBrush(Colors.Green);
            ApplyColorToModel(model, brush);

            // Запоминаем выделенную модель
            selectedModel = model;
        }


        // Снятие выделения с модели
        private void DeselectModel()
        {
            if (selectedModel != null)
            {
                // Убираем стрелки осей координат
                RemoveAxisArrows();

                // Возвращаем предыдущей модели исходный цвет
                var brush = new SolidColorBrush(Colors.Gray);
                ApplyColorToModel(selectedModel, brush);
                selectedModel = null;
            }
        }



        // Применение цвета к модели
        private void ApplyColorToModel(ModelVisual3D model, Brush color)
        {
            var material = new DiffuseMaterial(color);
            if (model.Content is GeometryModel3D geometryModel)
            {
                geometryModel.Material = material;
            }
            else if (model.Content is Model3DGroup modelGroup)
            {
                foreach (var child in modelGroup.Children)
                {
                    if (child is GeometryModel3D childGeometryModel)
                    {
                        childGeometryModel.Material = material;
                    }
                }
            }
        }

        private void Viewport3D_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Проверяем, был ли клик по модели
            var source = (Viewport3D)sender;
            var point = e.GetPosition(source);
            var hitResult = VisualTreeHelper.HitTest(source, point);
            if (hitResult.VisualHit is ModelVisual3D model)
            {
                SelectModel(model); // Выделяем модель
            }
            else
            {
                DeselectModel(); // Снимаем выделение, если клик был не по модели
            }
        }

        private void ShowAxisArrows(ModelVisual3D model)
        {
            // Убираем предыдущие стрелки
            RemoveAxisArrows();

            // Получаем центр объекта
            var bounds = model.Content.Bounds;
            var center = new Point3D(bounds.X + bounds.SizeX / 2, bounds.Y + bounds.SizeY / 2, bounds.Z + bounds.SizeZ / 2);

            // Создаем стрелки осей координат
            var xAxisArrow = new ArrowVisual3D
            {
                Direction = new Vector3D(1, 0, 0),
                Diameter = 0.1,
                Point1 = center,
                Point2 = new Point3D(center.X + 2, center.Y, center.Z),
                Fill = Brushes.Red
            };

            var yAxisArrow = new ArrowVisual3D
            {
                Direction = new Vector3D(0, 1, 0),
                Diameter = 0.1,
                Point1 = center,
                Point2 = new Point3D(center.X, center.Y + 2, center.Z),
                Fill = Brushes.Green
            };

            var zAxisArrow = new ArrowVisual3D
            {
                Direction = new Vector3D(0, 0, 1),
                Diameter = 0.1,
                Point1 = center,
                Point2 = new Point3D(center.X, center.Y, center.Z + 2),
                Fill = Brushes.Blue
            };

            // Добавляем стрелки в сцену
            viewport.Children.Add(xAxisArrow);
            viewport.Children.Add(yAxisArrow);
            viewport.Children.Add(zAxisArrow);

            // Сохраняем ссылки на стрелки для последующего удаления
            axisArrows.Add(xAxisArrow);
            axisArrows.Add(yAxisArrow);
            axisArrows.Add(zAxisArrow);
        }

        private void RemoveAxisArrows()
        {
            foreach (var arrow in axisArrows)
            {
                viewport.Children.Remove(arrow);
            }
            axisArrows.Clear();
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
        private void AddSphereButton_Click(object sender, RoutedEventArgs e)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(new Point3D(0, 0, 0)); // Центр сферы
            mesh.TriangleIndices.Add(0);
        }



        private void AddPyramidButton_Click(object sender, RoutedEventArgs e)
        {
            ModelVisual3D pyramid = new ModelVisual3D();
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(new Point3D(0, 0, 0)); // Вершины пирамиды
            mesh.Positions.Add(new Point3D(-0.5, -0.5, 0.5));
            mesh.Positions.Add(new Point3D(0.5, -0.5, 0.5));
            mesh.Positions.Add(new Point3D(0.5, 0.5, 0.5));
            mesh.Positions.Add(new Point3D(-0.5, 0.5, 0.5));

            mesh.TriangleIndices.Add(0); // Грани пирамиды
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(1);

            // Создание модели пирамиды
            pyramid.Content = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Black));
            //viewport.Children.Add(pyramid);


            //viewport.Children.Add(cube);

            

            // Добавление куба на сцену 
            viewport.Children.Add(pyramid);
        }

        private void AddCubeButton_Click(object sender, RoutedEventArgs e)
        {
            ModelVisual3D cube = new ModelVisual3D();
            
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(new Point3D(0, 0, 0));
            mesh.Positions.Add(new Point3D(1, 0, 0));
            mesh.Positions.Add(new Point3D(1, 1, 0));
            mesh.Positions.Add(new Point3D(0, 1, 0));
            mesh.Positions.Add(new Point3D(0, 0, 1));
            mesh.Positions.Add(new Point3D(1, 0, 1));
            mesh.Positions.Add(new Point3D(1, 1, 1));
            mesh.Positions.Add(new Point3D(0, 1, 1));
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(7);
            cube.Content = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Red));



            //viewport.Children.Add(cube);

           

         
            // Добавление куба на сцену 
            viewport.Children.Add(cube);
        }
   
    }
}


