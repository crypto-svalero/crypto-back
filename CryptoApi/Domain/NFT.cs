using System;
using System.ComponentModel.DataAnnotations;

namespace CryptoApi.Domain
{
	public class NFT
	{
        [Key]
        public int Id { get; set; }
		public string Name { get; set; }
		public int Value { get; set; }
		public Guid Code { get; set; }
		public string Image { get; set; }
		public DateTime CreationDate { get; set; }
		public bool Availability { get; set; }
	}
}

