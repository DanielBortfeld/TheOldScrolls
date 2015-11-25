using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game
{
    public class TestingDeviceEventArgs : EventArgs
    {
        private TestingDeviceActions action;
        private object param;

        public TestingDeviceActions Action
        {
            get { return action; }
            set { action = value; }
        }

        public object Param
        {
            get { return param; }
            set { param = value; }
        }
    }
}
