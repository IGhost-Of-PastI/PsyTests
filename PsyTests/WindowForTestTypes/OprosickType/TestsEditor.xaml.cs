using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using PsyTests.WindowForTestTypes.OprosickType;

namespace PsyTests
{
   
    /// <summary>
    /// Логика взаимодействия для TestsEditor.xaml
    /// </summary>
    /// 
 

    public partial class TestsEditor : Window
    {
        ObservableCollection<Shkala> shkalas = new ObservableCollection<Shkala>();
        public TestsEditor()
        {
            InitializeComponent();
            ShkalasList.ItemsSource = shkalas;
        }
        private void OnChildWindowClosed(object sender, string value)
        {
            shkalas.Add(new Shkala(value));
        }
        private void OnChildWindowClosed1(object sender, string value)
        {
            var selectedItem = ShkalasList.SelectedItem as Shkala;
            if (selectedItem != null)
            {

                selectedItem.Keys.Add(new Key(value,selectedItem));
                KeysList.ItemsSource = null;
                KeysList.ItemsSource = selectedItem.Keys;
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
            var selectedItem = ShkalasList.SelectedItem as Shkala;
            if(selectedItem !=null)
            {
                KeysList.ItemsSource = null;
                KeysList.ItemsSource=selectedItem.Keys;
            }
        }

        private void SaveTest_Click(object sender, RoutedEventArgs e)
        {
            OprosnicTest test = new OprosnicTest(TestName.Text, TestOpisan.Text, "pathToImage", TestAlgorith.Text, shkalas);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(OprosnicTest));
            using (FileStream fs = new FileStream($"Tests/{TestName.Text}.json", FileMode.Create))
            {
                serializer.WriteObject(fs, test);
            }
        }
    }
}
