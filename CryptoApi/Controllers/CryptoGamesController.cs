using System;
using CryptoApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CryptoApi.Controllers {
    [ApiController]
    [Route("[controller]")]

    public class CryptoGameController : ControllerBase {
        private static List<CryptoGame> games = new List<CryptoGame>
        {
        new CryptoGame { Id = 1, Name = "BitcoinGame", Url = "www.google.com", CryptoUsed = "Bitcoin", Availability = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", CreationDate = new DateTime(2022, 6, 20)},
        new CryptoGame { Id = 2, Name = "EthereumGame", Url = "www.google.com", CryptoUsed = "Ethereum", Availability = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", CreationDate = new DateTime(2022, 6, 20)},
        };


        // Se me muestran todas las cryptos 
        [HttpGet]
        public ActionResult<List<Crypto>> Get() {
            return Ok(games); //200
        }

        [HttpPut] // Update to fav, not fav
        public ActionResult Put(int id) {
            var existingCryptoGame = games.Find(x => x.Id == id);
            if (existingCryptoGame == null) {
                return Conflict("There is not any crypto with this Id"); // Status 409
            } else {
                existingCryptoGame.Availability = !existingCryptoGame.Availability;
                return Ok();
            }
        }

    }
}
