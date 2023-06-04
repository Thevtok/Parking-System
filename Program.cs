


namespace ParkingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            RunServer();
        }

        static void RunServer()
        {
            RunServer server = new RunServer();
            server.Start();
        }
    }
}
