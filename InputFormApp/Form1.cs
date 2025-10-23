using InputForms;
using System;
using System.Windows.Forms;

namespace InputFormApp
{
    public partial class Form1 : Form
    {
        InputForm form;

        void Start()
        {
            form = new InputForm(this);
            form.Add("Vezetéknév", (new InputTextbox("Vezetéknév", "Kiss", 10)).AddRule(null))
                .Add("Keresztnév", (new InputTextbox("Keresztnév", "Aladár")).AddRule(null))
                .Add("Nem", new InputSelect("Nem", new string[] { "Férfi", "Nő", "Egyéb" }))
                .Add("Dátum", new InputDate("Születési dátum", new DateTime(2020, 12, 25)))
                .Add("Törölt", new InputCheckbox("Törölt", true))
                .Add("Keresztné", (new InputTextbox("Keresztné", "Aladár")).AddRule(null))
                .MoveTo(10, 10)
                .FieldIgazítás()
                .SetButton("Elküld")
                .OnSubmit(() =>
                {
                    string name = form["Vezetéknév"] + " " + form["Keresztnév"];
                    string Nem = form["Nem"];
                    string Dátum = form["Dátum"];
                    string törölt = form["Törölt"];
                    MessageBox.Show($"{name} ({Nem})\n {Dátum}\n {törölt}");
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
