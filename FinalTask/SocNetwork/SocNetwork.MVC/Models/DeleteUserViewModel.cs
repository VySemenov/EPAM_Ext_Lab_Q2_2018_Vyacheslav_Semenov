﻿namespace SocNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class DeleteUserViewModel
    {
        public DeleteUserViewModel(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}