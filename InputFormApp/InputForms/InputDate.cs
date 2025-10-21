using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace InputForms
{
    class InputDate : InputField
    {
        public InputDate(string   text, DateTime Dátum, Control parent = null) : base(text, parent)
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
