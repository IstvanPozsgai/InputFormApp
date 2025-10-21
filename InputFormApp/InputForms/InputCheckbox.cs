using System.Drawing;
using System.Windows.Forms;

namespace InputForms
{
    class InputCheckbox : InputTextbox
    {
        public InputCheckbox(string text, bool Jelölt, Control parent = null) : base(text, text, parent)
        {
            (input as CheckBox).Checked = Jelölt;
            (input as CheckBox).Text = text;

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
