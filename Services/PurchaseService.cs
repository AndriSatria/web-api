using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly WebApiDbContext _context;
        readonly IMapper mapper = new Mapper(new MapperConfiguration(x => x.AddProfile<MappingProfile>()));

        public PurchaseService(WebApiDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PurchaseTransactionDto> GetAllTrans()
        {
            var purchaseTransactions = _context.PurchaseTransactions
                .Include(x => x.PurchaseTransactionDetails);

            if (!purchaseTransactions.Any())
                return null;

            return mapper.Map<IEnumerable<PurchaseTransactionDto>>(purchaseTransactions);
        }

        public PurchaseTransactionDto GetTrans(long receiptNo)
        {
            var purchaseTransaction = _context.PurchaseTransactions
                .Include(x => x.PurchaseTransactionDetails)
                .FirstOrDefault(c => c.ReceiptNo == receiptNo);

            if (purchaseTransaction == null)
                return null;

            return mapper.Map<PurchaseTransactionDto>(purchaseTransaction);
        }

        public PurchaseTransactionDto CreateTrans(long id, PurchaseTransactionDto purchaseTransaction)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return null;

            var postTrans = mapper.Map<PurchaseTransaction>(purchaseTransaction);
            _context.PurchaseTransactions.Add(postTrans);
            _context.SaveChanges();

            return mapper.Map<PurchaseTransactionDto>(postTrans);
        }
    }

    public interface IPurchaseService
    {
        IEnumerable<PurchaseTransactionDto> GetAllTrans();
        PurchaseTransactionDto GetTrans(long id);
        PurchaseTransactionDto CreateTrans(long id, PurchaseTransactionDto purchaseTransaction);
    }
}
