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
using System.Windows.Shapes;
using TaskTracking.Models;

namespace TaskTracking.AdminActions
{
    /// <summary>
    /// Логика взаимодействия для UserCreateWindow.xaml
    /// </summary>
    public partial class UserCreateWindow : Window
    {
        TaskTrackingEntities db = new TaskTrackingEntities();

        Guid idUser;
        public UserCreateWindow()
        {
            InitializeComponent();
            fillComboBox();

            saveButton.Click += saveButton_Click_add;
        }
        public UserCreateWindow(Guid idUser)
        {
            InitializeComponent();
            fillComboBox();

            this.idUser = idUser;

            var user = db.User.Find(idUser);

            nameTextBox.Text = user.Name; 
            loginTextBox.Text = user.Email;
            passwordTextBox.Text = user.Password;
            roleComboBox.SelectedValue = user.Role;

            saveButton.Click += saveButton_Click_edit;
        }

        private void fillComboBox()
        {
            var roles = new List<Role>
            {
                Role.USER,
                Role.ADMIN
            };

            roleComboBox.ItemsSource = EnumViewModel.convertToEnumViewModel(roles);
        }

        private void saveButton_Click_add(object sender, RoutedEventArgs e)
        {

            try
            {
                User item = new User
                {
                    Id = Guid.NewGuid(),
                    Name = nameTextBox.Text,
                    Email = loginTextBox.Text,
                    Password = passwordTextBox.Text,
                    Role = Convert.ToInt32(roleComboBox.SelectedValue),
                    DateCreateUtc = DateTime.Now,
                    IsBan = false
                };

                db.User.Add(item);

                db.SaveChanges();

                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка. Проверьте поля формы");
            }

        }

        private void saveButton_Click_edit(object sender, RoutedEventArgs e)
        {
            try
            {
                User item = db.User.Find(idUser);

                item.Name = nameTextBox.Text;
                item.Email = loginTextBox.Text;
                item.Password = passwordTextBox.Text;
                item.Role = Convert.ToInt32(roleComboBox.SelectedValue);

                db.SaveChanges();

                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка. Проверьте поля формы");
            }

        }

    }
}
