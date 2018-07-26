namespace Task4
{
    using System.Collections.Generic;

    public class Dialog
    {
        public int Id { get; set; }

        public List<Message> Messages { get; set; }

        public List<User> Users { get; set; }
    }
}