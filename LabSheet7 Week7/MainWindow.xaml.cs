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

namespace LabSheet7_Week7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Entities db = new Entities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Q1Bttn_Click(object sender, RoutedEventArgs e)
        {
            //Ex1
            var query = from c in db.Customers
                        group c by c.Country into n
                        orderby n.Count() descending
                        select new 
                        { 
                            Customer = n.Key,
                            Count = n.Count()
                        };

          
            Ex1DgDetails.ItemsSource = query.ToList();
         

        }

        private void Q2Bttn_Click(object sender, RoutedEventArgs e)
        {
            var query = from customers in db.Customers
                        where customers.Country == "Italy"
                        select new
                        {
                            customers.Country,
                            customers.CustomerID,
                            customers.CompanyName,
                            customers.ContactName
                        };

            Ex2DgDetails.ItemsSource = query.ToList();   

        }

        private void Q3Bttn_Click(object sender, RoutedEventArgs e)
        {
            var query = from products in db.Products
                        where products.UnitsInStock >= 0
                        select new
                        {
                            products.ProductName,
                            products.UnitsInStock
                        };
            Ex3DgDetails.ItemsSource = query.ToList();

            
                
        }

        private void Q4Bttn_Click(object sender, RoutedEventArgs e)
        {
            var query = from products in db.Order_Details
                        where products.Discount > 0
                        select new
                        {
                            products.Product.ProductName,
                            products.Discount,
                            products.OrderID

                        };
            Ex4DgDetails.ItemsSource = query.ToList();

        }

        private void Q5Bttn_Click(object sender, RoutedEventArgs e)
        {

            var query = db.Orders.Sum(x => x.Freight);
            
            Ex5DgDetails.Text = string.Format($"\nThe total value of freight for all orders is {0:c}", query);


        }

        private void Q6Bttn_Click(object sender, RoutedEventArgs e)
        {
            var query = from prod in db.Categories
join p in db.Products on prod.CategoryName equals p.Category.CategoryName
                        orderby p.Category.CategoryName, p.UnitPrice descending
                        select new
                        {
                          CategoryName = prod.CategoryName,
                          CategoryID= prod.CategoryID,
                          ProductName = p.ProductName,
                          UnitPrice = p.UnitPrice
                         
                        };

            Ex6DgDetails.ItemsSource = query.ToList();

        }

        private void Q7Bttn_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Orders
                        group c by c.CustomerID into g
                        orderby g.Count() descending
                        select new
                        {
                            CustomerID = g.Key,
                            NumberOfOrders = g.Count()
                        };

            Ex7DgDetails.ItemsSource = query.ToList();
        }

        private void Q8Bttn_Click(object sender, RoutedEventArgs e)
        {
            var query = from o in db.Orders
                        join c in db.Customers on o.CustomerID equals c.CustomerID
                        group c by c.CompanyName into g
                        orderby g.Count() descending
                        select new
                        {
                            CompanyName = g.Key,
                            NumberOfOrder = g.Count(),
                        };
            Ex8DgDetails.ItemsSource = query.Take(10).ToList();
        }

        private void Q9Bttn_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        where c.Orders.Count() == 0
                        select new
                        {
                            CompanyName = c.CompanyName,
                            NumberOfOrders = c.Orders.Count(),

                        };

            Ex9DgDetails.ItemsSource = query.ToList();
        }
    }
}
