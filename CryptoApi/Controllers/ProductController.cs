using System;
using CryptoApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CryptoApi.Controllers {
    [ApiController]
    [Route("[controller]")]

    public class ProductController : ControllerBase {
        private static List<Product> games = new List<Product>
        {
        new Product { Id = 1, Name = "T-shirt Doge", Value = 5, Url = "www.google.com", Favourite = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", CreationDate = new DateTime(2022, 6, 20)},
        new Product { Id = 2, Name = "Couch Doge", Value = 3, Url = "www.google.com", Favourite = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", CreationDate = new DateTime(2022, 6, 20)},
        };


        // Se me muestran todas las cryptos 
        [HttpGet]
        public ActionResult<List<Crypto>> Get() {
            return Ok(games); //200
        }

        [HttpPut] // Update to fav, not fav
        public ActionResult Put(int id) {
            var existingProduct = games.Find(x => x.Id == id);
            if (existingProduct == null) {
                return Conflict("There is not any product with this Id"); // Status 409
            } else {
                existingProduct.Favourite = !existingProduct.Favourite;
                return Ok();
            }
        }

    }
}
