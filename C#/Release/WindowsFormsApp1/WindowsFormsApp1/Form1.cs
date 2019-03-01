using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Net.Http.Headers;

namespace WindowsFormsApp1
{
    public partial class ProdistERP : Form
    {
        Token token;
        Account account;

        public ProdistERP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gettoken();
            if (token != null)
            {
                textBox1.Text = token.access_token;
                login();
            }
        }

        private void gettoken()
        {

            var uriRequest = new Uri("http://pc9/connect/token");

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", "apitester@test.nl"),
                new KeyValuePair<string, string>("password", "XS4Test@api")
            });

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var httpResponse = httpClient.PostAsync(uriRequest, content).Result;
                    var rawMessage =  httpResponse.Content.ReadAsStringAsync().Result;

                    token = ReadToObject<Token>(rawMessage);
                }
                catch
                { }
             }
        }

        private void login()
        {

            var uriRequest = new Uri("http://pc9/connect/api/login");

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", "f.epping1@nbdbiblion.nl"),
                new KeyValuePair<string, string>("password", "frank01")
            });

            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token.access_token);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //ACCEPT header


                    var httpResponse = httpClient.PostAsync(uriRequest, content).Result;
                    var rawMessage = httpResponse.Content.ReadAsStringAsync().Result;
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                        account = ReadToObject<Account>(rawMessage);
                    else
                    {
                        ErrorMessage message = ReadToObject<ErrorMessage>(rawMessage);
                        MessageBox.Show(message.Message);
                    }
                }
                catch(Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }


        public static T ReadToObject<T>(string json)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                return (T)ser.ReadObject(ms);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void ProdistERP_Load(object sender, EventArgs e)
        {

        }
    }
}
