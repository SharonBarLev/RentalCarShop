using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class BrandWindow : Window
    {
        private ServiceReference1.Service1Client ws = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");

        public BrandWindow()
        {
            InitializeComponent();
            LoadBrands();
        }

        private void LoadBrands()
        {
            try
            {
                var list = ws.showAllBrand();
                if (list != null)
                {
                    BrandGrid.ItemsSource = list.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading brands: " + ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(TxtTargetBrandID.Text, out int brandID) || brandID != 0)
                {
                    MessageBox.Show("To add a new brand, Brand ID must be 0.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtBrandName.Text))
                {
                    MessageBox.Show("Please enter a Brand Name.");
                    return;
                }

                ws.AddBrand(TxtBrandName.Text, TxtCountryOfOrigin.Text, TxtWebsiteURL.Text, TxtDescription.Text);

                MessageBox.Show("Brand added successfully!");
                LoadBrands();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error: " + ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TxtTargetBrandID.Text, out int brandIDToUpdate) || brandIDToUpdate <= 0)
            {
                MessageBox.Show("Please enter or select a valid Brand ID greater than 0 to update.");
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(TxtBrandName.Text))
                {
                    MessageBox.Show("Brand Name cannot be empty.");
                    return;
                }

                ws.UpdateBrand(TxtBrandName.Text, TxtCountryOfOrigin.Text, TxtWebsiteURL.Text, TxtDescription.Text, brandIDToUpdate);

                MessageBox.Show($"Brand {brandIDToUpdate} updated successfully!");
                LoadBrands();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TxtTargetBrandID.Text, out int brandIDToDelete) || brandIDToDelete <= 0)
            {
                MessageBox.Show("Please enter or select a valid Brand ID to delete.");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete Brand ID {brandIDToDelete}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    ws.DeleteBrand(brandIDToDelete);
                    MessageBox.Show("Brand deleted.");
                    LoadBrands();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Error: " + ex.Message);
                }
            }
        }

        private void BrandGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BrandGrid.SelectedItem != null)
            {
                try
                {
                    dynamic selectedBrand = BrandGrid.SelectedItem;

                    TxtTargetBrandID.Text = selectedBrand.MyBrandID.ToString();
                    TxtBrandName.Text = selectedBrand.MyBrandName;
                    TxtCountryOfOrigin.Text = selectedBrand.MyCountryOfOrigin;
                    TxtWebsiteURL.Text = selectedBrand.MyWebsiteURL;
                    TxtDescription.Text = selectedBrand.MyDescription;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading selected brand data: " + ex.Message);
                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearFields();

        private void ClearFields()
        {
            BrandGrid.SelectionChanged -= BrandGrid_SelectionChanged;

            TxtTargetBrandID.Clear();
            TxtBrandName.Clear();
            TxtCountryOfOrigin.Clear();
            TxtWebsiteURL.Clear();
            TxtDescription.Clear();
            BrandGrid.SelectedItem = null;

            BrandGrid.SelectionChanged += BrandGrid_SelectionChanged;
        }
    }
}