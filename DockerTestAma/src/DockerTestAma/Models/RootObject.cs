using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerTestAma.Models
{
    public class RootObject
    {
        public List<Container> containers { get; set; }
    }
}
