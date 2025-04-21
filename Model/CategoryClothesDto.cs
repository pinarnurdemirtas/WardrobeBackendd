namespace WardrobeBackendd.Model;

public class CategoryClothesDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public List<Clothes> Clothes { get; set; }
}
