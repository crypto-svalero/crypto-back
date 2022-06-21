using System;
namespace CryptoApi.Domain
{
	public class Fiat
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double Value { get; set; }
		public string Image { get; set; }
		public DateTime LastModified { get; set; }
		public bool Active { get; set; }
	}
}

