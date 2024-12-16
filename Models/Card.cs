using System.ComponentModel.DataAnnotations;

namespace shop_Web.Models
{

    public class Card
    {
        [Key]
        public int card_id { get; set; }
        public int cat_id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}