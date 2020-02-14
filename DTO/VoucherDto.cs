using System;

namespace WebApi.DTO
{
    public class VoucherDto
    {
        public string VoucherNo { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Value { get; set; }
    }
}
