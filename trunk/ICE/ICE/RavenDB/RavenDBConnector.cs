using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Embedded;

namespace ICE.RavenDB
{
    public class RavenDBConnector : IRavenDBConnector
    {

        private readonly IDocumentStore _store;
        public IDocumentStore Store { get { return _store; } }
        public RavenDBConnector()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            _store = new EmbeddableDocumentStore() { DataDirectory = currentDirectory + "/ICEDB", UseEmbeddedHttpServer = true};
            _store.Initialize();

        }
    }
}
