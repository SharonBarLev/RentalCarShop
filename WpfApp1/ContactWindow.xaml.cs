using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessModelCarShop;

namespace WpfApp1
{
    public partial class ContactWindow : Window
    {
        private ServiceReference1.Service1Client ws = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");
        private int selectedContactIDFromGrid = -1;

        public ContactWindow()
        {
            InitializeComponent();
            LoadContacts();
            DpDateSent.SelectedDate = DateTime.Now;
        }

        private void LoadContacts()
        {
            try
            {
                var list = ws.showAllContact();
                if (list != null)
                    ContactGrid.ItemsSource = list.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading contacts: " + ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtUsername.Text))
                {
                    MessageBox.Show("Please enter a Username.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(TxtFullName.Text))
                {
                    MessageBox.Show("Please enter a Full Name.");
                    return;
                }

                string username = TxtUsername.Text;
                string fullName = TxtFullName.Text;
                string email = TxtEmail.Text;
                string subject = TxtSubject.Text;
                string message = TxtMessage.Text;
                DateTime dateSent = DpDateSent.SelectedDate ?? DateTime.Now;

                ws.AddContact(username, fullName, email, subject, message, dateSent);

                MessageBox.Show("Contact added successfully!");
                LoadContacts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error: " + ex.Message);
            }
        }

        private void BtnUpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            int contactIDToUpdate = -1;
            if (!int.TryParse(TxtTargetContactID.Text, out contactIDToUpdate))
            {
                contactIDToUpdate = selectedContactIDFromGrid;
            }

            if (contactIDToUpdate <= 0)
            {
                MessageBox.Show("Please select or type a valid Contact ID to update.");
                return;
            }

            try
            {
                var current = ws.ShowContact(contactIDToUpdate);
                if (current == null)
                {
                    MessageBox.Show("Contact not found.");
                    return;
                }

                bool isTreated = ChkTreated.IsChecked ?? current.MyTreated;

                ws.UpdateContactStatus(contactIDToUpdate, isTreated);

                MessageBox.Show($"Contact {contactIDToUpdate} status updated successfully!");
                LoadContacts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Status Error: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int contactIDToDelete = -1;
            if (!int.TryParse(TxtTargetContactID.Text, out contactIDToDelete))
            {
                contactIDToDelete = selectedContactIDFromGrid;
            }

            if (contactIDToDelete <= 0)
            {
                MessageBox.Show("Please select or type a valid Contact ID to delete.");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete Contact ID {contactIDToDelete}?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    ws.DeleteContact(contactIDToDelete);
                    MessageBox.Show("Contact deleted.");
                    LoadContacts();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Error: " + ex.Message);
                }
            }
        }

        private void ContactGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContactGrid.SelectedItem is Contact selectedContact)
            {
                selectedContactIDFromGrid = selectedContact.MyContactID;
                TxtTargetContactID.Text = selectedContact.MyContactID.ToString();
                TxtUsername.Text = selectedContact.MyUsername;
                TxtFullName.Text = selectedContact.MyFullName;
                TxtEmail.Text = selectedContact.MyEmail;
                TxtSubject.Text = selectedContact.MySubject;
                TxtMessage.Text = selectedContact.MyMessage;
                DpDateSent.SelectedDate = selectedContact.MyDateSent;
                ChkTreated.IsChecked = selectedContact.MyTreated;
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearFields();

        private void ClearFields()
        {
            selectedContactIDFromGrid = -1;
            TxtTargetContactID.Clear();
            TxtUsername.Clear();
            TxtFullName.Clear();
            TxtEmail.Clear();
            TxtSubject.Clear();
            TxtMessage.Clear();
            DpDateSent.SelectedDate = DateTime.Now;
            ChkTreated.IsChecked = false;
            ContactGrid.SelectedItem = null;
        }
    }
}
