namespace DockerTestAma.Models
{
    using System;

    public class DockerContainer : IContainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string State { get; set; }
    }
}
