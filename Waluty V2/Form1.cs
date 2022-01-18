using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace Waluty_V2
{
    public partial class Form1 : Form
    {
        static HttpClient klient1 = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e) //przycisk PRZELICZ
        {
            try
            {
                string x1 = "http://api.nbp.pl/api/exchangerates/rates/a/" + textBox1.Text + "/?format=json";
                var a1 = await klient1.GetAsync(x1);
                string odp1 = await a1.Content.ReadAsStringAsync();

                JObject odp2 = JObject.Parse(odp1);
                var odp3 = odp2["rates"][0]["mid"]; //zagniezdzam sie w to co zwraca json, najpier wchodze w rates->0 jest jako pierwszy obiekt->mid to
                                                    //jest element w pierwszym obiekcie
            
                string a = odp3.ToString();
                float b = odp3.ToObject<float>();
                float amount = float.Parse(textBox3.Text);
                float m = b * amount;
                string m1 = m.ToString();

                textBox2.Text = a;
                textBox4.Text = m1;
            }

            catch (Newtonsoft.Json.JsonReaderException)
            {
                MessageBox.Show("Nieprawidłowa nazwa waluty");
            }

            catch (System.FormatException)
            {
                MessageBox.Show("Nieprawidłowa wartość waluty");
            }
           
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show("Nie masz dostępu do internetu");
            }
        }

        private void button2_Click(object sender, EventArgs e) //przycisk ZAPISZ
        {
            using (StreamWriter zPlik = new StreamWriter("KursWalut.txt", append: true)) 
            {
                zPlik.WriteLineAsync("Przeliczenie z daty i godziny: " + System.DateTime.Now + "\n" + "Waluta: " + textBox1.Text + "\nWartość w PLN: " + textBox2.Text + "\n" + "Ilość: " + textBox3.Text + "\nWartość w PLN: " + textBox4.Text + "\n \n");
                MessageBox.Show("Zapisano");
            }
        }

        private void button3_Click_1(object sender, EventArgs e) //przycisk WYJSCIE
        {
            this.Close();
        }
    }
}
