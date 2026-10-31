using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessModelCarShop;

namespace WpfApp1
{
    public partial class OrderDetailWindow : Window
    {
        private ServiceReference1.Service1Client ws = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");
        private int selectedDetailIDFromGrid = -1;

        public OrderDetailWindow()
        {
            InitializeComponent();
            LoadOrderDetails();
        }

        private void LoadOrderDetails()
        {
            try
            {
                var list = ws.showAllOrderDetails();
                if (list != null)
                    OrderDetailGrid.ItemsSource = list.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading order details: " + ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(TxtOrderID.Text, out int orderID) ||
                    !int.TryParse(TxtProductID.Text, out int productID) ||
                    !int.TryParse(TxtQuantity.Text, out int quantity) ||
                    !decimal.TryParse(TxtUnitPrice.Text, out decimal unitPrice))
                {
                    MessageBox.Show("Please ensure all fields contain valid numerical values.");
                    return;
                }

                ws.AddOrderDetails(orderID, productID, quantity, unitPrice);

                MessageBox.Show("Order detail record added successfully!");
                LoadOrderDetails();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error: " + ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int detailIDToUpdate = -1;
            if (!int.TryParse(TxtTargetDetailID.Text, out detailIDToUpdate))
            {
                detailIDToUpdate = selectedDetailIDFromGrid;
            }

            if (detailIDToUpdate <= 0)
            {
                MessageBox.Show("Please select or enter a valid Detail ID to update.");
                return;
            }

            try
            {
                var current = ws.showOrderDetails(detailIDToUpdate);
                if (current == null)
                {
                    MessageBox.Show("Order detail record not found.");
                    return;
                }

                int orderID = string.IsNullOrWhiteSpace(TxtOrderID.Text) ? current.MyOrderID : int.Parse(TxtOrderID.Text);
                int productID = string.IsNullOrWhiteSpace(TxtProductID.Text) ? current.MyProductID : int.Parse(TxtProductID.Text);
                int quantity = string.IsNullOrWhiteSpace(TxtQuantity.Text) ? current.MyQuantity : int.Parse(TxtQuantity.Text);
                decimal unitPrice = string.IsNullOrWhiteSpace(TxtUnitPrice.Text) ? current.MyUnitPrice : decimal.Parse(TxtUnitPrice.Text);

                ws.UpdateOrderDetails(orderID, productID, quantity, unitPrice, detailIDToUpdate);

                MessageBox.Show($"Order Detail ID {detailIDToUpdate} updated successfully!");
                LoadOrderDetails();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int detailIDToDelete = -1;
            if (!int.TryParse(TxtTargetDetailID.Text, out detailIDToDelete))
            {
                detailIDToDelete = selectedDetailIDFromGrid;
            }

            if (detailIDToDelete <= 0)
            {
                MessageBox.Show("Please select or enter a valid Detail ID to delete.");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete Detail ID {detailIDToDelete}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    ws.DeleteOrderDetails(detailIDToDelete);
                    MessageBox.Show("Order detail record deleted.");
                    LoadOrderDetails();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Error: " + ex.Message);
                }
            }
        }

        private void OrderDetailGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDetail = OrderDetailGrid.SelectedItem as dynamic;

            if (selectedDetail != null)
            {
                selectedDetailIDFromGrid = selectedDetail.MyOrderDetailsID;
                TxtTargetDetailID.Text = selectedDetail.MyOrderDetailsID.ToString();
                TxtOrderID.Text = selectedDetail.MyOrderID.ToString();
                TxtProductID.Text = selectedDetail.MyProductID.ToString();
                TxtQuantity.Text = selectedDetail.MyQuantity.ToString();
                TxtUnitPrice.Text = selectedDetail.MyUnitPrice.ToString();
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearFields();

        private void ClearFields()
        {
            selectedDetailIDFromGrid = -1;
            TxtTargetDetailID.Clear();
            TxtOrderID.Clear();
            TxtProductID.Clear();
            TxtQuantity.Clear();
            TxtUnitPrice.Clear();
            OrderDetailGrid.SelectedItem = null;
        }
    }
}
