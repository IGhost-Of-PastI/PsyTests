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
        private void LoadTest()
        {
            string folderPath = "Tests/";
            string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");

            foreach (string filePath in jsonFiles)
            {
                string json = File.ReadAllText(filePath);
                OprosnicTest? test = JsonSerializer.Deserialize<OprosnicTest>(json);
                if(test!=null)
                {
                    TestMetaData meta = test.metaData;
                    meta.PathToImg= System.IO.Path.GetFullPath(test.metaData.PathToImg);
                    test.metaData = meta;
                    oprosnics.Add(test);
                }
            }
            ListOfOprosnics.ItemsSource = null;
            ListOfOprosnics.ItemsSource = oprosnics;
        }
        public MainWindow()
        {
            InitializeComponent();
            LoadTest();
        }
        public void OnTestEditorSaveEvent()
        {
            LoadTest();
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
            testsEditor.AfterSaveEvent += OnTestEditorSaveEvent;
            testsEditor.ShowDialog();
        }
    }
}
