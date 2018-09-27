namespace SocNetwork.Models.ViewModels.Post
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class CreatePostViewModel
    {
        public string Text { get; set; }

        public int PageId { get; set; }
    }
}