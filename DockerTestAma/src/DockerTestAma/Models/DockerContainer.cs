namespace DockerTestAma.Models
{
    using System;
    using System.Diagnostics.Contracts;
    //Design by Contract 

    /// <summary>
    /// DockerContainer object, which holds Docker Container information.
    /// Implementing the ID and name properties from IContainer interface.
    /// </summary>
    public class DockerContainer : IContainer
    {
        //ID is a nullable type, shorthand for Nullable<int>. So we can put it on our Contract.
        private int? id;
        private string name;

        public int? Id {
            get
            {
                return id;
            }
            set
            {
                Contract.Requires((id == null || id > 0), "ID cannot be null or 0");
                id = value;
            }
        }

        public string Name {
            get
            {
                return name;
            }
            set
            {
                Contract.Requires(!string.IsNullOrEmpty(Name), "Container Name cannot be Null");
                name = value;
            }
        }

        public DateTime CreationDate { get; set; }
        public string State { get; set; }
    }
}
