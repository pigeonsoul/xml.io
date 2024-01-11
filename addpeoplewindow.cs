using System;
using System.Windows;

namespace WpfApp1
{
    public partial class AddCustomerWindow : Window
    {
        public Customer NewCustomer { get; private set; }

        public AddCustomerWindow()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(lastNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(peopleCountTextBox.Text) ||
                string.IsNullOrWhiteSpace(roomNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(roomPriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(roomLevelTextBox.Text))
            {
                MessageBox.Show("Заполните обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!DateTime.TryParse(datePicker.Text, out DateTime date))
            {
                MessageBox.Show("Неверное значение даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(peopleCountTextBox.Text, out int peopleCount) || peopleCount <= 0)
            {
                MessageBox.Show("Неверное значение количества заселяемых человек.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(roomNumberTextBox.Text, out int roomNumber) || roomNumber <= 0)
            {
                MessageBox.Show("Неверное значение номера комнаты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(roomPriceTextBox.Text, out decimal roomPrice) || roomPrice <= 0)
            {
                MessageBox.Show("Неверное значение цены за комнату.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(customerCodeTextBox.Text))
            {
                MessageBox.Show("Введите индивидуальный код клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(roomLevelTextBox.Text, out int roomLevel) || roomLevel <= 0)
            {
                MessageBox.Show("Неверное значение уровня комнаты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NewCustomer = new Customer()
            {
                Date = date,
                FirstName = firstNameTextBox.Text,
                LastName = lastNameTextBox.Text,
                Patronymic = patronymicTextBox.Text,
                PeopleCount = peopleCount,
                RoomNumber = roomNumber,
                PassportData = passportDataTextBox.Text,
                RoomPrice = roomPrice,
                CustomerCode = customerCodeTextBox.Text,
                RoomLevel = roomLevel,
            };

            DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}