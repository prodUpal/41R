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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ismagilov16_17
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage(User user)
        {
            InitializeComponent();

            var currentProduct = Ismagilov41Entities.GetContext().Product.ToList();

            if(user == null) {
                Name.Text = "гость";
                Role.Text = "гость";
            }
            else {
                Name.Text = user.UserName + user.UserSurname + user.UserPatronymic;
                switch (user.UserRole)
                {
                    case 2:
                        Role.Text = "Клиент"; break;
                    case 3:
                        Role.Text = "Менедежер"; break;
                    case 1:
                        Role.Text = "Администратор"; break;
                }
            }


            ProdAll.Text = Convert.ToString(currentProduct.Count);
            ProdAtTheMoment.Text = Convert.ToString(currentProduct.Count);


            ProductListView.ItemsSource = currentProduct;
        }


        private void UpdateProduct()
        {
            var currentProduct = Ismagilov41Entities.GetContext().Product.ToList();

            currentProduct = currentProduct.Where(p => p.ProductName.ToLower().Contains(TBSearch.Text.ToLower())).ToList();

            if (SortCombo.SelectedIndex == 0)
            {
                currentProduct = currentProduct.Where(p => Int32.Parse(p.SkidkaDeystv) > 0 && Int32.Parse(p.SkidkaDeystv) <= 9.99).ToList();
            }

            if (SortCombo.SelectedIndex == 1)
            {
                currentProduct = currentProduct.Where(p => Int32.Parse(p.SkidkaDeystv) > 10 && Int32.Parse(p.SkidkaDeystv) <= 14.99).ToList();
            }

            if (SortCombo.SelectedIndex == 2)
            {
                currentProduct = currentProduct.Where(p => Int32.Parse(p.SkidkaDeystv) > 15).ToList();
            }
            if (SortCombo.SelectedIndex == 3)
            {
                currentProduct = currentProduct.Where(p => Int32.Parse(p.SkidkaDeystv) != null).ToList();

            }

            ProdAtTheMoment.Text = Convert.ToString(currentProduct.Count);

            if (CostUp.IsChecked.Value)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.ProductCost).ToList();
            }
            if (CostDown.IsChecked.Value)
            {
                currentProduct = currentProduct.OrderBy(p => p.ProductCost).ToList();
            }

            ProductListView.ItemsSource = currentProduct;
        }



        private void SortCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void TBSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }



        private void CostUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }

        private void CostDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }
    }
}

