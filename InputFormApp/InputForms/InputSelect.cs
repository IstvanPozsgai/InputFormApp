using System.Drawing;
using System.Windows.Forms;

namespace InputForms
{
    class InputSelect : InputField
    {
        public InputSelect(string text, string[] options, Control parent = null) : base(text, parent)
        {
            (input as ComboBox).Items.AddRange(options);
            (input as ComboBox).SelectedIndex = 0;
        }

        protected override Control CreateField()
        {
            ComboBox combo = new ComboBox();
            combo.Font = new Font("sans-serif", 12f);
            combo.Width = 300;
            combo.DropDownStyle = ComboBoxStyle.DropDownList;

            return combo;
        }
    }
}
