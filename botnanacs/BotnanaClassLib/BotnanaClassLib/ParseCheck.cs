using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BotnanaClassLib
{
    public class ParseCheck
    {
        public delegate bool ParseFunc<T1, T2>(T1 a, out T2 b);

        // Try parse string to non-zero uint32.
        public static bool UIntTryParseNotZero(string str, out UInt32 n)
        {
            return (UInt32.TryParse(str, out n) && n != 0) ? true : false;
        }

        // Try parse hex string to uint32.
        public static bool UIntTryParseFromHex(string str, out UInt32 n)
        {
            try { n = Convert.ToUInt32(str, 16); return true; } catch { n = 0; return false; }
        }

        // Try parse string to non-zero int32.
        public static bool IntTryParseNotZero(string str, out Int32 n)
        {
            return (Int32.TryParse(str, out n) && n != 0) ? true : false;
        }

        // Try parse string to positive double.
        public static bool DoubleTryParsePos(string str, out double n)
        {
            return (Double.TryParse(str, out n) && n > 0.0) ? true : false;
        }

        // Try parse string to non-negative double.
        public static bool DoubleTryParseNotNeg(string str, out double n)
        {
            return (Double.TryParse(str, out n) && n >= 0.0) ? true : false;
        }

        // Check text of a TextBox by specified UInt32 parser, and render color of the text.
        public static void TextBoxCheckByParserUInt(object sender, ParseFunc<string, UInt32> parser)
        {
            TextBox tb = sender as TextBox;
            if (parser(tb.Text, out _)) { tb.ForeColor = Color.Black; } else { tb.ForeColor = Color.Red; }
        }

        // Check text of a TextBox by specified Int32 parser, and render color of the text.
        public static void TextBoxCheckByParserInt(object sender, ParseFunc<string, Int32> parser)
        {
            TextBox tb = sender as TextBox;
            if (parser(tb.Text, out _)) { tb.ForeColor = Color.Black; } else { tb.ForeColor = Color.Red; }
        }

        // Check text of a TextBox by specified Double parser, and render color of the text.
        public static void TextBoxCheckByParserDouble(object sender, ParseFunc<string, Double> parser)
        {
            TextBox tb = sender as TextBox;
            if (parser(tb.Text, out _)) { tb.ForeColor = Color.Black; } else { tb.ForeColor = Color.Red; }
        }
    }
}
