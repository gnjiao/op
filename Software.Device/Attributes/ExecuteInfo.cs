﻿using System;

namespace Software.Device
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ExecuteInfo : Attribute
    {
        public string Command { private set; get; }
        public string Describe { private set; get; }
        public string Example { private set; get; }

        public ExecuteInfo(string cmd, string des, string exp)
        {
            Command = cmd;
            Describe = des;
            Example = exp;
        }
    }
}
