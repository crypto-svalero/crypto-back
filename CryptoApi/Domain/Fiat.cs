using System;
using System.ComponentModel.DataAnnotations;

namespace CryptoApi.Domain
{
	public class Fiat
	{
        [Key]
        public int Id { get; set; }
		public string Name { get; set; }
		public double Value { get; set; }
		public string Image { get; set; }
		public DateTime LastModified { get; set; }
		public bool Active { get; set; }
	}
}

