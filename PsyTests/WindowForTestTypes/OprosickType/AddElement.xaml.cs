using System;
using System.Windows;

namespace PsyTests.WindowForTestTypes.OprosickType
{
    /// <summary>
    /// Логика взаимодействия для AddElement.xaml
    /// </summary>
    public partial class AddElement : Window
    {
        public event EventHandler<string> ChildWindowClosed;

        public AddElement()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChildWindowClosed?.Invoke(this, StringName.Text);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
