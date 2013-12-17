using Raven.Client;

namespace ICE.RavenDB
{
    public interface IRavenDBConnector
    {
        IDocumentStore Store { get; }
    }
}