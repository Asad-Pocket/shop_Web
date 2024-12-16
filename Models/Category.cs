using System.ComponentModel.DataAnnotations;

namespace shop_Web.Models
{
    public class Category
    {
        [Key]
        public int cat_id { get; set; }
        public string cat_name { get; set; }
    }
}
