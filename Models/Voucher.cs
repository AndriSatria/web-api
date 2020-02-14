using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        public string VoucherNo => $"Ascentis{Id.ToString().PadLeft(4, '0')}";
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Value { get; set; }
    }
}
