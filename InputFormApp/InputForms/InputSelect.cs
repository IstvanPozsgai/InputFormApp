using System.Drawing;
using System.Windows.Forms;

namespace InputForms
{
    class InputSelect : InputTextbox
    {
        readonly int MaxLength;
        public InputSelect(string text, string[] options,int maxLength=15, Control parent = null) : base(text, text, maxLength, parent)
        {
            MaxLength = maxLength;
            (input as ComboBox).Items.AddRange(options);
            (input as ComboBox).SelectedIndex = 0;
        }

        protected override Control CreateField()
        {
            ComboBox combo = new ComboBox();
            combo.Font = new Font("sans-serif", 12f);
            combo.Width = 300;
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.MaxLength =MaxLength;
            return combo;
        }
    }
}
