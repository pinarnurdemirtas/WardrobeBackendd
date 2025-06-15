using System.ComponentModel.DataAnnotations.Schema;

namespace WardrobeBackendd.Model
{
	public class Clothes
	{
		public int Id { get; set; }

		public int User_id { get; set; }

		public string Name { get; set; }

		public string Image_url { get; set; }

		// Foreign Keys
		public int Category_id { get; set; }
		public int Season_id { get; set; }
		public int ColorId { get; set; }

		// Navigation Properties
		[ForeignKey("Category_id")]
		public Category Category { get; set; }

		[ForeignKey("Season_id")]
		public Season Season { get; set; }

		[ForeignKey("ColorId")]
		public Color Color { get; set; }
	}
}