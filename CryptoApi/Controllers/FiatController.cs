using System;
using CryptoApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CryptoApi.Controllers {
    [ApiController]
    [Route("[controller]")]

    public class FiatController : ControllerBase {
        private static List<Fiat> fiats = new List<Fiat>
        {
        new Fiat { Id = 1, Name = "Dollar", Value = 1, Active = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", LastModified = new DateTime(2022, 6, 20)},
        new Fiat { Id = 2, Name = "Euro", Value = 1.1, Active = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", LastModified = new DateTime(2022, 6, 20)},
        };


        // Se me muestran todas las cryptos 
        [HttpGet]
        public ActionResult<List<Crypto>> Get() {
            return Ok(fiats); //200
        }

        [HttpPut] // Update to fav, not fav
        public ActionResult Put(int id) {
            var existingFiat = fiats.Find(x => x.Id == id);
            if (existingFiat == null) {
                return Conflict("There is not any fiat with this Id"); // Status 409
            } else {
                existingFiat.Active = !existingFiat.Active;
                return Ok();
            }
        }

    }
}
