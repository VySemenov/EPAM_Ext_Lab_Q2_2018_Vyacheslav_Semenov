namespace Task_6_2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main(string[] args)
        {
            List<Person> persons = new List<Person>();

            DateTime time = new DateTime(2018, 07, 25, 9, 0, 0);

            Person john = new Person { Name = "John" };
            Person patrick = new Person { Name = "Patrick" };
            Person pumpkin = new Person { Name = "Pumpkin" };
            Person alice = new Person { Name = "Alice" };

            CameNotify(persons, john, time);

            time = time.AddMinutes(3);
            CameNotify(persons, alice, time);

            time = time.AddHours(2);
            CameNotify(persons, patrick, time);

            time = time.AddHours(6);
            time = time.AddMinutes(12);
            CameNotify(persons, pumpkin, time);

            time = time.AddHours(1);
            GoneNotify(persons, pumpkin, time);

            time = time.AddMinutes(3);
            GoneNotify(persons, patrick, time);

            time = time.AddMinutes(15);
            GoneNotify(persons, alice, time);

            time = time.AddMinutes(32);
            GoneNotify(persons, john, time);

            Console.ReadKey();
        }

        public static void CameNotify(List<Person> list, Person person, DateTime time)
        {
            if (!list.Contains(person))
            {
                foreach (var p in list)
                {
                    person.Came += p.Hello;
                }

                list.Add(person);
                person.OnCame(time);
            }
        }

        public static void GoneNotify(List<Person> list, Person person, DateTime time)
        {
            if (list.Contains(person))
            {
                list.Remove(person);
                foreach (var p in list)
                {
                    person.Gone += p.Bye;
                }

                person.OnGone(time);
            }
        }
    }
}
