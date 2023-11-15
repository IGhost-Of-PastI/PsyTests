using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace PsyTests
{
    public partial class TestPage : Page
    {
        public TestPage(TestProcess process)
        {
            InitializeComponent();
            this.process = process;
            maxQuestions = process.keys.Count;
            process.SetAllValuesToZero();
            MaxQuestions.Content += maxQuestions.ToString();
            CurrentPage.Content = currentQuestion+1;
            foreach (Key key in process.keys)
            {
                QuestrionPage page = new(key);
                pages.Add(page);
                page.Checked += NextPage;
            }
            QuestionFrame.Navigate(pages[0]);
        }
       
        List<QuestrionPage> pages=new();
        int currentQuestion = 0;
        int maxQuestions;
        TestProcess process;
        private QuestrionPage GetNextQuestion()
        {
            if (currentQuestion != maxQuestions - 1)
            {
                currentQuestion++;
                CurrentPage.Content = currentQuestion+1;
                return pages[currentQuestion];
            }
            else
            {
                FinalizeTest();
                return null;
            }
        }
      
        void NextPage()
        {
            QuestrionPage page = GetNextQuestion();
            if (page != null)
            {
                QuestionFrame.Navigate(page);
            }
        }
        void FinalizeTest()
        {
            bool ansvered=true;
            foreach(QuestrionPage page in pages)
            {
                if(page.IsAnyAnswer()!=true)
                {
                    ansvered=false;
                    break;
                }
            }
            if(ansvered==true)
            {
                foreach(QuestrionPage page in pages)
                {
                    page.CheckAnsver();
                }
                this.NavigationService.Navigate(new PageResult(process));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
