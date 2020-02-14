using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        public string VoucherNo { get { return "Ascentis" + Id.ToString().PadLeft(4, '0'); } }
        [Required] public DateTime EffectiveDate { get; set; }
        [Required] public DateTime ExpiryDate { get; set; }
        [Required] public decimal Value { get; set; }
    }
}
