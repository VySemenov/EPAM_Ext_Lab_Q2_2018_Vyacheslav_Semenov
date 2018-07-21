namespace Task4
{
    public class Attachment
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            var attachment = obj as Attachment;
            return attachment != null &&
                   Id == attachment.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}