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
using System.Windows.Threading;

namespace ismagilov16_17
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();

        public AuthPage()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += Timer_Tick;
        }

        private void SignGhost_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProductPage(null));
            Login.Text = "";
            Password.Text = "";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Login.IsEnabled = true; // Включить TextBox
            timer.Stop(); // Остановить таймер
        }


        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Text;

            if (login == "" || password == "")
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка");
            }
            else
            {
                User user = Ismagilov41Entities.GetContext().User.ToList().Find(p => p.UserLogin == login && p.UserPassword == password);
                if (user != null)
                {
                    Manager.MainFrame.Navigate(new ProductPage(user));
                    Login.Text = "";
                    Password.Text = "";
                }
                else
                {
                    MessageBox.Show("Введены неверные данные!", "Ошибка");
                    Login.IsEnabled = false;
                    Sign.IsEnabled = false;
                    timer.Start(); // Запустить таймер
                }
            }
        }
    }
}
