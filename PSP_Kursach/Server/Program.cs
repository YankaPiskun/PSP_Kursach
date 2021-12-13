using System.Collections.Generic;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Network.Server(new List<string> { "http://26.220.136.57:8080/",
                "http://26.220.136.57:8080/Start/",
                "http://26.220.136.57:8080/Bullet/",
                "http://26.220.136.57:8080/Prizes/" });
            server.Loop();
        }
    }
}
