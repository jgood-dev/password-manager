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

namespace PassMgr.Views
{
    public partial class ViewOrUpdate : Window
    {
        public ViewOrUpdate(Entry entry)
        {
            InitializeComponent();
            LoadInfo(entry);
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            updateEntry();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.aliasTextBox.Text))
            {
                MessageBox.Show("There must be an alias", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (String.IsNullOrEmpty(this.urlTextBox.Text))
            {
                MessageBox.Show("There must be a url", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (String.IsNullOrEmpty(this.usernameTextBox.Text))
            {
                MessageBox.Show("There must be a username", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (String.IsNullOrEmpty(this.passwordTextBox.Text))
            {
                MessageBox.Show("There must be a password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.DialogResult = true;
        }
        private void LoadInfo(Entry e)
        {
            aliasTextBox.Text = e.Alias;
            urlTextBox.Text = e.Url;
            usernameTextBox.Text = e.Username;
            passwordTextBox.Text = e.Password;
        }
        private void updateEntry()
        {
            aliasTextBox.IsReadOnly = false;
            urlTextBox.IsReadOnly = false;
            usernameTextBox.IsReadOnly = false;
            passwordTextBox.IsReadOnly = false;

            aliasTextBox.Background = Brushes.White;
            urlTextBox.Background = Brushes.White;
            usernameTextBox.Background = Brushes.White;
            passwordTextBox.Background = Brushes.White;

            saveButton.Visibility = Visibility.Visible;
            updateButton.Visibility = Visibility.Collapsed;
        }
    }
}