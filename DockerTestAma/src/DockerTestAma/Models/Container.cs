using System;
using System.Collections.Generic;

namespace DockerTestAma.Models{

    public class Container{
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string State { get; set; }
    }
}
