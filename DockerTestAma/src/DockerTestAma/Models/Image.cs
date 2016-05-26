using System;

namespace DockerTestAma.Models
{
    public class Image
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Repository { get; set; }
        public string Tag { get; set; }
        public DateTime CreationDate { get; set; }
        public double Size { get; set; }
    }
}
