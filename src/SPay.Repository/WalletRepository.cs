﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPay.BO.DataBase.Models;

namespace SPay.Repository
{
    public interface IWalletRepository
    {
        Task<decimal?> GetBalanceForStore(string storeKey);
    }
    public class WalletRepository : IWalletRepository
    {
        private readonly SPayDbContext _context;
        public WalletRepository(SPayDbContext _context)
        {
            this._context = _context;
        }
        public async Task<decimal?> GetBalanceForStore(string storeKey)
        {
            var result = await _context.Wallets.Where(w => w.StoreKey.Equals(storeKey)).Select(w => w.Balance).FirstOrDefaultAsync();
            return result;
        }
    }
}
