using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace InputForms
{
    class InputField
    {
  readonly       Label label;
        protected Control input;
        string rule;
   readonly      int Maximum;

        public InputField(string text, Control parent = null,int maximum=15) 
        {
            Maximum = maximum;
            label = new Label
            {
                Text = text,
                Font = new Font("sans-serif", 10f),
                AutoSize = true
            };

            input = CreateField();

            if (parent != null) Add(parent);
     
        }

        public string Value
        {
            set { input.Text = value; }
            get { return input.Text;}
        }

        public InputField Add (Control parent)
        {
            parent.Controls.Add(label);
            parent.Controls.Add(input);
            return this;
        }

        public InputField MoveTo(int x, int y)
        {
            label.Top = y;
            input.Top = y;
            label.Left = x;
            input.Left = label.Left + label.Width + 10;
            return this;
        }

        public InputField AddRule(string rule)
        {
            this.rule = rule;
            return this;
        }



        public bool IsValid()
        {
            if (rule == null) return true;

            return Regex.IsMatch(Value, "^" + rule + "$");
        }

        protected virtual Control CreateField()
        {
            TextBox textBox = new TextBox();
            textBox.Font = new Font("sans-serif", 12f);
            textBox.Width = 250;
            textBox.MaxLength = Maximum;
            return textBox;
        }
    }
}
