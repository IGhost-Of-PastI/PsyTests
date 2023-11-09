using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;


namespace PsyTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<OprosnicTest> oprosnics = new ObservableCollection<OprosnicTest>();
        public MainWindow()
        {
            InitializeComponent();
            string folderPath = "Tests/";
            string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");

            foreach (string filePath in jsonFiles)
            {
                string json = File.ReadAllText(filePath);
                OprosnicTest? test = JsonSerializer.Deserialize<OprosnicTest>(json);
                oprosnics.Add(test);
            }
            TestsList.ItemsSource = oprosnics;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            OprosnicTest item = (OprosnicTest)button.DataContext;
            PreTestWindow preTestWindow = new(item);
            preTestWindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TestsEditor testsEditor = new TestsEditor();
            testsEditor.ShowDialog();
        }
    }
}
