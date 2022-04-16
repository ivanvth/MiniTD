using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD
{
    class GameEventArgs : EventArgs
    {
        GameEventType type;
        List<object> objectArgs = new List<object>();
        List<string> stringArgs = new List<string>();
        public GameEventArgs(GameEventType type)
        {
            this.type = type;
        }

        public void AddStringArg(string s)
        {
            stringArgs.Add(s);
        }

        public void AddObjectArg(object o)
        {
            objectArgs.Add(o);
        }
    }
}
