using WardrobeBackendd.Model;

public class Color
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Clothes> Clothes { get; set; }
}