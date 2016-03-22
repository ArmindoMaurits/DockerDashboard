using System;

namespace DockerTestAma.Models
{
    public class Container
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime creationDate { get; set; }
        public enum state {New = 1, Created, Started, Error, Deleted, Crashed}
    }
}
