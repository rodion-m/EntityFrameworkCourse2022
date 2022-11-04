// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Wpf.Ui.Mvvm.Contracts;

namespace SimpleJournal;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public static ISnackbarService Snackbar { get; set; } = null!;
}
