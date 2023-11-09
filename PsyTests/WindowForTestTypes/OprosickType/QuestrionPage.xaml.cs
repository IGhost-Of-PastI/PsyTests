using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PsyTests
{
    /// <summary>
    /// Логика взаимодействия для QuestrionPage.xaml
    /// </summary>
    public partial class QuestrionPage : Page
    {
        Key key;
        bool ansvered=false;
        public event Action Checked;

        public bool IsAnyAnswer()
        {
            return ansvered;
        }
        public QuestrionPage(Key key)
        {
            InitializeComponent();
            this.key = key;
            Question.Text = key.Name;
        }
        int Deside(bool sign, int modifier)
        {
            if(key.Value^sign)
            {
                return (-1 * modifier);
            }
            else
            {
                return modifier;
            }
        }
        public void CheckAnsver()
        {
            var radiobuttons = LogicalTreeHelper.GetChildren(RadiobuttonsPanel).OfType<RadioButton>().ToList();
            int temp=0;
            for (int i=0;i<radiobuttons.Count;i++)
            {
                if(radiobuttons[i].IsChecked==true)
                {
                    temp = i; break;
                }
                else
                {

                }
            }
            switch(temp)
            {
                case 0: {
                        key.shkala.ChangeValueOfShkala(Deside(false,2));
                        break;}
                case 1: {
                        key.shkala.ChangeValueOfShkala(Deside(false, 1));
                        break; }
                case 2: {
                        key.shkala.ChangeValueOfShkala(Deside(false, 0));
                        break; }
                case 3: {
                        key.shkala.ChangeValueOfShkala(Deside(true, 1));
                        break; }
                case 4: {
                        key.shkala.ChangeValueOfShkala(Deside(true, 1));
                        break; }
            }
           
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(ansvered!=true)
            {
                ansvered = true;
                Checked?.Invoke();
            }
        }
    }
}
