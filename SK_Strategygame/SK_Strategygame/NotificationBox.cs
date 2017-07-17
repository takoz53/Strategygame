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

        private void notifyThread ()
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
            Thread a = new Thread(notifyThread);
            a.Start();
        }

        public void NotifyWood()
        {
            Thread a = new Thread(WoodThread);
        }
        public void NotifyStone()
        {
            Thread a = new Thread(StoneThread);
        }
        public void NotifyMoney()
        {
            Thread a = new Thread(MoneyThread);
        }
        public void NotifyFood()
        {
            Thread a = new Thread(FoodThread);
        }

        private void WoodThread()
        {
            MessageBox((IntPtr)0, "You don't have enough Wood!", "Not enough Wood!", (int)types.OKOnly);
        }
        private void StoneThread()
        {
            MessageBox((IntPtr)0, "You don't have enough Stone", "Not enough Stone!", (int)types.OKOnly);
        }
        private void MoneyThread()
        {
            MessageBox((IntPtr)0, "You don't have enough Money!", "Not enough Money!", (int)types.OKOnly);
        }
        private void FoodThread()
        {
            MessageBox((IntPtr)0, "You don't have enough Food!", "Not enough Food!", (int)types.OKOnly);
        }
    }
}
