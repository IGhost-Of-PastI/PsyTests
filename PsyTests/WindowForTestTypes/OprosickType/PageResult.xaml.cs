using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PsyTests
{
    /// <summary>
    /// Логика взаимодействия для PageResult.xaml
    /// </summary>
    public partial class PageResult : Page
    {
        public PageResult(ObservableCollection<Shkala> shkalas)
        {
            InitializeComponent();
            foreach (Shkala shkala in shkalas)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                Label label = new Label();
                label.Content = shkala.Name;
                stackPanel.Children.Add(label);
                ProgressBar bar = new ProgressBar();
                bar.Maximum = shkala.GetNumberOfLinkedQuestions();
                bar.Minimum = 0;
                bar.Value = shkala.GetValue();
                stackPanel.Children.Add(label);
                ResultsPanel.Children.Add(stackPanel);
            }
           
        }
    }
}
