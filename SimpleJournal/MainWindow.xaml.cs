// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Wpf.Ui.Mvvm.Services;

namespace SimpleJournal;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        Wpf.Ui.Appearance.Accent.ApplySystemAccent();
        App.Snackbar = new SnackbarService();
        App.Snackbar.SetSnackbarControl(RootSnackbar);

        // Wpf.Ui.Appearance.Theme.ApplyDarkThemeToWindow(this);
        // Wpf.Ui.Appearance.Background.Apply(this, Wpf.Ui.Appearance.BackgroundType.Mica);
    }
}
