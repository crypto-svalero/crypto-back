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
        public ActionResult<List<NFT>> GetNfts() {
            return Ok(_context.NFTs); //200
        }

        [HttpPut] // Update availability
        public ActionResult PutNft(int id) {
            var existingNft = _context.NFTs?.Where(x => x.Id == id).FirstOrDefault();
            if (existingNft == null) {
                return Conflict("There is not any nft with this Id"); // Status 409
            } else {
                existingNft.Availability = !existingNft.Availability;
                return Ok();
            }
        }

        [HttpPost] // Create
        public ActionResult PostNft(NFT nft) {
            var existingNFT = _context.NFTs?.Where(x => x.Name == nft.Name).FirstOrDefault();

            if (existingNFT != null) {
                return Conflict("There is an existing fiat with this name"); // status 409
            } else {
                var newNft = new NFT {
                    Id = nft.Id,
                    Name = nft.Name,
                    Value = nft.Value,
                    Code = nft.Code,
                    Image = nft.Image,
                    CreationDate = nft.CreationDate,
                    Availability = nft.Availability,

                };
                _context.NFTs?.Add(newNft);
                _context.SaveChanges();
                var resourceUrl = Request.Path.ToString() + "/" + newNft.Name;
                return Created(resourceUrl, newNft); // Status 201
            }
        }

        [HttpDelete]
        [Route("{Id}")]
        public ActionResult DeleteNft(int Id) {
            var NftToRemove = _context.NFTs?.Where(x => x.Id == Id).FirstOrDefault();
            if (NftToRemove == null) {
                return NotFound("There is not a nft saved with the selected id"); // status 404
            } else {
                _context.NFTs?.Remove(NftToRemove);
                _context.SaveChanges();
                return Ok(); // status 200
            }
        }

    }
}
