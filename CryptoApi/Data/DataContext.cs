using System;
using CryptoApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace CryptoApi.Data
{
        public class DataContext : DbContext {

            public DataContext(DbContextOptions<DataContext> options) : base(options) { }

            public DbSet<Crypto>? Cryptos { get; set; }
            public DbSet<CryptoGame>? CryptoGames { get; set; }
            public DbSet<Fiat>? Fiats { get; set; }
            public DbSet<NFT>? NFTs { get; set; }
            public DbSet<Product>? Products { get; set; }

        }
    
}

