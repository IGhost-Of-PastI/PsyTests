using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PsyTests
{
    /// <summary>
    /// Логика взаимодействия для PageResult.xaml
    /// </summary>
    public partial class PageResult : Page
    {
        public PageResult(TestProcess testProcess)
        {
            InitializeComponent();
            foreach (Shkala shkala in testProcess.shkalas)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                Label label = new Label();
                label.Content = shkala.Name;
                if(shkala.value<0)
                {
                    shkala.SetValueToZero();
                }
                label.Content += $"{shkala.GetValue()}/{testProcess.GetNumberOfLinkedQuestions(shkala)*2}";
                //stackPanel.Children.Add(label);
                //ProgressBar bar = new ProgressBar();
                //bar.Maximum = testProcess.GetNumberOfLinkedQuestions(shkala)*2;
                //bar.Minimum = 0;
                //bar.Value = shkala.GetValue();
                //bar.Width = 100;
                //bar.Height = 20;
                stackPanel.Children.Add(label);
                //stackPanel.Children.Add(bar);
                ResultsPanel.Children.Add(stackPanel);
            }
           
        }
    }
}
