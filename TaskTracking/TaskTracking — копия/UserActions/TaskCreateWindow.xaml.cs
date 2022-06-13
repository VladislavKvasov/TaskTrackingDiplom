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
using System.IO;

namespace TaskTracking.UserActions
{
    /// <summary>
    /// Логика взаимодействия для TaskCreateWindow.xaml
    /// </summary>
    public partial class TaskCreateWindow : Window
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        TaskTrackingEntities db = new TaskTrackingEntities();
        List<File> files = new List<File>();
        Guid boardId;
        Guid taskId;

        public TaskCreateWindow(Guid boardId)
        {
            InitializeComponent();
            this.boardId = boardId;

            fillComboBox();

            saveButton.Click += saveButton_Click;
        }

        public TaskCreateWindow(Guid boardId, Guid id)
        {
            InitializeComponent();
            this.boardId = boardId;
            this.taskId = id;

            fillComboBox();

            statusComboBox.Visibility = Visibility.Visible;
            statusTextBlock.Visibility = Visibility.Visible;

            var task = db.Task.Find(taskId);

            nameTextBox.Text = task.Name;
            descTextBox.Text = task.Description;

            priorityComboBox.SelectedValue = task.Priority;
            statusComboBox.SelectedValue = task.Status;
            executorComboBox.SelectedValue = task.ExecutorId;
            boardComboBox.SelectedValue = task.BoardId;

            saveButton.Click += saveButton_Click_edit;
        }

        private void fillComboBox()
        {
            var priorities = new List<Priority>
            {
                Priority.LOW,
                Priority.NORMAL,
                Priority.HIGH
            };

            priorityComboBox.ItemsSource = EnumViewModel.convertToEnumViewModel(priorities);

            var statuses = new List<Status>
            {
                Status.BACKLOG,
                Status.IN_WORK,
                Status.READY,
                Status.CANCEL
            };

            statusComboBox.ItemsSource = EnumViewModel.convertToEnumViewModel(statuses);

            executorComboBox.ItemsSource = db.User.Where(x => x.Role == (int)Role.USER).ToList();
            boardComboBox.ItemsSource = db.Board.ToList();

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = new Task();

                var selectedBoard = db.Board.Find(boardId);

                //only for create
                item.Id = Guid.NewGuid();
                item.DateCreateUtc = DateTime.Now.ToUniversalTime();
                item.AuthorId = Globals.user.Id;
                item.ShortName = db.Board.Find(selectedBoard.Id).ShortName + "-" + (db.Board.Find(boardId).Task.Count() + 1);
                item.BoardId = boardId;
                item.Status = (int)Status.BACKLOG;

                item.Priority = Convert.ToInt32((priorityComboBox.SelectedItem as EnumViewModel).Value);
                item.Name = nameTextBox.Text;
                item.Description = descTextBox.Text;
                item.ExecutorId = (executorComboBox.SelectedItem as User).Id;

                db.Task.Add(item);

                //files
                if (files.Count > 0)
                {
                    db.File.AddRange(
                        files.Select(x =>
                           new File()
                           {
                               Id = Guid.NewGuid(),
                               Type = x.Type,
                               File1 = x.File1,
                               AuthorId = Globals.user.Id,
                               TaskId = item.Id,
                               Name = x.Name
                           }
                        )
                    );
                }


                db.SaveChanges();

                Close();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Произошла ошибка");
            }
        }

        private void saveButton_Click_edit(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = db.Task.Find(taskId);

                var newStatus = Convert.ToInt32((statusComboBox.SelectedItem as EnumViewModel).Value);

                item.Priority = Convert.ToInt32((priorityComboBox.SelectedItem as EnumViewModel).Value);
                item.Name = nameTextBox.Text;
                item.Description = descTextBox.Text;
                item.ExecutorId = (executorComboBox.SelectedItem as User).Id;

                if (item.Status != newStatus)
                {
                    if (newStatus == (int)Status.IN_WORK)
                        item.DateStartWorkUtc = DateTime.Now.ToUniversalTime();

                    if (newStatus == (int)Status.READY)
                        item.DateEndWorkUtc = DateTime.Now.ToUniversalTime();

                    if (newStatus == (int)Status.BACKLOG)
                    {
                        item.DateCreateUtc = DateTime.Now.ToUniversalTime();
                        item.DateStartWorkUtc = null;
                        item.DateEndWorkUtc = null;
                    }
                }

                item.Status = newStatus;

                //files
                if (files.Count > 0)
                {
                    db.File.AddRange(
                        files.Select(x =>
                           new File()
                           {
                               Id = Guid.NewGuid(),
                               Type = x.Type,
                               File1 = x.File1,
                               AuthorId = Globals.user.Id,
                               TaskId = item.Id,
                               Name = x.Name
                           }
                        )
                    );
                }


                db.SaveChanges();

                Close();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Произошла ошибка");
            }
        }


        private void addFilesButton_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int count = 0;
                foreach (var item in openFileDialog.FileNames)
                {
                    files.Add(new File()
                    {
                        Type = item.Substring(item.LastIndexOf(".")),
                        Name = item.Substring(item.LastIndexOf("\\")+ 1),
                        File1 = System.IO.File.ReadAllBytes(item)
                    }
                    );
                    count++;
                }
                addFilesButton.Content = "Файлов выбрано: " + count;
            }
        }
    }
}
