namespace Task4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Message
    {
        public int Id { get; set; }

        public int Text { get; set; }

        public List<Attachment> Attachment { get; set; }
    }
}