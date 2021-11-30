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

namespace Matut_Andreea_Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        AutoLotEntitiesModel ctx = new AutoLotEntitiesModel();
        CollectionViewSource customerViewSource;
        CollectionViewSource inventoryViewSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource customerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // customerViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource inventoryViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("inventoryViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // inventoryViewSource.Source = [generic data source]

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = null;
            Inventory inventory = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    customer = new Customer()
                    {
                        FirstName = firstNameTextBox.Text.Trim(),
                        LastName = lastNameTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Customers.Add(customer);
                    customerViewSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                    //instantiem Customer entity
                    inventory = new Inventory()
                    {
                        FirstName = firstNameTextBox.Text.Trim(),
                        LastName = lastNameTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Inventory.Add(inventory);
                    inventoryViewSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                else
 if (action == ActionState.Edit)
                {
                    try
                    {
                        customer = (Customer)customerDataGrid.SelectedItem;
                        customer.FirstName = firstNameTextBox.Text.Trim();
                        customer.LastName = lastNameTextBox.Text.Trim();
                        //salvam modificarile
                        ctx.SaveChanges();
                        inventory = (Inventory)inventoryDataGrid.SelectedItem;
                        inventory.FirstName = firstNameTextBox.Text.Trim();
                        inventory.LastName = lastNameTextBox.Text.Trim();
                        //salvam modificarile
                        ctx.SaveChanges();
                    }
                    catch (DataException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    customerViewSource.View.Refresh();
                    // pozitionarea pe item-ul curent
                    customerViewSource.View.MoveCurrentTo(customer);
                    inventoryViewSource.View.Refresh();
                    // pozitionarea pe item-ul curent
                    inventoryViewSource.View.MoveCurrentTo(inventory);

else if (action == ActionState.Delete)
                    {
                        try
                        {
                            customer = (Customer)customerDataGrid.SelectedItem;
                            ctx.Customers.Remove(customer);
                            inventory = (Inventory)inventoryDataGrid.SelectedItem;
                            ctx.Inventory.Remove(inventory);
                            ctx.SaveChanges();
                        }
                        catch (DataException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        customerViewSource.View.Refresh();
                        inventoryViewSource.View.Refresh();
                    }
                }
            }

        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            customerViewSource.View.MoveCurrentToNext();
            inventoryViewSource.View.MoveCurrentToNext();
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            inventoryViewSource.View.MoveCurrentToPrevious();
        }
    }
}
