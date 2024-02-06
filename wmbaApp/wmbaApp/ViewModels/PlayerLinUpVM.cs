using System.ComponentModel.DataAnnotations.Schema;
using wmbaApp.Models;

namespace wmbaApp.ViewModels
{
    public class PlayerLinUpVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
       
        public int GameId { get; set; }
        
        
        public int TeamId { get; set; }
        
       public List<int> PlayersIds { get; set; }

    }
}
