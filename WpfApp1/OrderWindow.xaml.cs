using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessModelCarShop;

namespace WpfApp1
{
    public partial class OrderWindow : Window
    {
        private ServiceReference1.Service1Client ws = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");
        private int selectedOrderIDFromGrid = -1;

        public OrderWindow()
        {
            InitializeComponent();
            LoadOrders();
            DpOrderDate.SelectedDate = DateTime.Now;
        }

        private void LoadOrders()
        {
            try
            {
                var list = ws.showAllOrders();
                if (list != null)
                    OrderGrid.ItemsSource = list.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtUsername.Text))
                {
                    MessageBox.Show("Please enter a Username for the order.");
                    return;
                }

                string username = TxtUsername.Text;
                DateTime orderDate = DpOrderDate.SelectedDate ?? DateTime.Now;
                decimal.TryParse(TxtTotalPrice.Text, out decimal totalPrice);
                string instructions = TxtInstructions.Text;

                string countryRegion = TxtCountryRegion.Text;
                string city = TxtCity.Text;
                string streetAddress = TxtStreetAddress.Text;
                string floor = TxtFloor.Text;
                string zipCode = TxtZipCode.Text;

                ws.AddOrder(username, orderDate, totalPrice, countryRegion, city, streetAddress, floor, zipCode, instructions);

                MessageBox.Show("Order added successfully!");
                LoadOrders();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error: " + ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int orderIDToUpdate = -1;
            if (!int.TryParse(TxtTargetOrderID.Text, out orderIDToUpdate))
            {
                orderIDToUpdate = selectedOrderIDFromGrid;
            }

            if (orderIDToUpdate <= 0)
            {
                MessageBox.Show("Please select or type a valid Order ID to update.");
                return;
            }

            try
            {
                var current = ws.showOrder(orderIDToUpdate);
                if (current == null)
                {
                    MessageBox.Show("Order not found.");
                    return;
                }

                string username = string.IsNullOrWhiteSpace(TxtUsername.Text) ? current.MyUsername : TxtUsername.Text;
                decimal totalPrice = string.IsNullOrWhiteSpace(TxtTotalPrice.Text) ? current.MyTotalPrice : decimal.Parse(TxtTotalPrice.Text);
                string address = string.IsNullOrWhiteSpace(TxtStreetAddress.Text) ? current.MyAddress : TxtStreetAddress.Text;
                DateTime orderDate = DpOrderDate.SelectedDate ?? current.MyOrderDate;
                string instructions = string.IsNullOrWhiteSpace(TxtInstructions.Text) ? current.MyAdditionalInstructions : TxtInstructions.Text;
                bool delivered = ChkDelivered.IsChecked ?? current.MyDelivered;

                ws.UpdateOrder(username, orderDate, totalPrice, address, instructions, delivered, orderIDToUpdate);

                MessageBox.Show($"Order {orderIDToUpdate} updated successfully!");
                LoadOrders();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int orderIDToDelete = -1;
            if (!int.TryParse(TxtTargetOrderID.Text, out orderIDToDelete))
            {
                orderIDToDelete = selectedOrderIDFromGrid;
            }

            if (orderIDToDelete <= 0)
            {
                MessageBox.Show("Please select or type a valid Order ID to delete.");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete Order ID {orderIDToDelete}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    ws.DeleteOrder(orderIDToDelete);
                    MessageBox.Show("Order deleted.");
                    LoadOrders();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Error: " + ex.Message);
                }
            }
        }

        private void OrderGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedOrder = OrderGrid.SelectedItem as dynamic;

            if (selectedOrder != null)
            {
                selectedOrderIDFromGrid = selectedOrder.MyOrderID;
                TxtTargetOrderID.Text = selectedOrder.MyOrderID.ToString();
                TxtUsername.Text = selectedOrder.MyUsername;
                TxtTotalPrice.Text = selectedOrder.MyTotalPrice.ToString();

                TxtStreetAddress.Text = selectedOrder.MyAddress;
                TxtCountryRegion.Clear();
                TxtCity.Clear();
                TxtFloor.Clear();
                TxtZipCode.Clear();

                DpOrderDate.SelectedDate = selectedOrder.MyOrderDate;
                TxtInstructions.Text = selectedOrder.MyAdditionalInstructions;
                ChkDelivered.IsChecked = selectedOrder.MyDelivered;
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearFields();

        private void ClearFields()
        {
            selectedOrderIDFromGrid = -1;
            TxtTargetOrderID.Clear();
            TxtUsername.Clear();
            TxtTotalPrice.Clear();

            TxtCountryRegion.Clear();
            TxtCity.Clear();
            TxtStreetAddress.Clear();
            TxtFloor.Clear();
            TxtZipCode.Clear();

            DpOrderDate.SelectedDate = DateTime.Now;
            TxtInstructions.Clear();
            ChkDelivered.IsChecked = false;
            OrderGrid.SelectedItem = null;
        }
    }
}
