using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskTracking.Models;

namespace TaskTracking.UserActions
{
    /// <summary>
    /// Логика взаимодействия для TaskDetailsWindow.xaml
    /// </summary>
    public partial class TaskDetailsWindow : Window
    {
        TaskTrackingEntities db = new TaskTrackingEntities();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        Task task;

        public TaskDetailsWindow(Guid id)
        {
            InitializeComponent();

            task = db.Task.Find(id);

            updateWindow();

            commentItemsControl.ItemsSource = task.Comment.OrderBy(x => x.DateCreateUtc).ToList();
        }

        private void updateWindow()
        {

            db = new TaskTrackingEntities();


            task = db.Task.Find(task.Id);

            nameTextBox.Text = task.Name;
            descTextBox.Text = task.Description;
            executorTextBox.Text = task.User1.Name;
            authorTextBox.Text = task.User.Name;
            dateCreateTextBox.Text = task.DateCreateUtc.ToLocalTime().ToString("dd.MM.yyyy в HH:mm");
            dateStartTextBox.Text = task.DateStartWorkUtc == null ? "Не начата" : task.DateStartWorkUtc.Value.ToLocalTime().ToString("dd.MM.yyyy в HH:mm");
            dateEndTextBox.Text = task.DateEndWorkUtc == null ? "Не закончена" : task.DateEndWorkUtc.Value.ToLocalTime().ToString("dd.MM.yyyy в HH:mm");


            var status = "";

            switch (task.Status)
            {
                case (int)Status.BACKLOG:
                    status = "Беклог";
                    break;
                case (int)Status.IN_WORK:
                    status = "В работе";
                    break;
                case (int)Status.READY:
                    status = "Готова";
                    break;
                case (int)Status.CANCEL:
                    status = "Отменена";
                    break;
            }

            statusTextBox.Text = status;

            switch (task.Priority)
            {
                case (int)Priority.LOW:
                    priorityTextBox.Text = "Низкий";
                    break;
                case (int)Priority.NORMAL:
                    priorityTextBox.Text = "Нормальный";
                    break;
                case (int)Priority.HIGH:
                    priorityTextBox.Text = "Высокий";
                    break;
            }

            if (task.File.Count == 0)
                filesListView.Visibility = Visibility.Collapsed;
            filesListView.ItemsSource = task.File.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TaskCreateWindow window = new TaskCreateWindow(task.BoardId, task.Id);
            window.ShowDialog();

            updateWindow();
        }

        //Скачивание
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var file = task.File.FirstOrDefault(x => x.Id == Guid.Parse((sender as System.Windows.Controls.Button).Tag.ToString()));

            saveFileDialog.FileName = file.Name;
            saveFileDialog.DefaultExt = file.Type;

            if (file != null)
            {
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(saveFileDialog.FileName, file.File1);
                }
            }
        }

        //Коммент
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                db.Comment.Add(
                    new Comment()
                    {
                        Id = Guid.NewGuid(),
                        Text = commentTextBox.Text,
                        TaskId = task.Id,
                        AuthorId = Globals.user.Id,
                        DateCreateUtc = DateTime.Now,
                    }
                );

                commentTextBox.Text = "";

                db.SaveChanges();

            }
            catch
            {
                System.Windows.MessageBox.Show("Ошибка при создании комментария");
            }

            db = new TaskTrackingEntities();

            task = db.Task.Find(task.Id);

            commentItemsControl.ItemsSource = task.Comment.OrderBy(x => x.DateCreateUtc).ToList();
        }
    }
}