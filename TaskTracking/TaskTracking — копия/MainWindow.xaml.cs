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
using TaskTracking.AdminActions;
using TaskTracking.Models;
using TaskTracking.UserActions;

namespace TaskTracking
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TaskTrackingEntities db = new TaskTrackingEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(loginTextBox.Text) ||
              String.IsNullOrEmpty(passwordTextBox.Password))
            {
                MessageBox.Show("Ошибка, одно или оба поле пустые");
                return;
            }

            User user = db.User.Where(x => x.Email == loginTextBox.Text && x.Password == passwordTextBox.Password).SingleOrDefault();

            if (user == null)
            {
                MessageBox.Show("Логин или пароль неверны");
                return;
            }

            if (user.IsBan == true)
            {
                MessageBox.Show("Ваш аккаунт заблокирован. Обратитесь к администратору");
                return;
            }

            switch (Enum.Parse(typeof(Role), user.Role.ToString()))
            {
                case Role.ADMIN:

                    AdminMainWindow window = new AdminMainWindow();

                    Globals.user = new Models.User()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Role = Role.ADMIN
                    };

                    window.ShowDialog();
                    break;

                case Role.USER:

                    UserMainWindow window1 = new UserMainWindow();

                    Globals.user = new Models.User()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Role = Role.USER
                    };

                    window1.ShowDialog();
                    break;

            }
        }
    }
}
