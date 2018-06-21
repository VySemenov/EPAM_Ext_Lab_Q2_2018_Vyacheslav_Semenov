namespace Task_1
{
    using System;
    using System.Collections.Generic;
    using Task_1.Models;

    /// <summary>
    /// Keeps a list of logs.
    /// Built on the principle of singleton.
    /// </summary>
    public class Repository
    {
        private static volatile Repository instance;
        private static object syncRoot = new object();

        private List<LogLine> logLines = new List<LogLine>();

        private Repository()
        {
        }

        public static Repository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Repository();
                        }
                    }
                }

                return instance;
            }
        }

        public List<LogLine> LogLines => this.logLines;
    }
}