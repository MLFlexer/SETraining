using System;
namespace ProjTest2.Shared.Models
{
    public class RawVideo
    {

        private RawVideo() { }
        public RawVideo(byte[] rawVideo)
        {
            Video = rawVideo;
        }
        public int Id { get; set; }
        public byte[] Video { get; set; }
    }
    
}

