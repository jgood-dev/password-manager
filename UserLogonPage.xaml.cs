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
    /// <summary>
    /// Interaction logic for UserLogonPage.xaml
    /// </summary>
    public partial class UserLogonPage : Window
    {
        List<User> users = new();

        public UserLogonPage()
        {
            InitializeComponent();
            GetUsers();
        }

        #region Data Access
        private void GetUsers()
        {
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

                    MainWindow main = new();
                    this.Visibility = Visibility.Collapsed;
                    main.ShowDialog();
                    
                }
            }
        }
        #endregion

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

    }
}
