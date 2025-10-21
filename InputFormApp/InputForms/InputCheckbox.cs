using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputForms
{
    class InputCheckbox : InputField
    {
        public InputCheckbox(string text, bool Jelölt, Control parent = null) : base(text, parent)
        {
            (input as CheckBox).Checked  = Jelölt;
            (input as CheckBox).Text  = text;

        }

        protected override Control CreateField()
        {
            CheckBox Jelölő = new CheckBox();
            Jelölő.Font = new Font("sans-serif", 12f);
            Jelölő.Width = 150;

            return Jelölő;
        }

    }
}
