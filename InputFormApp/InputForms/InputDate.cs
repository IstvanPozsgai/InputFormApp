using System;
using System.Drawing;
using System.Windows.Forms;

namespace InputForms
{
    class InputDate : InputTextbox
    {
        public InputDate(string text, DateTime Dátum, int maxLength = 15, Control parent = null) : base(text, text, maxLength, parent)
        {
            (input as DateTimePicker).Value = Dátum;
            (input as DateTimePicker).Format = DateTimePickerFormat.Short;

        }

        protected override Control CreateField()
        {
            DateTimePicker Dátumvezérlő = new DateTimePicker();
            Dátumvezérlő.Font = new Font("sans-serif", 12f);
            Dátumvezérlő.Width = 150;

            return Dátumvezérlő;
        }

    }
}
