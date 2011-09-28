using System.Drawing;
using System.Windows.Forms;
using ScintillaNet;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    internal static class ScintillaExtensions
    {
        internal static void InitializeDefaults (this Scintilla scintilla)
        {
            var ni = scintilla.NativeInterface;
            scintilla.AcceptsTab = false;
            scintilla.CallTip.BackColor = Color.Empty;
            ni.SetCaretLineBack (Colour (0xdc, 0xff, 0xff));
            ni.SetCaretLineVisible (true);
            scintilla.Font = new Font ("Courier New", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            scintilla.LongLines.EdgeColor = Color.FromArgb (192, 220, 192);
            scintilla.LongLines.EdgeColumn = 80 * 2;
            scintilla.LongLines.EdgeMode = EdgeMode.Line;
            scintilla.Scrolling.ScrollBars = ScrollBars.Vertical;
            scintilla.Selection.ForeColor = Color.Transparent;
            scintilla.Selection.ForeColorUnfocused = Color.Transparent;
            scintilla.Selection.BackColor = Color.FromArgb (224, 224, 224);
            scintilla.Selection.BackColorUnfocused = Color.FromArgb (240, 240, 240);
            scintilla.UseFont = true;
            scintilla.Whitespace.ForeColor = Color.Black;
            ni.SetWrapMode ((int) WrapMode.Word);
        }

        internal static void InitializeDiff(this Scintilla scintilla)
        {
            scintilla.LongLines.EdgeMode = EdgeMode.None;
            var ni = scintilla.NativeInterface;
            ni.SetReadOnly (true);

            ni.SetLexer ((int) Lexer.Diff);
            // Default
            ni.StyleSetFore (0, Colour (0x00, 0x00, 0x00));
            // Comment (part before "diff ..." or "--- ..." and , Only in ..., Binary file...)
            ni.StyleSetFore (1, Colour (0x7f, 0x7f, 0x00));
            // Command (diff ...)
            ni.StyleSetFore (2, Colour (0x00, 0x7f, 0x00));
            // Source file (--- ...) and Destination file (+++ ...)
            ni.StyleSetFore (3, Colour (0x7f, 0x00, 0x00));
            // Position setting (@@ ...)
            ni.StyleSetFore (4, Colour (0x7f, 0x00, 0x7f));
            // Line removal (-...)
            ni.StyleSetFore (5, Colour (0x00, 0x7f, 0x7f));
            // Line addition (+...)
            ni.StyleSetFore (6, Colour (0x00, 0x00, 0x7f));
        }

        internal static void SetReadOnlyText(this Scintilla scintilla, string text)
        {
            var ni = scintilla.NativeInterface;
            ni.SetReadOnly (false);
            scintilla.Text = text;
            ni.SetReadOnly (true);
        }

        internal static int Colour (byte red, byte green, byte blue)
        {
            return red | ( green << 8 ) | ( blue << 16 );
        }

    }
}
