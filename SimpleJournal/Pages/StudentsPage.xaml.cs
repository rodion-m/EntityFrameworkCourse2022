using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Wpf.Ui.Common;
using System.Threading.Tasks;
using SimpleJournal.Entities;
using SimpleJournal.ValueObjects;

namespace SimpleJournal;

/// <summary>
/// Interaction logic for StudentsPage.xaml
/// </summary>
public partial class StudentsPage
{
    public ObservableRangeCollection<Student> Students { get; } = new();
    public ObservableRangeCollection<Group> Groups { get; } = new();
    public ObservableRangeCollection<string> Suggestions { get; } = new();

    public Student CurrentStudent { get; set; } = new();
    
    private AppDbContext _db = null!;

    public StudentsPage()
    {
        InitializeComponent();
        contentStackPanel.Visibility = Visibility.Collapsed;
    }
    ~StudentsPage()
    {
        _db.Dispose();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await EnsureDbConnected();
        
        List<Group> groups = await _db.Groups
            //.AsNoTracking() //"EF, забудь про эти записи" (ЛОМАЕТ ДОБАВЛЕНИЕ)
            .ToListAsync();

        List<Student> students = await _db.Students
            .OrderBy(it => it.Name)
            .Include(s => s.Group)
            .Include(s => s.Visits)
            .ToListAsync();

        Groups.ReplaceRange(groups);
        Students.ReplaceRange(students);
       //  Suggestions.ReplaceRange(
       //      students.Take(100).Select(it => it.Name)
       //          .Concat(groups.Select(it => it.Name))
       //          .Distinct()
       // );

        loadingStackPanel.Visibility = Visibility.Collapsed;
        contentStackPanel.Visibility = Visibility.Visible;
    }

    private async Task EnsureDbConnected()
    {
        if (_db is null)
        {
            _db = new AppDbContext();
            await Task.Run(() => _db.Database.CanConnectAsync());
        }
    }

    private void Page_Unloaded(object sender, RoutedEventArgs e)
    {
    }

    private async void Students_CurrentCellChanged(object sender, EventArgs e)
    {
        await TrySaveChangesAsync();
    }

    private void Students_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        if (e.Command == DataGrid.DeleteCommand)
        {
            var dataGrid = (DataGrid)sender;
            var item = (Student) dataGrid.SelectedItem;
            _db.Students.Remove(item);
        }
    }
    
    private async void AddStudent(object sender, RoutedEventArgs e)
    {
        var student = new Student(Guid.NewGuid(), nameTextBox.Text)
        {
            EducationCost = (int) educationCostNumberBox.Value,
            Phone = new Phone(phoneTextBox.Text),
            Group = (Group)groupComboBox.SelectedItem,
            Visits = new List<Visit>()
        };

        Students.Add(student);
        studentsDataGrid.ScrollIntoView(student);
        await _db.Students.AddAsync(student);
        await _db.SaveChangesAsync();

        // var newStudent = CurrentStudent with { Id = Guid.NewGuid()};
        // _db.Entry(CurrentStudent).State = EntityState.Unchanged;
        // await _db.Students.AddAsync(newStudent);
        // Students.Add(newStudent);
        // await _db.SaveChangesAsync();
    }

    private async void SaveChanges_Click(object sender, RoutedEventArgs e)
    {
        await TrySaveChangesAsync();
    }
    private async Task TrySaveChangesAsync()
    {
        var changesEntriesCount = await _db.SaveChangesAsync();
        if (changesEntriesCount > 0)
        {
            await App.Snackbar.ShowAsync("Оповещение", "Изменения сохранены", SymbolRegular.Save24);
        }
    }

    private void StudentsDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        CurrentStudent = studentsDataGrid.SelectedItem as Student;
        if (CurrentStudent == null) return; 
        nameTextBox.Text = CurrentStudent.Name;
        phoneTextBox.Text = CurrentStudent.Phone?.ToString();
        educationCostNumberBox.Text = CurrentStudent.EducationCost.ToString();
        groupComboBox.SelectedItem = CurrentStudent.Group;

        //foreach (var group in groupComboBox.ItemsSource.Cast<Group>())
        //{
        //    if (group.Equals(student.Group))
        //    {
        //        ;
        //    }
        //}
    }

    private async void searchTextBox_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            if (!string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                List<Student> students = await _db.Students
                    .Where(it => 
                        it.Name.Contains(searchTextBox.Text))
                    .OrderBy(it => it.Name)
                    .Include(s => s.Group)
                    .Include(s => s.Visits)
                    .ToListAsync();

                Students.ReplaceRange(students);
            }
            else
            {
                MessageBox.Show("Введите текст для поиска");
            }
        }
    }
}
