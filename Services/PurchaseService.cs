using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApi.DTO;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly AppSettings _appSettings;
        private readonly WebApiDbContext _context;
        readonly IMapper mapper = new Mapper(new MapperConfiguration(x => x.AddProfile<MappingProfile>()));

        public PurchaseService(IOptions<AppSettings> appSettings, WebApiDbContext context)
        {
            _appSettings = appSettings.Value;
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

            purchaseTransaction = mapper.Map<PurchaseTransactionDto>(postTrans);

            // if member’s birth month falls into transact month
            if (user.Dob.Month == postTrans.TransactDateTime.Month)
            {
                //add 10 points to user
                user.Point = user.Point.HasValue ? user.Point += _appSettings.BirthmonthPoint : user.Point = _appSettings.BirthmonthPoint;
                _context.SaveChanges();
                // give Rp. 100 value voucher with 3 months of validity
                purchaseTransaction.Voucher = CreateVoucher(postTrans.TransactDateTime,
                    postTrans.TransactDateTime.AddMonths(_appSettings.VoucherValidity),
                    _appSettings.VoucherValue);
            };

            return purchaseTransaction;
        }

        private VoucherDto CreateVoucher(DateTime effectiveDate, DateTime expiryDate, decimal value)
        {
            // issue new voucher
            var voucher = new Voucher()
            {
                EffectiveDate = effectiveDate,
                ExpiryDate = expiryDate,
                Value = value
            };

            _context.Vouchers.Add(voucher);
            _context.SaveChanges();

            return mapper.Map<VoucherDto>(voucher);
        }
    }

    public interface IPurchaseService
    {
        IEnumerable<PurchaseTransactionDto> GetAllTrans();
        PurchaseTransactionDto GetTrans(long id);
        PurchaseTransactionDto CreateTrans(long id, PurchaseTransactionDto purchaseTransaction);
    }
}
