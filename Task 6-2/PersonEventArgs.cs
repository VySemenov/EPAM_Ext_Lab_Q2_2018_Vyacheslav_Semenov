namespace Task_6_2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PersonEventArgs : EventArgs
    {
        public PersonEventArgs(DateTime time)
        {
            this.Time = time;
        }

        public DateTime Time { get; }
    }
}
