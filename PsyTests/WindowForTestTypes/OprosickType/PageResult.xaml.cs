using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;

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
                //StackPanel stackPanel = new StackPanel();
                //stackPanel.Orientation = Orientation.Horizontal;
                Label label = new Label();
                label.Content = shkala.Name;
                if(shkala.value<0)
                {
                    shkala.SetValueToZero();
                }
                //label.Content += $"{shkala.GetValue()}/{testProcess.GetNumberOfLinkedQuestions(shkala)*2}";
                //stackPanel.Children.Add(label);
                ProgressBar progressBar = new ProgressBar();
                progressBar.Maximum = testProcess.GetNumberOfLinkedQuestions(shkala) * 2;
                progressBar.Minimum = 0;
                progressBar.Value = shkala.GetValue();
                progressBar.Width = 200;
                progressBar.Height = 30;
                progressBar.Foreground = new SolidColorBrush(Colors.LightBlue);
                //stackPanel.Children.Add(label);

                progressBar.Name = "progressBar";

                // Создание TextBlock
                TextBlock textBlock = new TextBlock();
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Background = Brushes.Transparent;

                // Привязка данных
                Binding valueBinding = new Binding("Value");
                valueBinding.Source = progressBar;
                valueBinding.StringFormat = "{0:0.}";
                Binding maximumBinding = new Binding("Maximum");
                maximumBinding.Source = progressBar;

                // Добавление текста в TextBlock
                textBlock.Inlines.Add(new Run() { Text = "" });
                textBlock.Inlines.Add(new Run() { Text = " / " });
                textBlock.Inlines.Add(new Run() { Text = "" });

                // Привязка текста к ProgressBar
                BindingOperations.SetBinding(textBlock.Inlines.ElementAt(0), Run.TextProperty, valueBinding);
                BindingOperations.SetBinding(textBlock.Inlines.ElementAt(2), Run.TextProperty, maximumBinding);

                // Создание Grid и добавление ProgressBar и TextBlock
                Grid grid = new Grid();
                grid.HorizontalAlignment = HorizontalAlignment.Stretch;
                grid.Height = 40;
                grid.Children.Add(label);
                grid.Children.Add(progressBar);
                grid.Children.Add(textBlock);

                grid.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });

                Grid.SetColumn(label, 0);
                Grid.SetColumn(progressBar, 1);
                Grid.SetColumn(textBlock, 1);

                //stackPanel.Children.Add(grid);
                ResultsPanel.Children.Add(grid);
            }
           
        }
    }
}
