namespace WardrobeBackendd.Model;

public class CreateCombineDto
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public List<int> ClothIds { get; set; }
}
