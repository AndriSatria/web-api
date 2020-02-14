using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class PurchaseTransaction
    {
        [Key] public int ReceiptNo { get; set; }
        [Required] public decimal Amount { get; set; }
        [Required] public DateTime TransactDateTime { get; set; }
        public ICollection<PurchaseTransactionDetail> PurchaseTransactionDetails { get; set; }
    }
}
