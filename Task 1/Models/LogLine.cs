namespace Task_1.Models
{
    using System;

    /// <summary>
    /// It is a log model.
    /// </summary>
    public class LogLine
    {
        public LogLine(DateTime time, string arg1, string arg2, string op, string result)
        {
            this.Time = time;
            this.Arg1 = arg1;
            this.Arg2 = arg2;
            this.Operation = op;
            this.Result = result;
        }

        public DateTime Time { get; set; }

        public string Arg1 { get; set; }

        public string Arg2 { get; set; }

        public string Operation { get; set; }

        public string Result { get; set; }

        public override string ToString()
        {
            return this.Time.ToString() + 
                " " + this.Arg1 + 
                " " + this.Operation + 
                " " + this.Arg2 + 
                " = " + this.Result;
        }
    }
}