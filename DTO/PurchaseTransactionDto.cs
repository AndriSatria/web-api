using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class PurchaseTransactionDto
    {
        public int ReceiptNo { get; set; }
        [Required] public decimal Amount { get; set; }
        [Required] public DateTime TransactDateTime { get; set; }
        [Required] public ICollection<PurchaseTransactionDetailDto> PurchaseTransactionDetails { get; set; }
    }
}
