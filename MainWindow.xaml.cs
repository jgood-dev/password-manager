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

//C: \Users\goodw\OneDrive\Desktop\MSSA\Labs\C#\Mod03\20483-Programming-in-C-Sharp\Allfiles\Mod04\Labfiles\Starter\Exercise 4
// ^^^ reference project ^^^

namespace PassMgr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Entry> entries = new List<Entry>();

        public MainWindow()
        {
            InitializeComponent();
            LoadEntriesList();
        }

        private void LoadEntriesList()
        {
            entries = null;
            entries = SqliteDataAccess.LoadEntries();

            entriesListBox.ItemsSource = null;
            entriesListBox.ItemsSource = entries;
        }

        private void viewButton_Click(object sender, RoutedEventArgs e)
        {
            Entry entry = this.entriesListBox.SelectedItem as Entry;
            updateEntry();

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            //delete method here
        }

        private void entriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idx = entriesListBox.SelectedIndex;

            if (idx != -1)
            {
                viewButton.IsEnabled = true;
            }
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            AddEntry();
        }

        private void AddEntry()
        {
            AddEntry aE = new AddEntry();

            if (aE.ShowDialog().Value)
            {
                Entry newEntry = new Entry();
                newEntry.Alias = aE.aliasTextBox.Text;
                newEntry.Url = aE.urlTextBox.Text;
                newEntry.Username = aE.usernameTextBox.Text;
                newEntry.Password = aE.passwordTextBox.Text;

                this.entries.Add(newEntry);

                SqliteDataAccess.SaveEntry(newEntry);

                LoadEntriesList();
            }


        }

        private void updateEntry()
        {
            throw new NotImplementedException();
        }
    }
}
