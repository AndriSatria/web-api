using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.DTO;

namespace WebApi.Models
{
    public class PurchaseTransactionDto
    {
        public int ReceiptNo { get; set; }
        [Required] public decimal Amount { get; set; }
        [Required] public DateTime TransactDateTime { get; set; }
        [Required] public ICollection<PurchaseTransactionDetailDto> PurchaseTransactionDetails { get; set; }
        public VoucherDto Voucher { get; set; }
    }
}
