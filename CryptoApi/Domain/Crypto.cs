using System;
using System.ComponentModel.DataAnnotations;

namespace CryptoApi.Domain
{
	public class Crypto
	{
        [Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public int CurrentValue { get; set; }
		public int LastValue { get; set; }
		public string Image { get; set; }
		public DateTime LastModified { get; set; }
		public bool Favourite { get; set; }
	}
}

