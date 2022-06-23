using System;
using CryptoApi.Data;
using CryptoApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CryptoApi.Controllers {
    [ApiController]
    [Route("[controller]")]

    public class ProductController : ControllerBase
    {
        //private static List<Product> games = new List<Product>
        //{
        //new Product { Id = 1, Name = "T-shirt Doge", Value = 5, Url = "www.google.com", Favourite = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", CreationDate = new DateTime(2022, 6, 20)},
        //new Product { Id = 2, Name = "Couch Doge", Value = 3, Url = "www.google.com", Favourite = false, Image = "https://w7.pngwing.com/pngs/766/282/png-transparent-bitcoin-btc-crypto-cryptocurrency-digital-currency-gold-money-fintech-coin-technology.png", CreationDate = new DateTime(2022, 6, 20)},
        //};

        private readonly DataContext _context; // referencia al conexto de del la db

        public ProductController(DataContext context) {
            _context = context;
        }


        // Se me muestran todas las cryptos 
        [HttpGet]
        public ActionResult<List<Product>> GetProducts() {
            return Ok(_context.Products); //200
        }

        [HttpPut] // Update to fav, not fav
        public ActionResult PutProducts(int id) {
            var existingProduct = _context.Products?.Where(x => x.Id == id).FirstOrDefault();
            if (existingProduct == null) {
                return Conflict("There is not any product with this Id"); // Status 409
            } else {
                existingProduct.Favourite = !existingProduct.Favourite;
                return Ok();
            }
        }

        [HttpPost] // Create
        public ActionResult PostProducts(Product product) {
            var existingProduct = _context.Products?.Where(x => x.Name == product.Name).FirstOrDefault();

            if (existingProduct != null) {
                return Conflict("There is an existing product with this name"); // status 409
            } else {
                var newProduct = new Product {
                    Id = product.Id,
                    Name = product.Name,
                    Value = product.Value,
                    Image = product.Image,
                    Url = product.Url,
                    CreationDate = product.CreationDate,
                    Favourite = product.Favourite,

                };
                _context.Products?.Add(newProduct);
                _context.SaveChanges();
                var resourceUrl = Request.Path.ToString() + "/" + newProduct.Name;
                return Created(resourceUrl, newProduct); // Status 201
            }
        }

        [HttpDelete]
        [Route("{Id}")]
        public ActionResult DeleteProduct(int Id) {
            var productToRemove = _context.Products?.Where(x => x.Id == Id).FirstOrDefault();
            if (productToRemove == null) {
                return NotFound("There is not a product saved with the selected id"); // status 404
            } else {
                _context.Products?.Remove(productToRemove);
                _context.SaveChanges();
                return Ok(); // status 200
            }
        }
    }
}
