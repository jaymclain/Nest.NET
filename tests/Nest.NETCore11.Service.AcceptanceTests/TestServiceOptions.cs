using System.IO;

namespace Rachio.NETCore11.Service.AcceptanceTests
{
    using Nest.NET.Service;

    public class TestServiceOptions : ServiceOptions
    {
        public TestServiceOptions()
        {
           AccessToken = File.ReadAllText("AccessToken.txt");
        }
    }
}
