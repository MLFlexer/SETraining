
namespace SETraining.Shared.Models;

public class Moderator
{
    public Moderator(string name)
    {
        Name = name;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Article>? Contents { get; set; } = null!;
}
