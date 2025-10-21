using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InputForms
{
    class InputForm : Panel
    {
        Dictionary<string, InputField> fields;
        Button button;
        Action clickAction;

        public InputForm(Control parent)
        {
            Width = 400;
            Height = 100;
            BackColor = Color.LightGray;

            parent.Controls.Add(this);

            fields = new Dictionary<string, InputField>();

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

        public InputForm Add(string name, InputField field)
        {
            int y = 15 + (fields.Count * 70);

            fields.Add(name, field);
            field.Add(this);
            field.MoveTo(25, y);

            y += 80;
            button.Top = y;

            y += 50;
            Height = y;

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
    }
}
