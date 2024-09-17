using System.ComponentModel.DataAnnotations;

namespace Final.EFW.Entities
{
    public class ArticleTag
    {
        protected internal void Var(Article _article, Tag _tag)
        {
            Article = _article;
            Tag = _tag;

        }
        [Key]
        public string? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public Article? Article { get; set; }
        public Tag? Tag { get; set; }
    }
}
