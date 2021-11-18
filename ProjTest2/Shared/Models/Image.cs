namespace ProjTest2.Shared.Models
{
    public class Image
    {
        public Image(byte[] rawImg)
        {
            rawImg = RawImage;
        }

        public byte[] RawImage { get; set; }
    }
}