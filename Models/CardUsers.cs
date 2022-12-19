namespace Board.Models
{
    public class CardUser
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
        public Card Card { get; set; }
        public User User { get; set; }
    }
}
