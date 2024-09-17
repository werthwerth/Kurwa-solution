using System.ComponentModel.DataAnnotations;

namespace Final.EFW.Entities
{
    public class ArticleComment
    {
        protected internal void Var(Article _article, Comment _comment)
        {
            Article = _article;
            Comment = _comment;

        }
        [Key]
        public string? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public Article? Article { get; set; }
        public Comment? Comment { get; set; }
    }
}
