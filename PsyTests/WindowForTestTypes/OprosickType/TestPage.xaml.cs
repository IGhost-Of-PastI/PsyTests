using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace PsyTests
{
    //enum States
    //{
    //    None,
    //    StartList,
    //    EndList
    //}
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
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
            //foreach (Shkala shkala in process.shkalas)
            //{
            //    shkala.SetValueToZero();
            //}
            foreach (Key key in process.keys)
            {
                QuestrionPage page = new(key);
                pages.Add(page);
                page.Checked += NextPage;
            }
            QuestionFrame.Navigate(pages[0]);
        }
        //для прогресс бар нужно знать коллчиство вопросв для конкретной шкалы
       
        //States states = States.StartList;
        List<QuestrionPage> pages=new();
        int currentQuestion = 0;
        int maxQuestions;
        TestProcess process;
        private QuestrionPage GetNextQuestion()
        {
            if (currentQuestion != maxQuestions - 1)
            {
                currentQuestion++;
                //if (currentQuestion == maxQuestions - 1)
                //{
                //    states = States.EndList;
                //    ButtonNextPage.Content = "Завершить тест";
                //}
                CurrentPage.Content = currentQuestion+1;
                return pages[currentQuestion];
            }
            else
            {
                FinalizeTest();
                return null;
            }
        }
        //private QuestrionPage GetPrevQuestion()
        //{
        //    if (currentQuestion != 0)
        //    {
        //        currentQuestion--;
        //        if (currentQuestion == 0)
        //        {
        //            states = States.StartList;
        //            ButtonNextPage.IsEnabled = false;
        //            ///вынести отключения и включения кнопок в другую функцию
        //        }
        //        return pages[currentQuestion];
        //    }

        //}
        void NextPage()
        {
            QuestrionPage page = GetNextQuestion();
            if (page != null)
            {
                QuestionFrame.Navigate(page);
            }
            //else
            //{
            //    //FinalizeTest();
            //}
        }
        //void PrevPage()
        //{
        //    QuestrionPage page = GetPrevQuestion();
        //    if (page != null)
        //    {
        //        QuestionFrame.Navigate(page);
        //    }
        //}
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
            else
            {
                //не на все отвечено
            }
        }
       

        //private void NextPage_Click(object sender, RoutedEventArgs e)
        //{
        //    NextPage();
        //}

        //private void PrevPage_Click(object sender, RoutedEventArgs e)
        //{
        //    PrevPage();
        //}
    }
}
