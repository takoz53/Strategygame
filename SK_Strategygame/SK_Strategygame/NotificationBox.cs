using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SK_Strategygame
{
    class NotificationBox
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        public bool active = false;

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

        private string m;
        private string c;
        private types t;

        private void dummyThread ()
        {
            active = true;
            MessageBox((IntPtr)0, m, c, (int)t);
            active = false;
        }

        public void Notify(string m, string c, types type)
        {
            this.m = m;
            this.c = c;
            t = type;
            Thread a = new Thread(dummyThread);
            a.Start();
        }
    }
}
