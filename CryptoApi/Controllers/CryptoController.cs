using System;
using CryptoApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CryptoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CryptoController : ControllerBase
    {
        private static List<Crypto> cryptos = new List<Crypto>
        {
        new Crypto { Id = 1, Name = "Bitcoin", CurrentValue = 10, LastValue = 9, Favourite = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", LastModified = new DateTime(2022, 6, 20)},
        new Crypto { Id = 2, Name = "Etehreum", CurrentValue = 5, LastValue = 4, Favourite = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", LastModified = new DateTime(2022, 6, 20)},
        };


        // Se me muestran todas las cryptos 
        [HttpGet]
        public ActionResult<List<Crypto>> Get() {
            return Ok(cryptos); //200
        }

        [HttpPut] // Update to fav, not fav
        public ActionResult Put(int id) {
            var existingCrypto = cryptos.Find(x => x.Id == id);
            if (existingCrypto == null) {
                return Conflict("There is not any crypto with this Id"); // Status 409
            } else {
                existingCrypto.Favourite = !existingCrypto.Favourite;
                return Ok();
            }
        }

        [HttpPost] // Create
        public ActionResult Post(Crypto crypto) {
            // var existingCartItem = _cartItem.Find(x => x.ProductId == cartItem.ProductId);
            //var existingCartItem = _context.CartItems?.Find(cartItem.ProductId);
            var existingCrypto = cryptos.Find(x => x.Name == crypto.Name);
            if (existingCrypto != null) {
                return Conflict("There is an existing crypto with this name"); // status 409
            } else {
                var newCrypto = new Crypto
                {
                    Id = crypto.Id,
                    Name = crypto.Name,
                    CurrentValue = crypto.CurrentValue,
                    LastValue = crypto.LastValue,
                    Favourite = crypto.Favourite,
                    Image = crypto.Image,
                    LastModified = crypto.LastModified
                };
                //_context.CartItems?.Add(cartItem);
                //_context.SaveChanges();
                var resourceUrl = Request.Path.ToString() + "/" + newCrypto.Name;
                return Created(resourceUrl, newCrypto); // Status 201
            }
        }

        [HttpDelete]
        [Route("{Id}")]

        public ActionResult Delete(int Id) {
            //var productToRemove = _context.CartItems?.Where(x => x.ProductId == ProductId).FirstOrDefault(); // buscando el CartItem por el ProductId
            var productToRemove = cryptos.Find(x => x.Id == Id);
            if (productToRemove == null) {
                return NotFound("There is not a crypto saved with the selected id"); // status 404
            } else {
                //_context.CartItems?.Remove(productToRemove);
                //_context.SaveChanges();
                cryptos.Remove(productToRemove);
                return Ok(); // status 200
            }
        }
    }
}

