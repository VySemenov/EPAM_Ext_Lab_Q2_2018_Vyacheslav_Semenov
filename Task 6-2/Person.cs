namespace Task_6_2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Person
    {
        public delegate void PersonStateHandler(Person person, PersonEventArgs e);

        public event PersonStateHandler Came;

        public event PersonStateHandler Gone;

        public string Name { get; set; }

        public void Hello(Person person, PersonEventArgs e)
        {
            DateTime time = e.Time;

            if (time.Hour < 12)
            {
                Console.WriteLine(
                    "\"{0}, {1}!\", - {2} {3}", 
                    Resource.GoodMorning, 
                    person.Name, 
                    Resource.Said, 
                    this.Name);
            }
            else
            if (time.Hour < 17)
            {
                Console.WriteLine(
                    "\"{0}, {1}!\", - {2} {3}",
                    Resource.GoodDay,
                    person.Name,
                    Resource.Said,
                    this.Name);
            }
            else
            {
                Console.WriteLine(
                    "\"{0}, {1}!\", - {2} {3}",
                    Resource.GoodEvening,
                    person.Name,
                    Resource.Said,
                    this.Name);
            }
        }

        public void Bye(Person person, EventArgs e)
        {
            Console.WriteLine(
                    "\"{0}, {1}!\", - {2} {3}",
                    Resource.GoodBye,
                    person.Name,
                    Resource.Said,
                    this.Name);
        }

        public void OnCame(DateTime time)
        {
            Console.WriteLine("[{0:00}:{1:00}]", time.Hour, time.Minute);
            Console.WriteLine("[ {0} {1}.]", this.Name, Resource.CameToWork);
            if (this.Came != null)
            {
                this.Came(this, new PersonEventArgs(time));
            }

            Console.WriteLine();
        }

        public void OnGone(DateTime time)
        {
            Console.WriteLine("[{0:00}:{1:00}]", time.Hour, time.Minute);
            Console.WriteLine("[{0} {1}.]", this.Name, Resource.WentHome);
            if (this.Gone != null)
            {
                this.Gone(this, new PersonEventArgs(time));
            }

            Console.WriteLine();
        }
    }
}
