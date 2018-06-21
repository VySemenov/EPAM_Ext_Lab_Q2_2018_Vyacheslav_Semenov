using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_1.Models;

namespace Task_1
{
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