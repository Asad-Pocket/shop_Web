using System.ComponentModel.DataAnnotations;

public class Item
{
    [Key]
    public int item_id { get; set; }
    public string item_name { get; set; }
    public string item_des { get; set; }
    public string brand { get; set; }
    public int unit_price { get; set; }
    public int qty { get; set; }
    public int cat_id { get; set; }
}
