using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        string logicaString = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control button in Controls)
            {
                if(button is Button && button.Text.Length == 2)
                {
                    button.Click += (q, w) =>
                    {
                        if(flowLayoutPanel1.Controls.Count == 0)
                        {
                            Button btn = new Button();
                            btn.Text = button.Text;
                            btn.Tag = 0;

                            flowLayoutPanel1.Controls.Add(btn);

                            btn.KeyDown += (i, u) =>
                            {
                                if (u.KeyCode == Keys.Delete)
                                {
                                    DeleteItems(btn);
                                    DeleteItems(btn);

                                }
                            };

                            btn.Click += (o, p) =>
                            {
                                SelectButton(btn);
                            };
                            
                        }
                        else
                        {
                            int index = flowLayoutPanel1.Controls.IndexOf(GetYellowButton());

                            PictureBox pb = new PictureBox();
                            pb.SizeMode = PictureBoxSizeMode.StretchImage;
                            pb.Image = Image.FromFile($"{AppDomain.CurrentDomain.BaseDirectory}seta.png");
                            pb.Tag = index;


                            Button btn = new Button();
                            btn.Text = $"{button.Text}: \n Dentro de {GetYellowButton().Text.Substring(0,2)}";
                            btn.Tag = index;
                            btn.Height = 60;

                            btn.Click += (o, p) =>
                            {
                                SelectButton(btn);
                            };
                            btn.KeyDown += (i, u) =>
                            {
                                if (u.KeyCode == Keys.Delete)
                                    DeleteItems(btn);
                            };

                            flowLayoutPanel1.Controls.Add(pb);
                            flowLayoutPanel1.Controls.Add(btn);
                        }
                        SetYellow();
                    };
                }
            }
        }


        public void DeleteItems(Button buttonSelected)
        {
            int buttonIndex = flowLayoutPanel1.Controls.IndexOf(buttonSelected);

            if(buttonIndex == 0)
            {
                flowLayoutPanel1.Controls.Clear();
            }
            else if(buttonIndex > 0)
            {
                var indexes = new List<int>();
                indexes.Add(buttonIndex);
                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    if((int)c.Tag >= buttonIndex)
                    {
                        indexes.Add(flowLayoutPanel1.Controls.IndexOf(c));
                    }
                    
                }

                for (int i = indexes[indexes.Count - 1]; i >= buttonIndex - 1; i--)
                {
                    flowLayoutPanel1.Controls.RemoveAt(i);
                }
                SetYellow();
            }

        }

        public void SelectButton(Button button)
        {
            Whiter();
            button.BackColor = Color.Yellow;
        }

        public void Whiter()
        {
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if(c is Button)
                {
                    c.BackColor = Color.White;
                }
            }
        }

        public void SetYellow()
        {

            Whiter();
            Button button = (Button)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1];

            button.BackColor = Color.Yellow;

        }

        public Button GetYellowButton()
        {
            foreach (Control item in flowLayoutPanel1.Controls)
            {
                if(item is Button && item.BackColor == Color.Yellow)
                {
                    return (Button)item;
                }
            }

            return new Button();
        }

        public void GetString()
        {
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if(c is Button)
                {
                    if (logicaString.Length == 0)
                    {
                        logicaString = $"{c.Text}()0";
                    }
                    else
                    {
                        int indexCursor = logicaString.LastIndexOf($"){c.Tag}") - 1;
                        //MessageBox.Show($"{logicaString.LastIndexOf($"){c.Tag}")}");
                        if (logicaString[indexCursor] == '(')
                        {
                            logicaString = logicaString.Replace($"){c.Tag}", $"{c.Text.Substring(0,2)}(){flowLayoutPanel1.Controls.IndexOf(c)}){c.Tag}");
                        }
                        else
                        {
                            logicaString = logicaString.Replace($"){c.Tag}", $";{c.Text.Substring(0,2)}(){flowLayoutPanel1.Controls.IndexOf(c)}){c.Tag}");
                        }

                    }
                }   
            }

        }


        public void DeleteNumbers()
        {
            GetString();

            foreach (char caractere in logicaString)
            {
                if(char.IsDigit(caractere))
                {
                    logicaString = logicaString.Replace(caractere.ToString(), "");
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DeleteNumbers();

            MessageBox.Show(logicaString);
            logicaString = "";
        }
    }
}
