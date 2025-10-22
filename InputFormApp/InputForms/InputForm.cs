using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace InputForms
{
    class InputForm : Panel
    {
        Dictionary<string, InputTextbox> fields;
        Button button;
        Action clickAction;

        public InputForm(Control parent)
        {
            Width = 100;
            Height = 100;
            BackColor = Color.LightGray;

            parent.Controls.Add(this);

            fields = new Dictionary<string, InputTextbox>();

            button = new Button();
            button.Text = "Send";
            button.Font = new Font("sans-serif", 11f, FontStyle.Bold);
            button.Width = 150;
            button.Height = 35;

            this.Controls.Add(button);

            button.Left = 25;
            button.Top = 25;

            button.Click += OnClick;
        }

        public string this[string name]
        {
            get { return GetValue(name); }
        }

        public InputForm Add(string name, InputTextbox field)
        {
            int y = 10 + (fields.Count * 40);

            fields.Add(name, field);
            field.Add(this);
            field.MoveTo(5, y);

            //Gomb új pozíció
            y += 80;
            button.Top = y;

            //Panel új magasság
            y += 50;
            Height = y;
            //Panel szélesség igazítása
            if (field.Width +field.Left+10 > Width)
            {
                Width = field.Width + field.Left +10;
            }
            AlignTextBoxesToLongestLabel(this);
            return this;
        }

        public string GetValue(string name)
        {
            if (fields.ContainsKey(name))
            {
                return fields[name].Value;
            }
            return null;
        }

        public InputForm MoveTo(int x, int y)
        {
            Left = x;
            Top = y;
            return this;
        }

        public InputForm SetButton(string text)
        {
            button.Text = text;
            return this;
        }

        public InputForm OnSubmit(Action action)
        {
            clickAction += action;
            return this;
        }

        void OnClick(object sender, EventArgs e)
        {
            if (clickAction != null)
            {
                string error = GetError();

                if (error != null)
                {
                    string msg = $"Hibásan kitöltve: {error}!";
                    MessageBox.Show(msg, "Hiba");
                }

                else clickAction();
            }
        }

        string GetError()
        {
            foreach (string name in fields.Keys)
            {
                if (!fields[name].IsValid()) return name;
            }
            return null;
        }


        private void AlignInputControls(Panel panel)
        {
            if (fields == null || !fields.Values.Any())
                return;

            // Csak azokat az elemeket vesszük figyelembe, ahol a Control TextBox típusú
            var textBoxPairs = fields.Values
                                      .Where(f => f.Control is InputTextbox && f.Label != null)
                                      .ToList();

            if (!textBoxPairs.Any())
                return;

            // 1. A leghosszabb Label szöveg szélességének kiszámítása
            int maxLabelWidth = 0;
            using (var g = panel.CreateGraphics())
            {
                foreach (var field in textBoxPairs)
                {
                    var size = g.MeasureString(field.Label.Text, field.Label.Font);
                    maxLabelWidth = Math.Max(maxLabelWidth, (int)size.Width);
                }
            }

            // 2. Margó hozzáadása (pl. 15 pixel)
            int alignedLeft = maxLabelWidth + 15;

            // 3. TextBox-ok igazítása
            foreach (var field in textBoxPairs)
            {
                field.Control.Left = alignedLeft;
                // Opcionális: Label-t is igazíthatod balra, ha nem AutoSize
                // field.Label.Left = 0;
            }
        }
    }
}
