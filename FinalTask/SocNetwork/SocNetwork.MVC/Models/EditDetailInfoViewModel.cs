namespace SocNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Users;

    public class EditDetailInfoViewModel
    {
        public EditDetailInfoViewModel(UserDetailInfo info)
        {
            this.Info = info;
        }

        public UserDetailInfo Info { get; set; }
    }
}