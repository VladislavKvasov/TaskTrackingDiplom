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

namespace TaskTracking.AdminActions
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {

        TaskTrackingEntities db = new TaskTrackingEntities();
        public AdminMainWindow()
        {
            InitializeComponent();
            updateWindow();
        }

        private void updateWindow()
        {
            listView.ItemsSource = null;
            listView.ItemsSource = db.User.ToList();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selected = (listView.SelectedItem as User);
            UserCreateWindow window = new UserCreateWindow(selected.Id);
            window.ShowDialog();

            updateWindow();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                var selected = (listView.SelectedItem as User);

                db.User.Remove(selected);

                db.SaveChanges();

                MessageBox.Show("Успешно");

                updateWindow();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка. Невозможно удалить пользователя.");
            }

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = (listView.SelectedItem as User);

                db.User.Find(selected.Id).IsBan = !db.User.Find(selected.Id).IsBan;

                db.SaveChanges();

                updateWindow();
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка. Невозможно выполнить действие.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserCreateWindow window = new UserCreateWindow();
            window.ShowDialog();

            updateWindow();
        }
    }
}
