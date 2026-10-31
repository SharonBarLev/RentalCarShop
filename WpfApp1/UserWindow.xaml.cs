using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessModelCarShop;

namespace WpfApp1
{
    public partial class UserWindow : Window
    {
        private ServiceReference1.Service1Client ws = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");
        private string selectedUsernameFromGrid = string.Empty;

        public UserWindow()
        {
            InitializeComponent();
            LoadUsers();
            DpJoinDate.SelectedDate = DateTime.Now;
        }

        private void LoadUsers()
        {
            try
            {
                var list = ws.showAllUsers();
                if (list != null)
                    UserGrid.ItemsSource = list.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading system users: " + ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = TxtTargetUsername.Text.Trim();

                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Please enter a valid Username to register.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtPassword.Text))
                {
                    MessageBox.Show("Please provide a password for the account.");
                    return;
                }

                string password = TxtPassword.Text;
                string phone = TxtPhoneNumber.Text;
                string email = TxtEmail.Text;
                bool isAdmin = ChkAdmin.IsChecked == true;
                DateTime joinDate = DpJoinDate.SelectedDate ?? DateTime.Now;

                ws.AddUser(username, password, phone, email, isAdmin, joinDate);

                MessageBox.Show($"User '{username}' registered successfully!");
                LoadUsers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registration Error: " + ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string usernameToUpdate = TxtTargetUsername.Text.Trim();
            if (string.IsNullOrWhiteSpace(usernameToUpdate))
            {
                usernameToUpdate = selectedUsernameFromGrid;
            }

            if (string.IsNullOrWhiteSpace(usernameToUpdate))
            {
                MessageBox.Show("Please select or enter a valid Username to update.");
                return;
            }

            try
            {
                var current = ws.showUser(usernameToUpdate);
                if (current == null)
                {
                    MessageBox.Show("User profile not found in the database.");
                    return;
                }

                string password = string.IsNullOrWhiteSpace(TxtPassword.Text) ? current.MyPassword : TxtPassword.Text;
                string phone = string.IsNullOrWhiteSpace(TxtPhoneNumber.Text) ? current.MyPhoneNumber : TxtPhoneNumber.Text;
                string email = string.IsNullOrWhiteSpace(TxtEmail.Text) ? current.MyEmail : TxtEmail.Text;
                bool isAdmin = ChkAdmin.IsChecked == true; 
                DateTime joinDate = DpJoinDate.SelectedDate ?? current.MyJoinDate;

                ws.UpdateUser(password, phone, email, isAdmin, joinDate, usernameToUpdate);

                MessageBox.Show($"User account '{usernameToUpdate}' updated successfully!");
                LoadUsers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            string usernameToDelete = TxtTargetUsername.Text.Trim();
            if (string.IsNullOrWhiteSpace(usernameToDelete))
            {
                usernameToDelete = selectedUsernameFromGrid;
            }

            if (string.IsNullOrWhiteSpace(usernameToDelete))
            {
                MessageBox.Show("Please select or enter a valid Username to delete.");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to permanently delete user '{usernameToDelete}'?", "Confirm Permanent Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    ws.DeleteUser(usernameToDelete);
                    MessageBox.Show("User profile deleted.");
                    LoadUsers();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Error: " + ex.Message);
                }
            }
        }

        private void UserGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserGrid.SelectedItem is User selectedUser)
            {
                selectedUsernameFromGrid = selectedUser.MyUsername;
                TxtTargetUsername.Text = selectedUser.MyUsername;
                TxtPassword.Text = selectedUser.MyPassword;
                TxtPhoneNumber.Text = selectedUser.MyPhoneNumber;
                TxtEmail.Text = selectedUser.MyEmail;
                ChkAdmin.IsChecked = selectedUser.MyAdmin;
                DpJoinDate.SelectedDate = selectedUser.MyJoinDate;
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearFields();

        private void ClearFields()
        {
            selectedUsernameFromGrid = string.Empty;
            TxtTargetUsername.Clear();
            TxtPassword.Clear();
            TxtPhoneNumber.Clear();
            TxtEmail.Clear();
            ChkAdmin.IsChecked = false;
            DpJoinDate.SelectedDate = DateTime.Now;
            UserGrid.SelectedItem = null;
        }
    }
}
