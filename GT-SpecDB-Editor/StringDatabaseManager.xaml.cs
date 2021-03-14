﻿using System;
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

using GT_SpecDB_Editor.Core;

namespace GT_SpecDB_Editor
{
    /// <summary>
    /// Interaction logic for StringDatabaseManager.xaml
    /// </summary>
    public partial class StringDatabaseManager : Window
    {
        public StringDatabase Database { get; set; }
        public bool HasSelected { get; set; }
        public (int index, string selectedString) SelectedString { get; set; }

        public StringDatabaseManager(StringDatabase strDb)
        {
            InitializeComponent();
            Database = strDb;
            lb_StringList.DataContext = strDb;
        }

        private bool StringFilter(object item)
        {
            if (string.IsNullOrEmpty(tb_FilterString.Text))
                return true;
            else
                return (item as string).IndexOf(tb_FilterString.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void btn_AddString_Click(object sender, RoutedEventArgs e)
        {
            if (Database.Strings.Contains(tb_NewString.Text))
            {
                MessageBox.Show("This string already exists in the string database. If you wish to select it search and select it.", "String already exists", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Database.Strings.Add(tb_NewString.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lb_StringList.ItemsSource);
            view.Filter = StringFilter;
        }

        private void tb_FilterString_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lb_StringList.ItemsSource).Refresh();
        }

        private void tb_EditString_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lb_StringList.SelectedIndex == -1)
                return;

            var selectedIndex = Database.Strings.IndexOf((string)lb_StringList.SelectedItem);
            Database.Strings[selectedIndex] = tb_EditString.Text;
            lb_StringList.SelectedIndex = selectedIndex;
        }

        private void lb_StringList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lb_StringList.SelectedIndex == -1)
                return;

            var selectedIndex = Database.Strings.IndexOf((string)lb_StringList.SelectedItem);
            var selectedString = (string)lb_StringList.SelectedItem;
            SelectedString = (selectedIndex, selectedString);
            HasSelected = true;
            Close();
        }

        private void btn_SelectEmptyString_Click(object sender, RoutedEventArgs e)
        {
            int index = Database.Strings.IndexOf("");
            string res;
            if (index == -1)
            {
                res = "";
                Database.Strings.Add(res);
                index = Database.Strings.Count - 1;
            }
            else
            {
                res = Database.Strings[index];
            }

            HasSelected = true;
            SelectedString = (index, res);
            Close();
        }

        private void lb_StringList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tb_EditString.IsEnabled = lb_StringList.SelectedIndex != -1;
            if (lb_StringList.SelectedIndex != -1)
            {
                var selectedString = (string)lb_StringList.SelectedItem;
                tb_EditString.Text = selectedString;
            }
            tb_EditString.IsEnabled = lb_StringList.SelectedIndex != -1;
            btn_DeleteString.IsEnabled = lb_StringList.SelectedIndex != -1;
        }

        private void btn_DeleteString_Click(object sender, RoutedEventArgs e)
        {
            if (lb_StringList.SelectedIndex == -1)
                return;

            var selectedIndex = Database.Strings.IndexOf((string)lb_StringList.SelectedItem);
            var selectedString = Database.Strings[selectedIndex];

            if (MessageBox.Show($"Are you sure that you want to delete '{selectedString}'? This *will* break the database by changing the other of all strings that comes after, make sure that you know what you are doing!",
                "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Database.Strings.Remove(selectedString);
            }
        }
    }
}
