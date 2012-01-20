using System.Drawing;
using System.Windows.Forms;
using ScintillaNet;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    internal static class ScintillaExtensions
    {
        private static readonly Font BestScintillaFont =
            new Font ("Courier New", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);

        internal static void InitializeDefaults (this Scintilla scintilla)
        {
            var ni = scintilla.NativeInterface;
            scintilla.AcceptsTab = false;
            scintilla.CallTip.BackColor = Color.Empty;
            ni.SetCaretLineBack (Colour (0xdc, 0xff, 0xff));
            ni.SetCaretLineVisible (true);
            scintilla.Font = BestScintillaFont;
            scintilla.LongLines.EdgeColor = Color.FromArgb (192, 220, 192);
            scintilla.LongLines.EdgeColumn = 80 * 2;
            scintilla.LongLines.EdgeMode = EdgeMode.Line;
            scintilla.Scrolling.ScrollBars = ScrollBars.Vertical;
            scintilla.Selection.ForeColor = Color.Transparent;
            scintilla.Selection.ForeColorUnfocused = Color.Transparent;
            scintilla.Selection.BackColor = Color.FromArgb (224, 224, 224);
            scintilla.Selection.BackColorUnfocused = Color.FromArgb (240, 240, 240);
            scintilla.Whitespace.ForeColor = Color.Black;
            ni.SetMarginWidthN(1, 0);
            ni.SetWrapMode ((int) WrapMode.Word);
        }

        internal static void InitializeDiff(this Scintilla scintilla)
        {
            scintilla.LongLines.EdgeMode = EdgeMode.None;
            var ni = scintilla.NativeInterface;
            ni.SetReadOnly (true);

            ni.SetLexer ((int) Lexer.Diff);
            // Default
            scintilla.SetStyle(0, Color.FromArgb(0x00, 0x00, 0x00), BestScintillaFont);
            // Comment (part before "diff ..." or "--- ..." and , Only in ..., Binary file...)
            scintilla.SetStyle(1, Color.FromArgb(0x7f, 0x7f, 0x00), BestScintillaFont);
            // Command (diff ...)
            scintilla.SetStyle(2, Color.FromArgb(0x00, 0x7f, 0x00), BestScintillaFont);
            // Source file (--- ...) and Destination file (+++ ...)
            scintilla.SetStyle(3, Color.FromArgb(0x7f, 0x00, 0x00), BestScintillaFont);
            // Position setting (@@ ...)
            scintilla.SetStyle(4, Color.FromArgb(0x7f, 0x00, 0x7f), BestScintillaFont);
            // Line removal (-...)
            scintilla.SetStyle(5, Color.FromArgb(0x00, 0x7f, 0x7f), BestScintillaFont);
            // Line addition (+...)
            scintilla.SetStyle(6, Color.FromArgb(0x00, 0x00, 0x7f), BestScintillaFont);

            #region Configure folding
            ni.SetMarginWidthN(2, 20);
            ni.SetFoldFlags(16);
            #endregion
        }

        internal static void SetStyle(this Scintilla scintilla, int index, Color foreColor, Font font)
        {
            scintilla.Styles[index].ForeColor = foreColor;
            scintilla.Styles[index].Font = font;
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

        internal static int GetCurrentLineNumber(this Scintilla scintilla)
        {
            return scintilla.NativeInterface.LineFromPosition(scintilla.CurrentPos);
        }
    }
}
