using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace PsyTests
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        //для прогресс бар нужно знать коллчиство вопросв для конкретной шкалы
        List<QuestrionPage> pages=new();
        int currentQuestion = 0;
        int maxQuestions;
        TestProcess process;
        QuestrionPage GetNextQuestion()
        {
            currentQuestion++;
            if(currentQuestion==maxQuestions)
            {
                currentQuestion--;
                return null;
            }
            else
            {
                return pages[currentQuestion];
            }
            
        }
        QuestrionPage GetPrevQuestion()
        {
            currentQuestion--;
            if(currentQuestion==0)
            {
                currentQuestion--;
                return null;
            }
            else
            {
                return pages[currentQuestion];
            }
            
        }
        void NextPage()
        {
            QuestrionPage page = GetNextQuestion();
            if (page != null)
            {
                QuestionFrame.Navigate(page);
            }
            else
            {
                FinalizeTest();
            }
        }
        void PrevPage()
        {
            QuestrionPage page = GetPrevQuestion();
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
                this.NavigationService.Navigate(new PageResult(process.shkalas));
            }
            else
            {
                //не на все отвечено
            }
        }
        public TestPage(TestProcess process)
        {
            InitializeComponent();
            this.process = process;
            maxQuestions = process.keys.Count;
            foreach(Shkala shkala in process.shkalas)
            {
                shkala.SetValueToZero();
            }
            foreach(Key key in process.keys)
            {
                QuestrionPage page = new(key);
                pages.Add(page);
                page.Checked += NextPage;
            }
            QuestionFrame.Navigate(pages[0]);
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            NextPage();
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            PrevPage();
        }
    }
}
