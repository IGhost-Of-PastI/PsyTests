using System.Windows;
using System.Windows.Controls;

namespace PsyTests
{
    /// <summary>
    /// Логика взаимодействия для PreTest.xaml
    /// </summary>
    public partial class PreTest : Page
    {
        OprosnicTest test;
        public PreTest(OprosnicTest test)
        {
            InitializeComponent();
            this.test = test;
            Name.Text = test.metaData.Name;
            Opis.Text = test.metaData.Opisanie;
            Algorith.Text = test.metaData.Algorithm;
            //ImageBox = test.metaData.PathToImg;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            TestPage testPage = new TestPage(new TestProcess(test.Shakli,test.Keys));
            this.NavigationService.Navigate(testPage);
        }
    }
}
