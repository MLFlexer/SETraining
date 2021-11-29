namespace SETraining.Shared.Models;

public class Image
{
    public Image(string path)
    {
        Path = path;
    }

    public int Id {get; set; }
    public string Path { get; set; }
}
