using InputForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputFormApp
{
    public partial class Form1 : Form
    {
        InputForm form;

        void Start()
        {
            form = new InputForm(this);
            form.Add("Vezetéknév", (new InputField("Vezetéknév")).AddRule("[a-z]+"))
                .Add("Keresztnév", (new InputField("Keresztnév")).AddRule("[a-z]+"))
                .Add("Nem", new InputSelect("Nem", new string[] { "Férfi", "Nő", "Egyéb" }))
                .Add("Dátum", new InputDate("Születési dátum", new DateTime(1900, 1, 1)))
                .Add("Törölt,", new InputCheckbox("Törölt", false))
                .MoveTo(10, 10)
                .SetButton("Elküld")
                .OnSubmit(() =>
                {
                    string name = form["Vezetéknév"] + " " + form["Keresztnév"];
                    string sex = form["Nem"];

                    MessageBox.Show($"{name} ({sex})");
                });
        }

        public Form1()
        {
            InitializeComponent();
            Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
