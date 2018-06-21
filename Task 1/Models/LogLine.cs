using System;

namespace Task_1.Models
{
    /// <summary>
    /// It is a log model.
    /// </summary>
    public class LogLine
    {
        public DateTime Time { get; set; }
        public string Arg1 { get; set; }
        public string Arg2 { get; set; }
        public string Operation { get; set; }
        public string Result { get; set; }

        public LogLine(DateTime time, string arg1, string arg2, string op, string result)
        {
            Time = time;
            Arg1 = arg1;
            Arg2 = arg2;
            Operation = op;
            Result = result;
        }

        public override string ToString()
        {
            return Time.ToString() + " " + Arg1 + " " + Operation + " " + Arg2 + " = " + Result;
        }
    }
}