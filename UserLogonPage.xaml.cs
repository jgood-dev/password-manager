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

namespace PassMgr.Views
{
    public partial class UserLogonPage : Window
    {
        List<User> users = new();

        public UserLogonPage()
        {
            InitializeComponent();
            SessionContext.Username = null;
            GetUsers();
        }

        #region Data Access
        private void GetUsers()
        {
            users = null;
            users = SqliteDataAccess.LoadUsers();
        }
        #endregion

        #region Logon Validation
        private void logonButton_Click(object sender, RoutedEventArgs e)
        {
            ValidateLogon();
        }

        private void ValidateLogon()
        {
            if (String.IsNullOrEmpty(this.usernameTextBox.Text))
            {
                MessageBox.Show("You must enter a username", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (String.IsNullOrEmpty(this.passwordTextBox.Text))
            {
                MessageBox.Show("You must enter a password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (User user in users)
            {
                if (user.username == this.usernameTextBox.Text &&
                    user.password == this.passwordTextBox.Text)
                {
                    SessionContext.Username = user.username;

                    this.Visibility = Visibility.Collapsed;
                    new MainWindow().ShowDialog();
                    

                    // when user logs off, logon page opens and info is cleared
                    this.usernameTextBox.Clear();
                    this.passwordTextBox.Clear();
                    this.Visibility = Visibility.Visible;
                    return;
                }
            }
            MessageBox.Show("Invalid username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

        private void newUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.usernameTextBox.Text))
            {
                MessageBox.Show("You must enter a username", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (String.IsNullOrEmpty(this.passwordTextBox.Text))
            {
                MessageBox.Show("You must enter a password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<string> usernames = new();
            users.ForEach(s => usernames.Add(s.username));

            User newUser = new(this.usernameTextBox.Text, this.passwordTextBox.Text);

            if (usernames.Contains(newUser.username))
            {
                MessageBox.Show("This username is already taken", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SessionContext.Username = newUser.username;
                SqliteDataAccess.NewUser(newUser);
                MessageBox.Show("New user created successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow main = new();
                this.Visibility = Visibility.Collapsed;
                main.ShowDialog();

                // When user logs off from new user account, logon page opens and info is cleared
                this.usernameTextBox.Clear();
                this.passwordTextBox.Clear();
                this.Visibility = Visibility.Visible;
            }
        }
    }
}
