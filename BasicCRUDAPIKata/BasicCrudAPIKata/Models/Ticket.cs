using System.ComponentModel.DataAnnotations;

namespace BasicCrudAPIKata.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; }

        public string Content { get; set; }

        public int PersonId { get; set; }
    }
}
