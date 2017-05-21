using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using MjpegProcessor;

namespace carControlClient
{
    public partial class GUI : Form
    {
        TcpClient car;
        BinaryReader biRea;
        BinaryWriter biWri;
        bool right;
        bool left;
        bool up;
        bool down;
        MjpegProcessor.MjpegDecoder ImgGetter;

        public GUI()
        {
            InitializeComponent();
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            if (!Connect()) lbStatus.Text="Verbindungsproblem. IP überprüfen!";
        }

        bool Connect()
        {
            car = new TcpClient();
            IPAddress Adresse;
            if (!IPAddress.TryParse(textBox1.Text,out Adresse))
            {
                MessageBox.Show("Keine gültige IP-Adresse!");
                return false;
            }
            String URL = "http://"+Adresse.ToString() + ":8090/test.mjpg";
            car.Connect(Adresse, 9001);
            Stream clst=car.GetStream();
            biRea = new BinaryReader(clst);
            biWri = new BinaryWriter(clst);
            int Wert = biRea.ReadInt16();
            biWri.Write((Int16)Math.Floor((decimal)Wert / 2 + 2));
            if (biRea.ReadBoolean())
            {
                //Annehmen der Verbindung. Bei Verdacht (whyever XD) ein false senden!
                biWri.Write(true);
                lbStatus.Text = "Verbindung hergestellt. Viel Spaß! :D";
                ImgGetter = new MjpegProcessor.MjpegDecoder();
                ImgGetter.FrameReady += mjpeg_FrameReady;
                ImgGetter.ParseStream(new Uri("http://" + textBox1.Text + ":8090/test.mjpg"));
                return true;
            }
            return false;
        }

        private void GUI_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case(Keys.Left):
                    if (left) break;
                    lbLeft.BackColor = Color.Green;
                    biWri.Write((Int16)(- 2));
                    left = true;
                    break;
                case(Keys.Right):
                    if (right) break;
                    lbRight.BackColor = Color.Green;
                    biWri.Write((Int16)2);
                    right = true;
                    break;
                case(Keys.Up):
                    if (up) break;
                    lbUp.BackColor = Color.Green;
                    biWri.Write((Int16)1);
                    up = true;
                    break;
                case(Keys.Down):
                    if (down) break;
                    lbDown.BackColor = Color.Green;
                    biWri.Write((Int16)(-1));
                    down = true;
                    break;
                default: 
                    lbStatus.Text = "Nicht belegte Taste: "+e.KeyCode.ToString();
                    break;
            }
            e.Handled = true;
        }

        private void GUI_KeyUp(object sender, KeyEventArgs e)
        {
           
            switch (e.KeyCode)
            {
                case (Keys.Left):
                    lbLeft.BackColor = Color.Transparent;
                    biWri.Write((Int16)(2));
                    left = false;
                    break;
                case (Keys.Right):
                    lbRight.BackColor = Color.Transparent;
                    biWri.Write((Int16)(-2));
                    right = false;
                    break;
                case (Keys.Up):
                    lbUp.BackColor = Color.Transparent;
                    biWri.Write((Int16)(-1));
                    up = false;
                    break;
                case (Keys.Down):
                    lbDown.BackColor = Color.Transparent;
                    biWri.Write((Int16)(1));
                    down = false;
                    break;
                case(Keys.Enter):
                    lbStatus.Text = "Reset/Tare-Funktion aktiviert";
                    biWri.Write((Int16)0);
                    break;
                case (Keys.Delete):
                    lbStatus.Text = "Autoanwendung beendet";
                    biWri.Write((Int16)(-3));
                    break;
                default:
                    lbStatus.Text = "Nicht belegte Taste: " + e.KeyCode.ToString();
                    break;
            }
            e.Handled = true;
        }

        private void mjpeg_FrameReady(object sender, FrameReadyEventArgs e)
        {
            pB1.Image = e.Bitmap;
        }
    }
}
