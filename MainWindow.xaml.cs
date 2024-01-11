using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using System.Windows.Controls;
using System.Xml.Linq;


namespace WpfApp1
{
      
    public partial class MainWindow : Window
    {
        private List<Customer> customers = new List<Customer>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCustomersFromXml();
        }

        private void addCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var addCustomerWindow = new AddCustomerWindow();
            addCustomerWindow.ShowDialog();

            if (addCustomerWindow.DialogResult == true)
            {
                var customer = addCustomerWindow.NewCustomer;
                customers.Add(customer);
                customerListView.Items.Add(customer);
            }
        }

        private void saveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveCustomersToXml();
        }

        private void discountButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyDiscount();
        }

        private void LoadCustomersFromXml()
        {
            if (!File.Exists("data.xml"))
                return;

            try
            {
                XDocument xmlDocument = XDocument.Load("data.xml");
                var customersXml = xmlDocument.Descendants("Customer");

                foreach (var customerXml in customersXml)
                {
                    Customer customer = new Customer()
                    {
                        Date = DateTime.Parse(customerXml.Element("Date").Value),
                        FirstName = customerXml.Element("FirstName").Value,
                        LastName = customerXml.Element("LastName").Value,
                        Patronymic = customerXml.Element("Patronymic").Value,
                        PeopleCount = int.Parse(customerXml.Element("PeopleCount").Value),
                        RoomNumber = int.Parse(customerXml.Element("RoomNumber").Value),
                        PassportData = customerXml.Element("PassportData").Value,
                        RoomPrice = decimal.Parse(customerXml.Element("RoomPrice").Value),
                        CustomerCode = customerXml.Element("CustomerCode").Value,
                        RoomLevel = int.Parse(customerXml.Element("RoomLevel").Value),
                    };

                    customers.Add(customer);
                    customerListView.Items.Add(customer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов из XML: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveCustomersToXml()
        {
            try
            {
                XDocument xmlDocument = new XDocument();
                XElement root = new XElement("Customers");

                foreach (var customer in customers)
                {
                    XElement customerXml = new XElement("Customer",
                        new XElement("Date", customer.Date),
                        new XElement("FirstName", customer.FirstName),
                        new XElement("LastName", customer.LastName),
                        new XElement("Patronymic", customer.Patronymic),
                        new XElement("PeopleCount", customer.PeopleCount),
                        new XElement("RoomNumber", customer.RoomNumber),
                        new XElement("PassportData", customer.PassportData),
                        new XElement("RoomPrice", customer.RoomPrice),
                        new XElement("CustomerCode", customer.CustomerCode),
                        new XElement("RoomLevel", customer.RoomLevel)
                    );

                    root.Add(customerXml);
                }

                xmlDocument.Add(root);
                xmlDocument.Save("data.xml");

                MessageBox.Show("Клиенты успешно сохранены в XML файл.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения клиентов в XML: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ApplyDiscount()
        {
            if (!int.TryParse(discountTextBox.Text, out int discountPercentage))
            {
                MessageBox.Show("Неверное значение скидки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (discountPercentage <= 0 || discountPercentage >= 100)
            {
                MessageBox.Show("Значение скидки должно быть между 0 и 100.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var customer in customers)
            {
                customer.RoomPrice *= (100 - discountPercentage) / 100m;
            }

            MessageBox.Show("Скидка успешно применена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }



    }
}

