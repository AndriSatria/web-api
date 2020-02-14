using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class PurchaseTransactionDetail
    {
        public int Id { get; set; }
        [Required] public int ItemNo { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public decimal Price { get; set; }
        [Required] public decimal TotalPrice { get; set; }
        public PurchaseTransaction PurchaseTransaction { get; set; }

    }
}
