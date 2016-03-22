using System;

namespace DockerTestAma.Models
{
    public class Image
    {
        public int id { get; set; }
        public string name { get; set; }
        public string repository { get; set; }
        public string tag { get; set; }
        public DateTime creationDate { get; set; }
        public int size { get; set; }
    }
}
