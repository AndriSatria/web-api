namespace WebApi.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string Salt { get; set; }
        public int BirthmonthPoint { get; set; }
        public int VoucherValidity { get; set; }
        public decimal VoucherValue { get; set; }
    }
}