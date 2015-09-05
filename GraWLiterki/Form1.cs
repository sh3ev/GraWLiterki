using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraWLiterki
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        Stats stats = new Stats();
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Dodaje losową literę do kontrolki  ListBox
            listBox1.Items.Add((Keys) random.Next(65, 90));
            listBox1.ForeColor = ColorGenerator();
            if (listBox1.Items.Count > 7)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Koniec gry");
                timer1.Stop();
                DialogResult dialogResult = MessageBox.Show("Czy chcesz rozpocząć nową grę ?", "Nowa gra", MessageBoxButtons.YesNo);
                if (dialogResult==DialogResult.Yes)
                {
                   NewGame();
                }
                else
                {
                    
                }

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Jeżeli klawisz literki dostępnej w kontrolce
            //ListBox został naciśnięty, to usuwamy ją i zwiększamy
            //tempo gry

            if (listBox1.Items.Contains(e.KeyCode))
            {
                listBox1.Items.Remove(e.KeyCode);
                listBox1.Refresh();
                if (timer1.Interval > 400)
                    timer1.Interval -= 10;
                if (timer1.Interval > 250)
                    timer1.Interval -= 7;
                if (timer1.Interval > 100)
                    timer1.Interval -= 2;
                difficultyProgressBar.Value = 800 - timer1.Interval;
                stats.Update(true);
                
                //gracz nacisnął prawidłowy klawisz, wiec statystyki gry aktualizuja sie,
                //wywołując metode Update() i przekazując do niej wartość true  stats.Update(true)
            }
            else
            {
                //gracz nacisnął niewłaściwy klawisz
                stats.Update(false);
            }
            //aktualizacja etykiet na pasku stanu
            correctLabel.Text = "Prawidłowych: " + stats.Correct;
            missedLabel.Text = "Błędów: " + stats.Missed;
            totalLabel.Text = "Wszystkich: " + stats.Total;
            accuracyLabel.Text = "Dokładność: " + stats.Accuracy + "%";

        }

        private void NewGame()
        {
            stats.RestartStatistic();
            correctLabel.Text = "Prawidłowych: " + stats.Correct;
            missedLabel.Text = "Błędów: " + stats.Missed;
            totalLabel.Text = "Wszystkich: " + stats.Total;
            accuracyLabel.Text = "Dokładność: " + stats.Accuracy + "%";
            listBox1.Items.Clear();
            listBox1.Refresh();
            difficultyProgressBar.Value = 0;
            timer1.Interval = 800;
            timer1.Start();
        }

        private Color ColorGenerator()
        {
            Random randomGen = new Random();
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomColorName = names[randomGen.Next(names.Length)];
            Color randomColor = Color.FromKnownColor(randomColorName);
            return randomColor;
        }
    }
}
