using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using PsyTests.WindowForTestTypes.OprosickType;
using System.Collections.Generic;

namespace PsyTests
{
    public partial class TestsEditor : Window
    {
        ObservableCollection<LinkedNode> LinkedNodes = new();
        public TestsEditor()
        {
            InitializeComponent();
            ShkalasList.ItemsSource = LinkedNodes;
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
                testMeta.PathToImg = "pathtoImg";
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
                //отправить экшион в родительское окно
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!");
            }
        }
    }
}
