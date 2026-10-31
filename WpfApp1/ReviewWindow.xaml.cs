using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessModelCarShop;

namespace WpfApp1
{
    public partial class ReviewWindow : Window
    {
        private ServiceReference1.Service1Client ws = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");
        private int selectedReviewIDFromGrid = -1;

        public ReviewWindow()
        {
            InitializeComponent();
            LoadReviews();
        }

        private void LoadReviews()
        {
            try
            {
                var list = ws.ShowAllReview();
                if (list != null)
                    ReviewGrid.ItemsSource = list.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reviews: " + ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtUsername.Text))
                {
                    MessageBox.Show("Please enter a Username to submit a review.");
                    return;
                }

                if (!int.TryParse(TxtProductID.Text, out int productID))
                {
                    MessageBox.Show("Please enter a valid Product ID.");
                    return;
                }

                string username = TxtUsername.Text;
                string revComment = TxtRev.Text;
                int rating = CmbRating.SelectedIndex + 1;

                ws.AddReview(username, productID, revComment, rating);

                MessageBox.Show("Review added successfully!");
                LoadReviews();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error: " + ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int reviewIDToUpdate = -1;
            if (!int.TryParse(TxtTargetReviewID.Text, out reviewIDToUpdate))
            {
                reviewIDToUpdate = selectedReviewIDFromGrid;
            }

            if (reviewIDToUpdate <= 0)
            {
                MessageBox.Show("Please select or type a valid Review ID to update.");
                return;
            }

            try
            {
                var current = ws.ShowReview(reviewIDToUpdate);
                if (current == null)
                {
                    MessageBox.Show("Review not found.");
                    return;
                }

                string username = string.IsNullOrWhiteSpace(TxtUsername.Text) ? current.MyUsername : TxtUsername.Text;
                int productID = string.IsNullOrWhiteSpace(TxtProductID.Text) ? current.MyProductID : int.Parse(TxtProductID.Text);
                string revComment = string.IsNullOrWhiteSpace(TxtRev.Text) ? current.MyRev : TxtRev.Text;
                int rating = CmbRating.SelectedIndex + 1;

                ws.UpdateReview(username, productID, revComment, rating, reviewIDToUpdate);

                MessageBox.Show($"Review ID {reviewIDToUpdate} updated successfully!");
                LoadReviews();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int reviewIDToDelete = -1;
            if (!int.TryParse(TxtTargetReviewID.Text, out reviewIDToDelete))
            {
                reviewIDToDelete = selectedReviewIDFromGrid;
            }

            if (reviewIDToDelete <= 0)
            {
                MessageBox.Show("Please select or type a valid Review ID to delete.");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete Review ID {reviewIDToDelete}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    ws.DeleteReview(reviewIDToDelete);
                    MessageBox.Show("Review deleted.");
                    LoadReviews();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Error: " + ex.Message);
                }
            }
        }

        private void ReviewGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedReview = ReviewGrid.SelectedItem as dynamic;

            if (selectedReview != null)
            {
                selectedReviewIDFromGrid = selectedReview.MyReviewID;
                TxtTargetReviewID.Text = selectedReview.MyReviewID.ToString();
                TxtUsername.Text = selectedReview.MyUsername;
                TxtProductID.Text = selectedReview.MyProductID.ToString();
                TxtRev.Text = selectedReview.MyRev;

                if (selectedReview.MyRating >= 1 && selectedReview.MyRating <= 5)
                {
                    CmbRating.SelectedIndex = selectedReview.MyRating - 1;
                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearFields();

        private void ClearFields()
        {
            selectedReviewIDFromGrid = -1;
            TxtTargetReviewID.Clear();
            TxtUsername.Clear();
            TxtProductID.Clear();
            TxtRev.Clear();
            CmbRating.SelectedIndex = 4; 
            ReviewGrid.SelectedItem = null;
        }
    }
}
