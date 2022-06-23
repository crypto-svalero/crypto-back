using System;
using CryptoApi.Data;
using CryptoApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CryptoApi.Controllers {
    [ApiController]
    [Route("[controller]")]

    public class FiatController : ControllerBase {
        //private static List<Fiat> fiats = new List<Fiat>
        //{
        //new Fiat { Id = 1, Name = "Dollar", Value = 1, Active = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", LastModified = new DateTime(2022, 6, 20)},
        //new Fiat { Id = 2, Name = "Euro", Value = 1.1, Active = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", LastModified = new DateTime(2022, 6, 20)},
        //};

        private readonly DataContext _context;

        public FiatController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Fiat>> GetFiats() {
            var fiats = _context.Fiats;
            return Ok(fiats); //200
        }

        [HttpPut] // Update to fav, not fav
        public ActionResult PutFiats(int id) {
            var existingFiat = _context.Fiats?.Where(x => x.Id == id).FirstOrDefault();
            if (existingFiat == null) {
                return Conflict("There is not any fiat with this Id"); // Status 409
            } else {
                existingFiat.Active = !existingFiat.Active;
                return Ok();
            }
        }

        [HttpPost] // Create
        public ActionResult PostFiat(Fiat fiat) {
            var existingFiat = _context.Fiats?.Where(x => x.Name == fiat.Name).FirstOrDefault();

            if (existingFiat != null) {
                return Conflict("There is an existing fiat with this name"); // status 409
            } else {
                var newFiat = new Fiat {
                    Id = fiat.Id,
                    Name = fiat.Name,
                    Value = fiat.Value,
                    Image = fiat.Image,
                    LastModified = fiat.LastModified,
                    Active = fiat.Active,

                };
                _context.Fiats?.Add(newFiat);
                _context.SaveChanges();
                var resourceUrl = Request.Path.ToString() + "/" + newFiat.Name;
                return Created(resourceUrl, newFiat); // Status 201
            }
        }

        [HttpDelete]
        [Route("{Id}")]
        public ActionResult DeleteFiat(int Id) {
            var FiatToRemove = _context.Fiats?.Where(x => x.Id == Id).FirstOrDefault();
            if (FiatToRemove == null) {
                return NotFound("There is not a fiat saved with the selected id"); // status 404
            } else {
                _context.Fiats?.Remove(FiatToRemove);
                _context.SaveChanges();
                return Ok(); // status 200
            }
        }

    }
}
