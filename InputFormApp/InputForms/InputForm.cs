using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace InputForms
{
    class InputForm : Panel
    {
        Dictionary<string, InputTextbox> fields;
        Button button;
        Action clickAction;
        int LeftMax = 0;

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

            FieldIgazítás();
            return this;
        }

        private void FieldIgazítás()
        {
            int rightmostEdge = this.Controls.OfType<Label>()
                .Max(label => label.Left + label.Width);

            if (rightmostEdge > LeftMax)
            {
                LeftMax = rightmostEdge;

                this.SuspendLayout();
                var controlsToAlign = this.Controls.OfType<Control>()
                    .Where(c => c is TextBox || c is ComboBox || c is CheckBox || c is DateTimePicker);

                foreach (Control control in controlsToAlign)
                {
                    control.Left = LeftMax + 10;
                }

                this.ResumeLayout();
            }
            int maxFieldRightEdge = this.Controls.OfType<Control>()
                                    .Where(c => c is TextBox || c is ComboBox || c is CheckBox || c is DateTimePicker)
                                    .Max(control => control.Left + control.Width);
            //Panel szélesség igazítása
            if (maxFieldRightEdge + 10 > Width)
            {
                Width = maxFieldRightEdge + 10;
                button.Left = (Width - button.Width) / 2;
            }
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

    }
}
