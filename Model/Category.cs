namespace WardrobeBackendd.Model;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentID { get; set; }
	public string Gender { get; set; }

}