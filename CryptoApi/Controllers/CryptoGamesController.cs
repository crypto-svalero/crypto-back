using System;
using CryptoApi.Data;
using CryptoApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CryptoApi.Controllers {
    [ApiController]
    [Route("[controller]")]

    public class CryptoGameController : ControllerBase {
        //private static List<CryptoGame> games = new List<CryptoGame>
        //{
        //new CryptoGame { Id = 1, Name = "BitcoinGame", Url = "www.google.com", CryptoUsed = "Bitcoin", Availability = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", CreationDate = new DateTime(2022, 6, 20)},
        //new CryptoGame { Id = 2, Name = "EthereumGame", Url = "www.google.com", CryptoUsed = "Ethereum", Availability = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", CreationDate = new DateTime(2022, 6, 20)},
        //};

        private readonly DataContext _context;

        public CryptoGameController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<CryptoGame>> Get() {
            return Ok(_context.CryptoGames); //200
        }

        [HttpPut] // Update to fav, not fav
        public ActionResult Put(int id) {
            var existingCryptoGame = _context.CryptoGames?.Where(x => x.Id == id).FirstOrDefault();
            if (existingCryptoGame == null) {
                return Conflict("There is not any crypto game with this Id"); // Status 409
            } else {
                existingCryptoGame.Availability = !existingCryptoGame.Availability;
                return Ok();
            }
        }

        [HttpPost] // Create
        public ActionResult PostGame(CryptoGame cryptogame) {
            var existingCryptoGame = _context.CryptoGames?.Where(x => x.Name == cryptogame.Name).FirstOrDefault();

            if (existingCryptoGame != null) {
                return Conflict("There is an existing game with this name"); // status 409
            } else {
                var newCryptoGame = new CryptoGame {
                    Id = cryptogame.Id,
                    Name = cryptogame.Name,
                    Image = cryptogame.Image,
                    Url = cryptogame.Url,
                    CryptoUsed = cryptogame.CryptoUsed,
                    CreationDate = cryptogame.CreationDate,
                    Availability = cryptogame.Availability,

                };
                _context.CryptoGames?.Add(newCryptoGame);
                _context.SaveChanges();
                var resourceUrl = Request.Path.ToString() + "/" + newCryptoGame.Name;
                return Created(resourceUrl, newCryptoGame); // Status 201
            }
        }

        [HttpDelete]
        [Route("{Id}")]
        public ActionResult DeleteGame(int Id) {
            //var productToRemove = _context.CartItems?.Where(x => x.ProductId == ProductId).FirstOrDefault(); // buscando el CartItem por el ProductId
            var CryptoGameToRemove = _context.CryptoGames?.Where(x => x.Id == Id).FirstOrDefault();
            if (CryptoGameToRemove == null) {
                return NotFound("There is not a game saved with the selected id"); // status 404
            } else {
                _context.CryptoGames?.Remove(CryptoGameToRemove);
                _context.SaveChanges();
                return Ok(); // status 200
            }
        }



    }
}
