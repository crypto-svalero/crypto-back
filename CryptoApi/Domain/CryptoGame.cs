using System;
using System.ComponentModel.DataAnnotations;

namespace CryptoApi.Domain
{
	public class CryptoGame
	{
        [Key]
        public int Id { get; set; }
		public string Name { get; set; }
		public string Image { get; set; }
		public string Url { get; set; }
		public string CryptoUsed { get; set; }
		public DateTime CreationDate { get; set; }
		public bool Availability { get; set; }
	}
}

