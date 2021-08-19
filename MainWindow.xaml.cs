using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Drawing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.ObjectModel;
using PassMgr.Views;
using PassMgr.Services;

// C:\Users\goodw\OneDrive\Desktop\MSSA\Labs\C#\Mod06\20483-Programming-in-C-Sharp\Allfiles\Mod06\Labfiles\Starter\Exercise 2
// ^^^ current reference project ^^^

namespace PassMgr
{
    public partial class MainWindow : Window
    {
        List<Entry> entries = new();

        public MainWindow()
        {
            InitializeComponent();
            LoadEntriesList();
        }

        private void LoadEntriesList()
        {
            entries = null;
            entries = SqliteDataAccess.LoadEntries();
            entries.Sort(delegate (Entry first, Entry second)
            {
                return first.Alias.CompareTo(second.Alias);
            });

            entriesListBox.ItemsSource = null;
            entriesListBox.ItemsSource = entries;
        }
        private void viewButton_Click(object sender, RoutedEventArgs e)
        {
            ViewEntry();
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteEntry();
        }
        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            AddEntry();
        }
        private void entriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idx = (sender as ListBox).SelectedIndex;

            if (idx != -1)
            {
                viewButton.Visibility = Visibility.Visible;
            }
        }

        private void AddEntry()
        {
            //this.Visibility = Visibility.Collapsed;
            AddEntry aE = new();

            if (aE.ShowDialog().Value)
            {
                Entry newEntry = new Entry();
                newEntry.Alias = aE.aliasTextBox.Text;
                newEntry.Url = aE.urlTextBox.Text;
                newEntry.Username = aE.usernameTextBox.Text;
                newEntry.Password = aE.passwordTextBox.Text;

                this.entries.Add(newEntry);

                SqliteDataAccess.SaveEntry(newEntry);
                viewButton.Visibility = Visibility.Collapsed;
                //this.Visibility = Visibility.Visible;
                LoadEntriesList();
            }

            this.Visibility = Visibility.Visible;
        }
        private void DeleteEntry()
        {
            Entry entry = this.entriesListBox.SelectedItem as Entry;
            SqliteDataAccess.DeleteEntry(entry);
            viewButton.Visibility = Visibility.Collapsed;

            LoadEntriesList();
        }
        private void ViewEntry()
        {
            int entriesIdx = this.entriesListBox.SelectedIndex;

            Entry e = entries[entriesIdx];
            ViewOrUpdate viewEntry = new(e);

            if (viewEntry.ShowDialog().Value)
            {
                Entry newEntry = new Entry();
                newEntry.Alias = viewEntry.aliasTextBox.Text;
                newEntry.Url = viewEntry.urlTextBox.Text;
                newEntry.Username = viewEntry.usernameTextBox.Text;
                newEntry.Password = viewEntry.passwordTextBox.Text;

                this.entries[entriesIdx] = newEntry;

                SqliteDataAccess.DeleteEntry(e);
                SqliteDataAccess.SaveEntry(newEntry);

                LoadEntriesList();
            }
            viewButton.Visibility = Visibility.Collapsed;
        }

        private void logOffButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
