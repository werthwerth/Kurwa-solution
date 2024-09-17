using System.ComponentModel.DataAnnotations;

namespace Final.EFW.Entities
{
    public class Tag
    {
        protected internal void Var(string _text, User? _author)
        {
            Text = _text;
            Author = _author;

        }
        [Key]
        public string? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Text { get; set; }
        public User? Author { get; set; }
    }
}