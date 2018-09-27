namespace DAL.Entities.Dialogues
{
    using System;
    using System.Collections.Generic;
    using DAL.Entities.Users;

    public class Dialog
    {
        public Dialog()
        {
        }

        public Dialog(int id, int uid, int iid)
        {
            this.Id = id;
            this.UserId = uid;
            this.InterlocutorId = iid;
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public int InterlocutorId { get; set; }

        public override bool Equals(object obj)
        {
            var dialog = obj as Dialog;
            return dialog != null &&
                   this.Id == dialog.Id &&
                   this.UserId == dialog.UserId &&
                   this.InterlocutorId == dialog.InterlocutorId;
        }

        public override int GetHashCode()
        {
            var hashCode = 111572479;
            hashCode = (hashCode * -1521134295) + this.Id.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.UserId.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.InterlocutorId.GetHashCode();
            return hashCode;
        }
    }
}
