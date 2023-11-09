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
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            TestPage testPage = new TestPage(new TestProcess(test.Shakli));
            this.NavigationService.Navigate(testPage);
        }
    }
}
