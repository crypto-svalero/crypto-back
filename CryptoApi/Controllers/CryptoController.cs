using System;
using CryptoApi.Data;
using CryptoApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CryptoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CryptoController : ControllerBase
    {
        //private static List<Crypto> cryptos = new List<Crypto>
        //{
        //new Crypto { Id = 1, Name = "Bitcoin", CurrentValue = 10, LastValue = 9, Favourite = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", LastModified = new DateTime(2022, 6, 20)},
        //new Crypto { Id = 2, Name = "Ethereum", CurrentValue = 5, LastValue = 4, Favourite = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", LastModified = new DateTime(2022, 6, 20)},
        //};

        private readonly DataContext _context;

        public CryptoController(DataContext context) {
            _context = context;
        }


        // Se me muestran todas las cryptos 
        [HttpGet]
        public ActionResult<List<Crypto>> Get() {
            return Ok(_context.Cryptos); //200
        }

        [HttpPatch] // Update to fav, not fav
        public ActionResult PatchFav(int id) {
            var existingCrypto = _context.Cryptos?.Where(x => x.Id == id).FirstOrDefault();
            if (existingCrypto == null)
            {
              return Conflict("There is not any crypto with this Id"); // Status 409
            }
            else
            {
                existingCrypto.Favourite = !existingCrypto.Favourite;
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpGet]
        [Route("/favorites")]
        public ActionResult<List<Crypto>> GetFavorites() {
            var favCryptos = _context.Cryptos?.Where(c => c.Favourite == true).ToList();
            return Ok(favCryptos); //200
        }




        [HttpPost] // Create
        public ActionResult Post(Crypto crypto) {
             var existingCrypto = _context.Cryptos?.Where(x => x.Name == crypto.Name).FirstOrDefault();
               
            if (existingCrypto != null)
            {
                return Conflict("There is an existing crypto with this name"); // status 409
            }
            else
            {
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
                _context.Cryptos?.Add(newCrypto);
                _context.SaveChanges();
                var resourceUrl = Request.Path.ToString() + "/" + newCrypto.Name;
                return Created(resourceUrl, newCrypto); // Status 201
            }
        }

        [HttpDelete]
        [Route("{Id}")]

        public ActionResult Delete(int Id) {
            //var productToRemove = _context.CartItems?.Where(x => x.ProductId == ProductId).FirstOrDefault(); // buscando el CartItem por el ProductId
            var CryptoToRemove = _context.Cryptos?.Where(x => x.Id == Id).FirstOrDefault();
            if (CryptoToRemove == null) {
                return NotFound("There is not a crypto saved with the selected id"); // status 404
            } else {
                _context.Cryptos?.Remove(CryptoToRemove);
                _context.SaveChanges();
                return Ok(); // status 200
            }
        }
    }
}

