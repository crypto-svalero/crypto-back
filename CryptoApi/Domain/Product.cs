using System;
namespace CryptoApi.Domain
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Value { get; set; }
		public string Image { get; set; }
		public string Url { get; set; }
		public DateTime CreationDate { get; set; }
		public bool Favourite { get; set; }
	}
}

