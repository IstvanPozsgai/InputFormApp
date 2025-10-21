using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace InputForms
{
    class InputTextbox
    {
        readonly Label label;
        protected Control input;
        string rule;
        readonly string Tartalom;
        readonly int MaxLength;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LabelSzöveg">Label felirat</param>
        /// <param name="parent"></param>
        /// <param name="MaxLength"></param>
        public InputTextbox(string LabelSzöveg, string tartalom, Control parent = null, int maxLength = 15)
        {
            MaxLength = maxLength;
            Tartalom = tartalom;
            label = new Label
            {
                Text = LabelSzöveg,
                Font = new Font("sans-serif", 12f),
                AutoSize = true
            };

            input = CreateField();

            if (parent != null) Add(parent);
        }

        public string Value
        {
            set { input.Text = value; }
            get { return input.Text; }
        }

        public InputTextbox Add(Control parent)
        {
            parent.Controls.Add(label);
            parent.Controls.Add(input);
            return this;
        }

        public InputTextbox MoveTo(int x, int y)
        {
            label.Top = y;
            input.Top = y;
            label.Left = x;
            input.Left = label.Left + label.Width + 10;
            return this;
        }

        public InputTextbox AddRule(string rule)
        {
            this.rule = rule;
            return this;
        }

        public bool IsValid()
        {
            string magyar = @"[aábcdeéfghiíjklmnoóöőpqrstuúüűvwxyzAÁBCDEÉFGHIÍJKLMNOÓÖŐPQRSTUÚÜŰVWXYZ ]";
            if (rule == null) rule = magyar;
            return Regex.IsMatch(Value, "^" + rule + "+$");
        }

        protected virtual Control CreateField()
        {
            TextBox textBox = new TextBox();
            textBox.Font = new Font("sans-serif", 12f);
            textBox.Width = Szélesség();
            textBox.MaxLength = MaxLength;
            textBox.Text = Tartalom;
            return textBox;
        }

        public int Szélesség()
        {
            int válasz = 10;
            using (Font font = new Font("Microsoft Sans Serif", 12f))
            {
                string worstCase = new string('W', MaxLength);
                Size textSize = TextRenderer.MeasureText(
                    worstCase,
                    font,
                    Size.Empty,
                    TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix
                );

                // Margó hozzáadása (a kurzor, belső padding miatt)
                válasz = textSize.Width + 8;
            }
            return válasz;
        }
    }
}
