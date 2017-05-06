using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace carServer
{
    class Program
    {
        static TcpClient client;
        static BinaryReader biRea;
        static BinaryWriter biWri;
        static void Main(string[] args)
        {
            Console.WriteLine("Anwendung gestartet!");
            GetClient();
            Console.ReadLine();
        }

        static bool GetClient()
        {
            Console.WriteLine("Warte auf Client");
            TcpListener Listener = new TcpListener(System.Net.IPAddress.Any ,9001);
            Listener.Start();
            client = Listener.AcceptTcpClient();
            biRea = new BinaryReader(client.GetStream());
            biWri = new BinaryWriter(client.GetStream());
            Console.WriteLine("Client gefunden!");
            Random Generator = new Random();
            int Wert = Generator.Next(0, 10);
            biWri.Write((Int16)Wert);
            int Response = biRea.ReadInt16();
            if (Math.Floor((double)Wert / 2 + 2) == Response)
            {
                //Handshake-Abfrage erfolgreich, Anfrage akzeptieren
                Console.WriteLine("Abfrage erfolgreich, Client wird getestet");
                biWri.Write(true);
                if (!biRea.ReadBoolean())
                {
                    Console.WriteLine("Client hat Verbindung abgelehnt");
                    return false;
                }
                Console.WriteLine("Verbindung hergestellt");
                return true;
            }
            else
            {
                Console.WriteLine("Falsche Antwort auf Integer-Test. Verbindung fehlgeschlagen!");
                return false;
            }
        }
    }
}
