using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SK_Strategygame
{
    class NotificationBox
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        public NotificationBox()
        {

        }
        public enum types
        {
            OKOnly,
            OKCancel,
            AbortRetryIgnore,
            YesNoCancel,
            YesNo,
            RetryCancel,
            CancelTryAgainContinue,
        }

        public void Notify(string m, string c, types type)
        {
            MessageBox((IntPtr)0, m, c, (int)type);
            //MessageBox((IntPtr)0, m, c, Convert.ToInt32(Enum.GetValues(types)));
        }
    }
}
