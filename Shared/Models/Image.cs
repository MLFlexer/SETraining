namespace SETraining.Shared.Models;
public class Image
{
    public Image(byte[] rawData)
    {
        RawData = rawData;
    }

    public int Id {get; set; }
    public byte[] RawData { get; set; }
}
