using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using PsyTests.WindowForTestTypes.OprosickType;
using System.Collections.Generic;
using System;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace PsyTests
{
    public partial class TestsEditor : Window
    {
        public BitmapImage LoadImage(string relativePath)
        {
            string fullpath = System.IO.Path.GetFullPath(relativePath);
            Uri uri = new Uri(fullpath);
           return new BitmapImage(uri);
        }

        public event Action AfterSaveEvent;
        ObservableCollection<LinkedNode> LinkedNodes = new();
        string pathToImage = @"Images/noImage.jpg";
        public TestsEditor()
        {
            InitializeComponent();
            ShkalasList.ItemsSource = LinkedNodes; 
            TestImage.Source = LoadImage(pathToImage);
        }
        private void AddNewElement(string value)
        {
            bool temp = false;
            foreach(LinkedNode node in LinkedNodes)
            {
                if(node.shkala.Name==value)
                {
                    temp = true;
                    break;
                }
            }
            if(temp==false)
            {
                LinkedNodes.Add(new LinkedNode(new Shkala(value)));
            }
            else
            {
                MessageBox.Show("Шкала с таким названием уже существует!");
            }
        }
        private void OnChildWindowClosed(object sender, string value)
        {
            AddNewElement(value);
        }
        private void OnChildWindowClosed1(object sender, string value)
        {
            var selectedItem = ShkalasList.SelectedItem as LinkedNode;
            if (selectedItem != null)
            {
                selectedItem.collection.Add(new Key(value, selectedItem.shkala));
                KeysList.ItemsSource = null;
                KeysList.ItemsSource = selectedItem.collection;

            }
        }
        private void AddShakala_Click(object sender, RoutedEventArgs e)
        {
            var childWindow = new AddElement();
            childWindow.ChildWindowClosed += OnChildWindowClosed;
            childWindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(ShkalasList.SelectedItem!=null)
            {
                var childWindow = new AddElement();
                childWindow.ChildWindowClosed += OnChildWindowClosed1;
                childWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбрана шкала для добавления ключа!");
            }
        }

        private void ShkalasList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ShkalasList.SelectedItem as LinkedNode;
            if(selectedItem !=null)
            {
                KeysList.ItemsSource = null;
                KeysList.ItemsSource=selectedItem.collection;
            }
        }

        bool IsAllShkalasAndKeysFilled()
        {
            if(LinkedNodes.Count!=0)
            {
                bool temp = true;
                foreach (LinkedNode linkedNode in LinkedNodes)
                {
                    if(linkedNode.collection.Count==0)
                    {
                        temp = false;
                        break;
                    }
                }
                return temp;
            }
            else
            {
                return false;
            }
        }

        private void SaveTest_Click(object sender, RoutedEventArgs e)
        {

            if(TestName.Text !=""&&TestOpisan.Text!=""&&TestAlgorith.Text!=""&&IsAllShkalasAndKeysFilled()==true)
            {
                TestMetaData testMeta = new();
                testMeta.Name = TestName.Text;
                testMeta.Opisanie = TestOpisan.Text;
                testMeta.Algorithm = TestAlgorith.Text;
                testMeta.PathToImg = pathToImage;
                List<Shkala> shkalas = new();
                List<Key> keys = new();
                foreach (LinkedNode linkedNode in LinkedNodes)
                {
                    shkalas.Add(linkedNode.shkala);
                    foreach (Key key in linkedNode.collection)
                    {
                        keys.Add(key);
                    }
                }

                OprosnicTest test = new OprosnicTest(testMeta, shkalas, keys);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(OprosnicTest));
                using (FileStream fs = new FileStream($"Tests/{TestName.Text}.json", FileMode.Create))
                {
                    serializer.WriteObject(fs, test);
                }
                MessageBox.Show("Сохранение прошло успешно!");
                AfterSaveEvent?.Invoke();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите закрыть окно? Все несохранённые данные будут утеряны!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(ShkalasList.SelectedItem!=null && KeysList.SelectedItem!=null)
            {
                var node = ShkalasList.SelectedItem as LinkedNode;
                node.collection.Remove(KeysList.SelectedItem as Key);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (ShkalasList.SelectedItem != null)
            {
                LinkedNodes.Remove(ShkalasList.SelectedItem as LinkedNode);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string sourcePath = openFileDialog.FileName;
                string targetPath = "Images/"; // Замените на путь к вашей папке
                string Filename = Path.GetFileName(sourcePath);
                try
                {
                    File.Copy(sourcePath, Path.Combine(targetPath, Path.GetFileName(sourcePath)), true);
                    pathToImage = targetPath + Filename;
                    TestImage.Source = LoadImage(pathToImage);
                    MessageBox.Show("Файл успешно скопирован!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при копировании файла: " + ex.Message);
                }
            }
        }
    }
}
