using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace wmbaApp.Models
{
    public class PlayersLineUp
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public Game Game { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Team Team  { get; set; }
        public ICollection<Player> Players { get; set; }=new HashSet<Player>();

    }
}
