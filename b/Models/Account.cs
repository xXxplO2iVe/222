using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_2.Models
{
    public enum AccountType
    {
        Checking = 1,
        Saving = 2
    }
    public class Account
    {
        public const decimal WITHDRAW_CHARGE = 0.1M;
        public const decimal TRANSFER_CHARGE = 0.2M;
        public const decimal minCheckingBalance = 200;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Display(Name = "Account Number")]
        public int AccountNumber { get; set; }

        [Required, Display(Name = "Account Type")]
        public AccountType AccountType { get; set; }

        [Required, ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Balance { get; set; }


        public virtual List<Transaction> Transactions { get; set; }

        public virtual List<BillPay> BillPays { get; set; }

        public void AddTransaction(TransactionType type, decimal amount, int? destination, string comment = null)
        {
            Transaction transaction = new Transaction()
            {
                TransactionType = type,
                AccountNumber = AccountNumber,
                DestinationAccountNumber = destination,
                Amount = amount,
                Comment = comment,
                TransactionTimeUtc = DateTime.UtcNow
            };

            Transactions.Add(transaction);
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
            AddTransaction(TransactionType.Deposit, amount, null);
        }

        public void BillPay(decimal amount)
        {
            Balance -= amount;
            AddTransaction(TransactionType.BillPay, amount, null);
        }

        public bool Withdraw(decimal amount)
        {
            bool sufficientFunds = CheckFunds(amount, WITHDRAW_CHARGE);

            if (!sufficientFunds)
            {
                return false;
            }

            var filteredList = FilterTransactions();

            if (filteredList.Count() >= 4)
            {
                Balance -= WITHDRAW_CHARGE;
                AddTransaction(TransactionType.ServiceCharge, WITHDRAW_CHARGE, null);
            }

            Balance -= amount;
            AddTransaction(TransactionType.Withdraw, amount, null);

            return true;
        }

        public bool Transfer(decimal amount, Account destinationAccount, string comment)
        {
            bool sufficientFunds = CheckFunds(amount, TRANSFER_CHARGE);

            if (!sufficientFunds)
            {
                return false;
            }

            var filteredList = FilterTransactions();

            if (filteredList.Count() >= 4)
            {
                Balance -= TRANSFER_CHARGE;
                AddTransaction(TransactionType.ServiceCharge, TRANSFER_CHARGE, null);
            }

            Balance -= amount;
            AddTransaction(TransactionType.Transfer, amount, destinationAccount.AccountNumber, comment);

            // Updating receiver account balance.
            destinationAccount.Balance = destinationAccount.Balance += amount;

            // Generate a transaction for receiver account.
            destinationAccount.AddTransaction(TransactionType.Transfer, amount, null);

            return true;
        }

        public bool CheckFunds(decimal amount, decimal serviceCharge)
        {
            var filteredList = FilterTransactions();

            if (filteredList.Count() >= 4)
            {
                if (AccountType == AccountType.Saving && amount + serviceCharge > Balance)
                {
                    return false;
                }
                if (AccountType == AccountType.Checking && Balance - amount - serviceCharge < minCheckingBalance)
                {
                    return false;
                }
            }
            else
            {
                if (AccountType == AccountType.Saving && amount > Balance)
                {
                    return false;
                }
                if (AccountType == AccountType.Checking && Balance - amount < minCheckingBalance)
                {
                    return false;
                }
            }

            return true;
        }

        public List<Transaction> FilterTransactions()
        {
            var filteredList = new List<Transaction>();

            foreach (var transaction in Transactions)
            {
                if (transaction.TransactionType == TransactionType.Withdraw || transaction.TransactionType == TransactionType.Transfer)
                {
                    filteredList.Add(transaction);
                }
            }

            return filteredList;
        }

        public void BillPay(BillPay billPay)
        {
            BillPays.Add(billPay);
        }
    }
}

