using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Assignment_2.Models
{
    public enum TransactionType
    {
        Deposit = 'D',
        Withdraw = 'W',
        Transfer = 'T',
        [Display(Name = "Service Charge")]
        ServiceCharge = 'S',
        BillPay = 'B',
    }

    public record Transaction
    {
        [Required, Display(Name = "ID")]
        public int TransactionID { get; init; }

        [Required, Display(Name = "Type")]
        public TransactionType TransactionType { get; init; }

        [Required, ForeignKey("Account"), Display(Name = "Acc No.")]
        public int AccountNumber { get; init; }
        public virtual Account Account { get; init; }

        [ForeignKey("DestinationAccount"), Display(Name = "Destination Acc No.")]
        public int? DestinationAccountNumber { get; init; }
        public virtual Account DestinationAccount { get; init; }

        [Required, DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; init; }

        #nullable enable
        [StringLength(30)]
        public string? Comment { get; init; }
        
        [Required, DataType(DataType.DateTime), Display(Name = "Time")]
        public DateTime TransactionTimeUtc { get; init; }
    }
}

