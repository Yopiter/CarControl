using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace carServer
{
    class Program
    {
        static TcpClient client;
		static TcpClient python;
        static BinaryReader biRea;
        static BinaryWriter biWri;
		static BinaryWriter pyWri;
		static int HauptR;
		static int SeitR;
		static Process py;

        static void Main(string[] args)
        {
            Console.WriteLine("Anwendung gestartet!");
			//StartPythonConsole (); Well no thanks
			GetClient ();
			while (client.Connected) {
				GetCommand ();
			}
            Console.ReadLine();
        }

		static void GetCommand ()
		{
			try{
			Int16 Comm = biRea.ReadInt16 ();
			switch (Comm) {
				case(-1): //Rückwärts
					HauptR--;
					break;
				case(1): //Vorwärts
					HauptR++;
					break;
				case(-2): //links
					SeitR--;
					break;
				case(2): //rechts
					SeitR++;
					break;
				case(0): //Do Tara-Stuff
					SeitR = HauptR = 0;
					break;
				case(-3): //Beenden
					EndApplication();
					break;
				default:
					Console.WriteLine ("Now That's Weird... Command Range +-2");
					//Rote Lampe blinken lassen
					break;
			}
			}
			catch(System.IO.IOException e) {
				python_send (0, 0);
				Console.WriteLine (e.Message +" bei "+ e.Source+", neuer Client wird gesucht. The Adventure continues!");
				GetClient ();
			}
			DoMotorStuff ();
		}

		static void DoMotorStuff ()
		{
			int leftM, rightM = 0;
			leftM = HauptR + (HauptR * SeitR);
			rightM = HauptR - (HauptR * SeitR);
			if (HauptR == 0) {
				leftM = SeitR;
				rightM = -SeitR;
			}
			python_send (leftM, rightM);
		}

		static void python_send (int leftM, int rightM)
		{
			byte[] left = IntToBytes (leftM);
			byte[] right = IntToBytes (rightM);
			python = new TcpClient ();
			python.Connect (System.Net.IPAddress.Loopback, 1338);
			pyWri = new BinaryWriter (python.GetStream ());
			pyWri.Write (left);
			pyWri.Write (right);
			python.Close ();
		}

        static bool GetClient()
        {
            Console.WriteLine("Warte auf Client");
            TcpListener Listener = new TcpListener(System.Net.IPAddress.Any ,9001);
            Listener.Start();
            client = Listener.AcceptTcpClient();
            biRea = new BinaryReader(client.GetStream());
            biWri = new BinaryWriter(client.GetStream());
			Listener.Stop ();
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

		static void StartPythonConsole () //deprecated since shitty not working shit
		{
			Console.WriteLine ("Hole Python-Client");
			//Prozess starten
			ProcessStartInfo ProcInfo = new ProcessStartInfo ();
			ProcInfo.WindowStyle = ProcessWindowStyle.Normal; //Später vielelicht als Hidden?
			ProcInfo.UseShellExecute = false;
			ProcInfo.FileName = "python";
			ProcInfo.Arguments= "/home/pi/car/CarControl.py";
			ProcInfo.Verb = "";
			ProcInfo.RedirectStandardOutput = true;
			py = new Process ();
			py.StartInfo = ProcInfo;
			py.Start ();
			Console.WriteLine ("Python-Skript gestartet!");
		}

		static byte[] IntToBytes (int MInt)
		{
			MInt = Math.Abs (MInt) > 1 ? MInt / Math.Abs (MInt) : MInt;
			string nachricht=MInt.ToString().Length==1? "+"+MInt.ToString(): MInt.ToString();
			return Encoding.ASCII.GetBytes (nachricht);
		}

		static void EndApplication ()
		{
			python.Close ();
			client.Close ();
			Environment.Exit (0);
		}
    }
}
