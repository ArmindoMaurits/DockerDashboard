using System;

namespace DockerTestAma.Models {
    public class Image {
        public string id { get; set; }
        public string name { get; set; }
        public string repository { get; set; }
        public string tag { get; set; }
        public DateTime creationDate { get; set; }
        public double size { get; set; }
    }
}
