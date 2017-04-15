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
            if(type == types.OKOnly)
                MessageBox((IntPtr)0, m, c, 0);
            else if(type == types.OKCancel)
                MessageBox((IntPtr)0, m, c, 1);
            else if(type == types.AbortRetryIgnore)
                MessageBox((IntPtr)0, m, c, 2);
            else if (type == types.YesNoCancel)
                MessageBox((IntPtr)0, m, c, 3);
            else if (type == types.YesNo)
                MessageBox((IntPtr)0, m, c, 4);
            else if (type == types.RetryCancel)
                MessageBox((IntPtr)0, m, c, 5);
            else if (type == types.CancelTryAgainContinue)
                MessageBox((IntPtr)0, m, c, 6);
            //MessageBox((IntPtr)0, m, c, Convert.ToInt32(Enum.GetValues(types)));
        }
    }
}
