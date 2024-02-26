using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IgnitiChallenge.WPF.Helpers
{
    public static class DialogsHelper
    {
        public static void StartLaterThanEndErrorDialog()
        {
            MessageBox.Show("Start date should be earlier than end date.", "Error");
        }

        public static void StartDateMissingValueDialog()
        {
            MessageBox.Show("Missing start date value. Please select value.", "Error");
        }

        public static void EndDateMissingValueDialog()
        {
            MessageBox.Show("Missing end date value. Please select value.", "Error");
        }

        public static void DatesMissingValuesDialog()
        {
            MessageBox.Show("Missing start and end date values. Please select values.", "Error");
        }
    }
}
