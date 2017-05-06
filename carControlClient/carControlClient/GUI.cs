using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace carControlClient
{
    public partial class GUI : Form
    {
        TcpClient car;
        StreamReader strRea;
        StreamWriter strWri;
        BinaryReader biRea;
        BinaryWriter biWri;
        
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
            car.Connect(Adresse, 9001);
            Stream clst=car.GetStream();
            strWri = new StreamWriter(clst);
            strRea = new StreamReader(clst);
            biRea = new BinaryReader(clst);
            biWri = new BinaryWriter(clst);
            int Wert = biRea.ReadInt16();
            biWri.Write((Int16)Math.Floor((decimal)Wert / 2 + 2));
            if (biRea.ReadBoolean())
            {
                //Annehmen der Verbindung. Bei Verdacht (whyever XD) ein false senden!
                biWri.Write(true);
                lbStatus.Text = "Verbindung hergestellt. Viel Spaß! :D";
                return true;
            }
            return false;
        }

        private void GUI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) lbRight.BackColor = Color.Green;
            if (e.KeyCode == Keys.Left) lbLeft.BackColor = Color.Green;
            if (e.KeyCode == Keys.Up) lbUp.BackColor = Color.Green;
            if (e.KeyCode == Keys.Down) lbDown.BackColor = Color.Green;
            e.Handled = true;
            switch (e.KeyCode)
            {
                    //Hier mal die fallbehandlung rein sobald ich Internet habe...
                default: break;
            }
        }

        void MakeMove(int Richtung, int Wichtung)
        {

        }

        private void GUI_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) { lbRight.BackColor = Color.Transparent; }
            if (e.KeyCode == Keys.Left) { lbLeft.BackColor = Color.Transparent; }
            if (e.KeyCode == Keys.Up) { lbUp.BackColor = Color.Transparent; }
            if (e.KeyCode == Keys.Down) { lbDown.BackColor = Color.Transparent; }
            e.Handled = true;
            switch (e.KeyCode)
            {
                //Hier mal die fallbehandlung rein sobald ich Internet habe...
                default: break;
            }
        }
    }
}
