namespace WardrobeBackendd.Model;

public class Clothes
{
    public int Id { get; set; }
    public int User_id { get; set; }
    public string Name { get; set; }
    public string Image_url { get; set; }
    public int Category_id { get; set; }
    public int Season_id { get; set; }
    public int ColorId { get; set; }
}