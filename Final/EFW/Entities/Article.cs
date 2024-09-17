using System.ComponentModel.DataAnnotations;

namespace Final.EFW.Entities
{
    public class Article
    {
        protected internal void Var(string? _subject, string _text, User? _author)
        {
            Text = _text;
            Author = _author;
            Subject = _subject;
        }
        [Key]
        public string? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Subject { get; set; }
        public string? Text { get; set; }
        public User? Author { get; set; }
    }
}