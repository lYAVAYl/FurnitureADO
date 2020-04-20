using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FactoryDAL;


namespace FurnitureADO
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            GenerateNewKapcha();
        }

        
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
           errorMsg.Content = "";
            bool allcorrect = true;

            if (inpLogin.Text.Trim() == "") errorMsg.Content += "Введите Логин\n";
            if (inpPassword.Password == "") errorMsg.Content += "Введите Пароль\n";
            if (inpKapcha.Text == "") errorMsg.Content += "Введите символы с картинки снизу (капча)\n";
            else if (inpKapcha.Text != robotTest.Content.ToString())errorMsg.Content += "Капча введена неверно\n";


            if (inpLogin.Text.Trim() != "" 
                && inpPassword.Password != "" 
                && inpKapcha.Text!=""
                && inpKapcha.Text == robotTest.Content.ToString())
            {
                UsersDAL users = new UsersDAL();
                if (users.LogInUser(inpLogin.Text, inpPassword.Password))
                {
                    // TODO: Load next page

                    var m = new MediaPlayer();
                    m.Open(new Uri(@"D:\FilmspornhubCommunityIntro.mp3"));
                    m.Play();

                    MessageBox.Show("Load next page...");
                }
                else
                {
                    errorMsg.Content += "Логин и/или пароль введены неверно";
                    
                }
            }
            else GenerateNewKapcha();

        }

        private void LoadNewKapcha(object sender, MouseButtonEventArgs e)
        {
            GenerateNewKapcha();
        }

        /// <summary>
        /// Генерация новой капчи
        /// </summary>
        private void GenerateNewKapcha()
        {
            string kapcha = "";
            int code;
            for (int i = 0; i < 4; i++)
            {
                do
                {
                    code = Guid.NewGuid().GetHashCode() % 100;
                    code = code < 0 ? code * (-1) : code;
                } while (code < 48
                        || code > 57 && code < 65
                        || code > 90 && code < 97
                        || code > 122);

                kapcha += Convert.ToChar(code);
            }

            robotTest.Content = kapcha;
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
