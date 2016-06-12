using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerTestAma.Models
{
    public interface IContainer
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
