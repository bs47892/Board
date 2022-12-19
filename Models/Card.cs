using System.ComponentModel.DataAnnotations;


namespace Board.Models
{
    public class Card
    {
        [Key] 
        public int CardId { get; set; }
        public int ListId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CardUser> CardUsers { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List List { get; set; }
    }
}
