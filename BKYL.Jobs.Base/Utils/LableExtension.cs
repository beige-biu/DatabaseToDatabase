using System;
using System.Windows.Forms;

namespace BKYL.Jobs.Base.Plugin
{
    public static class LableExtension
    {
        delegate void SetTextCallBack(Label rtBox, string text);
        public static void TextByAsync(this Label rtBox, string text)
        {
            try
            {
                if (rtBox.IsDisposed) return;
                if (rtBox.InvokeRequired)
                {
                    SetTextCallBack stcb = new SetTextCallBack(AppendTextColorful);
                    rtBox.Invoke(stcb, new object[] { rtBox, text});
                }
                else
                {
                    rtBox.Text = text;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        static void AppendTextColorful(this Label rtBox, string text)
        {
            rtBox.Text = text;
        }
    }
}
