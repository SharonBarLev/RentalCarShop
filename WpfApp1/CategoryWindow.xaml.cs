using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class CategoryWindow : Window
    {
        private ServiceReference1.Service1Client ws = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");

        public CategoryWindow()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                var list = ws.showAllCategory();
                if (list != null)
                {
                    CategoryGrid.ItemsSource = list.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(TxtTargetCategoryID.Text, out int catID) || catID != 0)
                {
                    MessageBox.Show("To add a new category, Category ID must be 0.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtCategoryName.Text))
                {
                    MessageBox.Show("Please enter a Category Name.");
                    return;
                }

                ws.AddCategory(TxtCategoryName.Text, TxtDescription.Text);

                MessageBox.Show("Category added successfully!");
                LoadCategories();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error: " + ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TxtTargetCategoryID.Text, out int catIDToUpdate) || catIDToUpdate <= 0)
            {
                MessageBox.Show("Please enter or select a valid Category ID greater than 0 to update.");
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(TxtCategoryName.Text))
                {
                    MessageBox.Show("Category Name cannot be empty.");
                    return;
                }

                ws.UpdateCategory(TxtCategoryName.Text, TxtDescription.Text, catIDToUpdate);

                MessageBox.Show($"Category {catIDToUpdate} updated successfully!");
                LoadCategories();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TxtTargetCategoryID.Text, out int catIDToDelete) || catIDToDelete <= 0)
            {
                MessageBox.Show("Please enter or select a valid Category ID to delete.");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete Category ID {catIDToDelete}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    ws.DeleteCategory(catIDToDelete);
                    MessageBox.Show("Category deleted.");
                    LoadCategories();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Error: " + ex.Message);
                }
            }
        }

        private void CategoryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryGrid.SelectedItem != null)
            {
                try
                {
                    dynamic selectedCategory = CategoryGrid.SelectedItem;

                    TxtTargetCategoryID.Text = selectedCategory.MyCategoryID.ToString();
                    TxtCategoryName.Text = selectedCategory.MyCategoryName;
                    TxtDescription.Text = selectedCategory.MyDescription;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading selected category data: " + ex.Message);
                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearFields();

        private void ClearFields()
        {
            CategoryGrid.SelectionChanged -= CategoryGrid_SelectionChanged;

            TxtTargetCategoryID.Clear();
            TxtCategoryName.Clear();
            TxtDescription.Clear();
            CategoryGrid.SelectedItem = null;

            CategoryGrid.SelectionChanged += CategoryGrid_SelectionChanged;
        }
    }
}