namespace DockerTestAma.Models
{
    /// <summary>
    /// Interface used as a contract for all types of containers.
    /// </summary>
    public interface IContainer
    {
        int? Id { get; set; }
        string Name { get; set; }
    }
}
