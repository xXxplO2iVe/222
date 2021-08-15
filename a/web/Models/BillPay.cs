using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Web.Models
{
    public enum Period
    {
        [Display(Name = "One Off")]
        OneOff = 1,
        Monthly = 2,
        Quarterly = 3,
        Annually = 4
    }

    public class BillPay
    {
        [Required]
        public int BillPayID { get; set; }

        [Required, ForeignKey("Account")]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        [Required, ForeignKey("Payee")]
        public int PayeeID { get; set; }
        public virtual Payee Payee { get; set; }

        [Required, DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Required, DataType(DataType.DateTime), Display(Name = "Scheduled Date")]
        public DateTime ScheduleTimeUtc { get; set; }

        [Required]
        public Period Period { get; set; }
        public bool Blocked { get; set; } = false;
    }
}

