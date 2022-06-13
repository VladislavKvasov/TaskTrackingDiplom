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

namespace TaskTracking.UserActions
{
    /// <summary>
    /// Логика взаимодействия для UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        TaskTrackingEntities db = new TaskTrackingEntities();

        public static Guid ExecutorTasksId = Guid.NewGuid();

        public UserMainWindow()
        {
            InitializeComponent();
            setBoards();
        }

        private void setBoards()
        {
            listBox.ItemsSource = null;

            List<Board> list = new List<Board>();

            list.Add(
                new Board()
                {
                    Id = ExecutorTasksId,
                    Name = "Все ваши задачи на общей доске"
                }
                );

            list.AddRange(db.Board.ToList());

            listBox.ItemsSource = list;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateBoard();
        }

        private void StackPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TaskDetailsWindow window = new TaskDetailsWindow(Guid.Parse((sender as Grid).Tag.ToString()));
            window.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TaskCreateWindow window = new TaskCreateWindow((listBox.SelectedItem as Board).Id);
            window.ShowDialog();
        }

        private void updateBoard()
        {
            string search = searchTextBox.Text;

            var selectedBoard = listBox.SelectedItem as Board;

            if (selectedBoard == null) return;

            notInWorkListBox.ItemsSource = null;
            inWorkListBox.ItemsSource = null;
            readyListBox.ItemsSource = null;
            cancelListBox.ItemsSource = null;

            if (selectedBoard.Id == ExecutorTasksId)
            {

                notInWorkListBox.ItemsSource = db.Task.Where(
                    x => x.ExecutorId == Globals.user.Id
                    && x.Status == (int)Status.BACKLOG
                    && x.Name.Contains(search)
                    )
                    .OrderBy(x => x.Priority)
                    .ToList();

                inWorkListBox.ItemsSource = db.Task.Where(
                    x => x.ExecutorId == Globals.user.Id
                    && x.Status == (int)Status.IN_WORK
                    && x.Name.Contains(search)
                    )
                    .OrderBy(x => x.Priority)
                    .ToList();

                readyListBox.ItemsSource = db.Task.Where(
                    x => x.ExecutorId == Globals.user.Id
                    && x.Status == (int)Status.READY
                    && x.Name.Contains(search)
                    )
                    .OrderBy(x => x.Priority)
                    .ToList();

                cancelListBox.ItemsSource = db.Task.Where(
                    x => x.ExecutorId == Globals.user.Id
                    && x.Status == (int)Status.CANCEL
                    && x.Name.Contains(search)
                    )
                    .OrderBy(x => x.Priority)
                    .ToList();

                createTaskButton.IsEnabled = false;
            }
            else
            {
               
                    notInWorkListBox.ItemsSource = db.Task.Where(
                        x => x.BoardId == selectedBoard.Id
                        && x.Status == (int)Status.BACKLOG
                        && x.Name.Contains(search)
                        )
                        .OrderBy(x => x.Priority)
                        .ToList();

                    inWorkListBox.ItemsSource = db.Task.Where(
                        x => x.BoardId == selectedBoard.Id
                        && x.Status == (int)Status.IN_WORK
                        && x.Name.Contains(search)
                        )
                        .OrderBy(x => x.Priority)
                        .ToList();

                    readyListBox.ItemsSource = db.Task.Where(
                        x => x.BoardId == selectedBoard.Id
                        && x.Status == (int)Status.READY
                        && x.Name.Contains(search)
                        )
                        .OrderBy(x => x.Priority)
                        .ToList();

                    cancelListBox.ItemsSource = db.Task.Where(
                        x => x.BoardId == selectedBoard.Id
                        && x.Status == (int)Status.CANCEL
                        && x.Name.Contains(search)
                        )
                        .OrderBy(x => x.Priority)
                        .ToList();
               

                createTaskButton.IsEnabled = true;
            }
        }

        private void TextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            updateBoard();
        }
    }
}