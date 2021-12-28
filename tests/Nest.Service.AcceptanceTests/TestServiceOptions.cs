using System.IO;
using Nest.NET.Service;

namespace Nest.Service.AcceptanceTests
{
    public class TestServiceOptions : ServiceOptions
    {
        public TestServiceOptions()
        {
           AccessToken = File.ReadAllText("AccessToken.txt");
        }
    }
}
