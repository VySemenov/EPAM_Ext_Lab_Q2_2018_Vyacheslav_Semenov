namespace Task4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Post
    {
        public int Id
        {
            get => default(int);
            set
            {
            }
        }

        public int Text
        {
            get => default(int);
            set
            {
            }
        }

        public List<Attachment> Attachments { get; set; }
    }
}