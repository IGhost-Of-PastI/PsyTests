using System.Windows;

namespace PsyTests
{
    /// <summary>
    /// Логика взаимодействия для PreTestWindow.xaml
    /// </summary>
    public partial class PreTestWindow : Window
    {
        public PreTestWindow(OprosnicTest oprosnicTest)
        {
            InitializeComponent();
            PreTest preTest = new PreTest(oprosnicTest);
            MainPage.Navigate(preTest);
           
        }
    }
}
