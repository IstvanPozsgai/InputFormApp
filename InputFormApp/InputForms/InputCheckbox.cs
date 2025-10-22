using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InputForms
{
    class InputCheckbox : InputTextbox
    {
        public InputCheckbox(string text, bool Jelölt, int maxLength = 15, Control parent = null) : base(text, text, maxLength, parent)
        {
            (input as CheckBox).Checked = Jelölt;
       //     (input as CheckBox).Text = text;

        }

        protected override Control CreateField()
        {
            CheckBox Jelölő = new CheckBox();
            Jelölő.Font = new Font("sans-serif", 12f);
            Jelölő.Width = Szélesség();
            return Jelölő;
        }

    }
}
