using System;
using System.Collections.Generic;
using Task_1.Models;

namespace Task_1
{
    /// <summary>
    /// Keeps a list of logs.
    /// Built on the principle of singleton.
    /// </summary>
    public class Repository
    {
        private static volatile Repository instance;
        private static object syncRoot = new Object();
        private Repository() { }

        private List<LogLine> logLines = new List<LogLine>();

        public List<LogLine> LogLines => this.logLines;

        public static Repository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Repository();
                    }
                }
                return instance;
            }
        }
    }
}