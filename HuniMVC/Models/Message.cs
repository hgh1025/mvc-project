
using Microsoft.EntityFrameworkCore;

namespace HuniMVC.Models
{
    public class Message
    {
        public virtual int? Id { get; set; }
        public int MovieId { get; set; }  // This is the foreign key
        public virtual Movie Movie { get; set; }  // Navigation property
        public virtual Guid? MessageId { get; set; } // pk

        public string Body { get; set; }
    
        public DateTime date { get; set; }
        //public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

        //public MessageComment Comments { get; set; }
    }
}
