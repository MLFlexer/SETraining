namespace ProjTest2.Shared.Models
{
    public class Image
    {

        
        public Image(byte[] rawImage)
        {
            RawImage = rawImage;
        }

        public int Id {get; set; }
        public byte[] RawImage { get; set; }
    }
}