using System;

namespace BankSystem
{
    using Entities;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public partial class MainWindow : Window
    {
        private Bank _bank;
        private int _selectFromAccountId;
        private int _selectToAccountId;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _bank = new Bank(1, "TestBank");

            NameBankTextBox.Text = _bank.Name;
        }

        private void UpdateListAccountsOfBank()
        {
            ListAccountsOfBank.Items.Clear();
            foreach (var accountName in _bank.Accounts)
            {
                ListAccountsOfBank.Items.Add(accountName);
            }
        }

        private void ConfirmCreationOfNewAccountOfBank_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameNewAccountOfBank.Text))
            {
                NewAccountOfBankStatus.Content = "Задайте имя счета";
                return;
            }

            if (double.TryParse(SumNewAccountOfBank.Text, out var sum))
            {
                if (sum < 0)
                {
                    NewAccountOfBankStatus.Content = "Сумма счета не может быть меньше 0";
                    return;
                }

                if (sum >= 0)
                {
                    _bank.CreateAccount(NameNewAccountOfBank.Text, sum);
                    UpdateListAccountsOfBank();

                    NewAccountOfBankGrid.Visibility = Visibility.Collapsed;
                    AccountsOfBank.Visibility = Visibility.Visible;
                }
            }
            else
            {
                NewAccountOfBankStatus.Content = "Сумма указана некорректно";
            }
        }

        private void ListAccountsOfBank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListAccountsOfBank.SelectedItem is Account selectAccount)
            {
                _selectFromAccountId = selectAccount.Id;

                AccountsOfBank.Visibility = Visibility.Collapsed;
                AccountOfBankGrid.Visibility = Visibility.Visible;
                NameAccountOfBank.Text = selectAccount.Name;
                SumAccountOfBank.Text = $"{selectAccount.Balance:C2}";

                NewTransactionOfAccountGrid.Visibility = Visibility.Collapsed;
                TransactionOfAccountGrid.Visibility = Visibility.Visible;
            }
        }

        private void NewTransactionOfAccountSelectOtherAccount_Click(object sender, RoutedEventArgs e)
        {
            NewTransactionOfAccountListOtherAccounts.Visibility = Visibility.Visible;
        }

        private void NewTransactionOfAccountListAccounts_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                if (listBox.Visibility == Visibility.Visible)
                {
                    listBox.Items.Clear();
                    foreach (var accountName in _bank.Accounts.Where(x => x.Id != _selectFromAccountId))
                    {
                        listBox.Items.Add(accountName);
                    }

                    if (listBox.Items.Count < 1)
                    {
                        NewTransactionOfAccountListOtherAccounts.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void NewTransactionOfAccountListAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NewTransactionOfAccountListOtherAccounts.Visibility = Visibility.Collapsed;
            if (NewTransactionOfAccountListOtherAccounts.SelectedItem is Account selectTo)
            {
                _selectToAccountId = selectTo.Id;
                NewTransactionOfAccountToName.Text = selectTo.Name;
            }
        }

        private void NewTransactionOfAccountConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(NewTransactionOfAccountAmount.Text, out var amount))
            {
                if (amount == 0)
                {
                    NewTransactionOfAccountStatus.Content = "Сумма перевода не может быть равна 0";
                    return;
                }

                if (amount < 0)
                {
                    NewTransactionOfAccountStatus.Content = "Сумма перевода не может быть меньше 0";
                    return;
                }

                var accountFrom = _bank.Accounts.Single(x => x.Id == _selectFromAccountId);
                if (accountFrom.Balance < amount)
                {
                    NewTransactionOfAccountStatus.Content = "Для перевода недостаточно средств";
                    return;
                }

                var accountTo = _bank.Accounts.Single(x => x.Id == _selectToAccountId);
                _bank.NewTransactions(accountFrom, accountTo, amount);

                SumAccountOfBank.Text = accountFrom.Balance.ToString();

                NewTransactionOfAccountStatus.Content = "Перевод выполнен";
            }
            else
            {
                NewTransactionOfAccountStatus.Content = "Сумма указана некорректно";
            }
        }

        private void NewTransactionOfAccount_Click(object sender, RoutedEventArgs e)
        {
            TransactionOfAccountGrid.Visibility = Visibility.Collapsed;
            NewTransactionOfAccount.Visibility = Visibility.Collapsed;

            NewTransactionOfAccountGrid.Visibility = Visibility.Visible;
        }

        private void NewTransactionOfAccountGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Grid grid)
            {
                if (grid.Visibility == Visibility.Collapsed)
                {
                    _selectToAccountId = default;
                    NewTransactionOfAccountToName.Text = string.Empty;
                    NewTransactionOfAccountAmount.Text = string.Empty;
                    NewAccountOfBankStatus.Content = string.Empty;
                }
            }
        }

        private void NewAccountOfBank_Click(object sender, RoutedEventArgs e)
        {
            AccountsOfBank.Visibility = Visibility.Collapsed;
            NewAccountOfBankGrid.Visibility = Visibility.Visible;
        }

        private void CancelCreationOfNewAccountOfBank_Click(object sender, RoutedEventArgs e)
        {
            NewAccountOfBankGrid.Visibility = Visibility.Collapsed;
            AccountsOfBank.Visibility = Visibility.Visible;
        }

        private void ReturnToListOfBankAccounts_Click(object sender, RoutedEventArgs e)
        {
            AccountOfBankGrid.Visibility = Visibility.Collapsed;
            AccountsOfBank.Visibility = Visibility.Visible;
        }

        private void ReturnToAccountOfBank_Click(object sender, RoutedEventArgs e)
        {
            NewTransactionOfAccountGrid.Visibility = Visibility.Collapsed;

            TransactionOfAccountGrid.Visibility = Visibility.Visible;
            NewTransactionOfAccount.Visibility = Visibility.Visible;
        }

        private void AccountOfBankGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Grid accountOfBankGrid)
            {
                if (accountOfBankGrid.Visibility == Visibility.Visible)
                {
                    NewTransactionOfAccount.Visibility = Visibility.Visible;
                }
            }
        }

        private void TransactionOfAccountGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Grid transactionOfAccountGrid)
            {
                if (transactionOfAccountGrid.Visibility == Visibility.Visible)
                {
                    var transactions = _bank.Transactions.Where(x => x.IdAccountFrom == _selectFromAccountId
                                                                     ||
                                                                     x.IdAccountTo == _selectFromAccountId)
                                            .OrderByDescending(x => x.Created);

                    var transactionHistory = transactions.Select(x =>
                    {
                        var amount = x.Amount;
                        var prefix = x.IdAccountFrom == _selectFromAccountId ? "-" : "+";

                        return $"{x.Created}: " + $"{prefix} {amount:C2}".PadRight(' ');
                    });

                    TransactionOfAccountList.Items.Clear();
                    foreach (var transaction in transactionHistory)
                    {
                        TransactionOfAccountList.Items.Add(transaction);
                    }
                }
            }
        }
    }
}
