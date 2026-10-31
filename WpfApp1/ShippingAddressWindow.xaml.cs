using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessModelCarShop;

namespace WpfApp1
{
    public partial class ShippingAddressWindow : Window
    {
        private ServiceReference1.Service1Client ws = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");
        private string selectedUsernameFromGrid = string.Empty;

        public ShippingAddressWindow()
        {
            InitializeComponent();
            LoadShippingAddresses();
        }

        private void LoadShippingAddresses()
        {
            try
            {
                var list = ws.ShowAllShippingAddress();
                if (list != null)
                    ShippingGrid.ItemsSource = list.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading shipping addresses: " + ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = TxtTargetUsername.Text.Trim();

                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Please enter a valid Username to create a shipping profile.");
                    return;
                }

                string country = TxtCountryRegion.Text;
                string city = TxtCity.Text;
                string street = TxtStreetAddress.Text;
                string floor = TxtFloor.Text;
                string zip = TxtZipCode.Text;
                string instructions = TxtAdditionalInstructions.Text;

                ws.AddShippingAddress(username, country, city, street, floor, zip, instructions);

                MessageBox.Show("Shipping address added successfully!");
                LoadShippingAddresses();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error: " + ex.Message);
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
                var current = ws.ShowShippingAddress(usernameToUpdate);
                if (current == null)
                {
                    MessageBox.Show("Shipping profile not found for the specified user.");
                    return;
                }

                string country = string.IsNullOrWhiteSpace(TxtCountryRegion.Text) ? current.MyCountryRegion : TxtCountryRegion.Text;
                string city = string.IsNullOrWhiteSpace(TxtCity.Text) ? current.MyCity : TxtCity.Text;
                string street = string.IsNullOrWhiteSpace(TxtStreetAddress.Text) ? current.MyStreetAddress : TxtStreetAddress.Text;
                string floor = string.IsNullOrWhiteSpace(TxtFloor.Text) ? current.MyFloor : TxtFloor.Text;
                string zip = string.IsNullOrWhiteSpace(TxtZipCode.Text) ? current.MyZipCode : TxtZipCode.Text;
                string instructions = string.IsNullOrWhiteSpace(TxtAdditionalInstructions.Text) ? current.MyAdditionalInstructions : TxtAdditionalInstructions.Text;

                ws.UpdateShippingAddress(country, city, street, floor, zip, instructions, usernameToUpdate);

                MessageBox.Show($"Shipping profile for '{usernameToUpdate}' updated successfully!");
                LoadShippingAddresses();
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

            if (MessageBox.Show($"Are you sure you want to delete the shipping profile for user '{usernameToDelete}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    ws.DeleteShippingAddress(usernameToDelete);
                    MessageBox.Show("Shipping address deleted successfully.");
                    LoadShippingAddresses();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Error: " + ex.Message);
                }
            }
        }

        private void ShippingGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAddress = ShippingGrid.SelectedItem as dynamic;

            if (selectedAddress != null)
            {
                selectedUsernameFromGrid = selectedAddress.MyUsername;
                TxtTargetUsername.Text = selectedAddress.MyUsername;
                TxtCountryRegion.Text = selectedAddress.MyCountryRegion;
                TxtCity.Text = selectedAddress.MyCity;
                TxtStreetAddress.Text = selectedAddress.MyStreetAddress;
                TxtFloor.Text = selectedAddress.MyFloor;
                TxtZipCode.Text = selectedAddress.MyZipCode;
                TxtAdditionalInstructions.Text = selectedAddress.MyAdditionalInstructions;
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearFields();

        private void ClearFields()
        {
            selectedUsernameFromGrid = string.Empty;
            TxtTargetUsername.Clear();
            TxtCountryRegion.Clear();
            TxtCity.Clear();
            TxtStreetAddress.Clear();
            TxtFloor.Clear();
            TxtZipCode.Clear();
            TxtAdditionalInstructions.Clear();
            ShippingGrid.SelectedItem = null;
        }
    }
}
