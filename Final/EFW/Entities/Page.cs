using System.ComponentModel.DataAnnotations;

namespace Final.EFW.Entities
{
    public class Page
    {
        protected internal void Var(string _controller, string _action)
        {
            Controller = _controller;
            Action = _action;
        }
        [Key]
        public string? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
    }
}

