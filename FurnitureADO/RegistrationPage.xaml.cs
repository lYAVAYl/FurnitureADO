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
using Microsoft.Win32;

namespace FurnitureADO
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            UsersDAL usersDAL = new UsersDAL();
            string errorMsg = "";


            if (inpLogin.Text == "") errorMsg += "Логин не введён!\n";
            else if (inpLogin.Text.Length > 30) errorMsg += "Логин слишком длинный!\n";
            else if (usersDAL.IsUserExists(inpLogin.Text)) errorMsg += "Пользователь с таким логином уже существует. Придумайте новый Логин!\n";
            if (inpPassword.Password == "") errorMsg += "Пароль не введён. Придумайте пароль!\n";
            else if (!PasswordLib.CheckPassword(inpPassword.Password)) errorMsg += "Пароль не соответсвует правилам:\n" +
                                                                                   "--> Длина пароля должна быть 6-18 символов \n" +
                                                                                   "--> Минимум 1 специальный символ: *&{}|+\n" +
                                                                                   "--> Минимум 1 цифра\n" +
                                                                                   "--> Минимум 1 заглавная буква\n" +
                                                                                   "--> Нет 3 подряд идущих одинаковых символов\n";
            else if (inpPassword.Password != inpPassword2.Password) errorMsg = "Пароли не совпадают\n";
            

            if (errorMsg=="")
            {
                // TODO: Добавить нового заказчика в БД
                usersDAL.AddUser(inpLogin.Text, inpPassword.Password, "заказчик", inpLastName.Text, inpName.Text, inpMiddleName.Text, "");
                NavigationService.Navigate(new LoginPage());
            }
            else
            {
                MessageBox.Show(errorMsg);
            }


        }

        /// <summary>
        /// Загрузка фото
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Images |*.jpg;*.png|All files (*.*)|(*.*)";
            bool? result = dialog.ShowDialog();
            //System.Window.Forms.DialogResult.OK;
            if (result==true)
            {
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(dialog.FileName, UriKind.Absolute));
                
                userPhoto.Source = brush.ImageSource;

            }
                
        }

        /// <summary>
        /// Можно вводить только буквы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnlyLetters(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
