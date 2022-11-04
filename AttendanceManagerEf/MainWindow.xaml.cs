using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;

namespace AttendanceManagerEf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _db;
        private List<Student> _students = new();

        public MainWindow()
        {
            InitializeComponent();
            _db = new AppDbContext();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var result = await db.Database.ExecuteSqlRawAsync(@"
                   create table Students
                   (
                    Id int primary key,
                    Name text
                   )"
                );
                MessageBox.Show("Все готово");
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _db.SaveChangesAsync();
            _students = await _db.Students.ToListAsync();
            dataGridStudents.ItemsSource = _students;
        }

        private async void buttonAddStudent_Click(object sender, RoutedEventArgs e)
        {
            var student = new Student(Guid.NewGuid(), textBoxStudentName.Text);
            await _db.AddAsync(student);
            await _db.SaveChangesAsync();
            _students.Add(student);
            dataGridStudents.Items.Refresh();
        }

        private async void dataGridStudents_CurrentCellChanged(object sender, EventArgs e)
        {
            var changesEntriesCount = await _db.SaveChangesAsync();
            if (changesEntriesCount > 0)
            {
                MessageBox.Show("Изменения сохранены");
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            _db.Dispose();
        }

        private void dataGridStudents_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)
            {
                var dataGrid = (DataGrid)sender;
                var item = dataGrid.SelectedItem as Student;
                _db.Students.Remove(item);
            }
        }

        private async void dataGridStudents_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var newItem = new Student(Guid.NewGuid(), "Без имени");
            e.NewItem = newItem;
            await _db.Students.AddAsync(newItem);
        }
    }
}
