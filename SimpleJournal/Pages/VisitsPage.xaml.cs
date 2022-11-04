using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System;
using System.ComponentModel;
using System.Linq;
using Wpf.Ui.Common;
using System.Threading.Tasks;
using SimpleJournal.Entities;

namespace SimpleJournal;

/// <summary>
/// Interaction logic for VisitsPage.xaml
/// </summary>
public partial class VisitsPage
{
    public ObservableRangeCollection<Visit> Visits { get; set; } = new();
    private IReadOnlyList<Student> _students = new List<Student>();
    private AppDbContext _db = null!;
    private bool _isUnloaded;

    public VisitsPage()
    {
        InitializeComponent();
    }
    
    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        _db = new AppDbContext();
        var items = await Task.Run(() => _db.Visits.ToListAsync());
        Visits.ReplaceRange(items);

        _students = await _db.Students.OrderBy(it => it.Name).AsNoTracking().ToListAsync();

        progressRing.Visibility = Visibility.Collapsed;
        studentsDataGrid.Visibility = Visibility.Visible;
        _isUnloaded = false;
    }

    private void Page_Unloaded(object sender, RoutedEventArgs e)
    {
        _db.Dispose();
        _isUnloaded = true;
    }

    private async void Visits_CurrentCellChanged(object sender, EventArgs e)
    {
        if (_isUnloaded) return;
        var changesEntriesCount = await _db.SaveChangesAsync();
        if (changesEntriesCount > 0)
        {
            await App.Snackbar.ShowAsync("Оповещение", "Изменения сохранены", SymbolRegular.Save24);
        }
    }

    private void Visits_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        if (e.Command == DataGrid.DeleteCommand)
        {
            var dataGrid = (DataGrid)sender;
            var item = (Student) dataGrid.SelectedItem;
            _db.Students.Remove(item);
        }
    }

    private async void AddVisit(object sender, RoutedEventArgs e)
    {
        //var visit = new Visit(Guid.NewGuid(), V);
        //await _db.Visits.AddAsync(visit);
        //Visits.Add(visit);
        //await _db.SaveChangesAsync();
    }
}
