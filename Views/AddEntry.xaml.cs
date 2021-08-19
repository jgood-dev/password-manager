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
using PassMgr.Services;

namespace PassMgr
{
    /// <summary>
    /// Interaction logic for AddEntry.xaml
    /// </summary>
    public partial class AddEntry : Window
    {
        public AddEntry()
        {
            InitializeComponent();
            txblkUser.Text = SessionContext.Username;
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
    }
}
