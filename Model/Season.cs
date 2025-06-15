namespace WardrobeBackendd.Model;

public class Season
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Clothes> Clothes { get; set; }
}