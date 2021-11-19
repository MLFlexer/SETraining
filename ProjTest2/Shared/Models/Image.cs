namespace ProjTest2.Shared.Models
{
    public class Image
    {
        public Image(int id, byte[] rawImage)
        {
            Id = id;
            RawImage = rawImage;
        }

        public int Id {get; set; }
        public byte[] RawImage { get; set; }
    }
}