using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace Ultimate_Predictor
{
   
    public partial class Form1 : Form
    {
        private const string APP_NAME = "ULTIMATE_PREDICTIONS";
        private readonly string PATH = $"{Environment.CurrentDirectory}//PredictionConfig.json";
        private string[] _predictions;
        private Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private async void bPredict_Click(object sender, EventArgs e)
        {
            bPredict.Enabled = false;
            await Task.Run(() =>
            {
                for (int i = 1; i <= 100; ++i)
                {
                    this.Invoke(new Action(() =>
                    {
                        UpdateProgressBar(i);
                        Text = $"{i}%";
                    }));

                    Thread.Sleep(20);
                }

            });
            int index = rand.Next(_predictions.Length);
            MessageBox.Show(_predictions[index]);
            bPredict.Enabled = true;    
            progressBar1.Value = 0;
            Text = APP_NAME;
        }
        private void UpdateProgressBar(int i)
        {
            if(i == progressBar1.Maximum)
            {
                progressBar1.Maximum = i + 1;
                progressBar1.Value = i + 1;
                progressBar1.Maximum = i;
            }
            else
            {
                progressBar1.Value = i + 1;
            }
            progressBar1.Value = i;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = APP_NAME;
            try
            {
                string data = File.ReadAllText(PATH);
                _predictions = JsonConvert.DeserializeObject <string[]> (data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if(_predictions == null)
                {
                    Close();
                }
                else if(_predictions.Length == 0)
                {
                    MessageBox.Show("Predictions are over!");
                    Close();
                }
            }
        }
    }
}
