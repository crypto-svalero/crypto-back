using System;
using CryptoApi.Data;
using CryptoApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CryptoApi.Controllers {
    [ApiController]
    [Route("[controller]")]

    public class NFTController : ControllerBase {
        //private static List<NFT> nfts = new List<NFT>
        //{
        //new NFT { Id = 1, Name = "Mono Azul", Value = 15, Code = new Guid(), Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", CreationDate = new DateTime(2022, 6, 20), Availability = true},
        //new NFT { Id = 2, Name = "Mono Verde", Value = 12, Code = new Guid(), Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", CreationDate = new DateTime(2022, 6, 20), Availability = true},
        //};

        private readonly DataContext _context; // referencia al conexto de del la db

        public NFTController(DataContext context) {
            _context = context;
        }


        // Se me muestran todas las cryptos 
        [HttpGet]
        public ActionResult<List<NFT>> Get() {
            return Ok(_context.NFTs); //200
        }

        [HttpPut] // Update availability
        public ActionResult Put(int id) {
            var existingNft = _context.NFTs?.Where(x => x.Id == id).FirstOrDefault();
            if (existingNft == null) {
                return Conflict("There is not any nft with this Id"); // Status 409
            } else {
                existingNft.Availability = !existingNft.Availability;
                return Ok();
            }
        }

    }
}
