namespace SocNetwork.Models.ViewModels.Post
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class EditPostViewModel
    {
        public int PostId { get; set; }

        public string Text { get; set; }
    }
}