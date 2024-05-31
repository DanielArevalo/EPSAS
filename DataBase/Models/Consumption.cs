using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    public class Consumption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string Line { get; set; }

        public DateTime Date { get; set; }

        public float Residential_Wh { get; set; }

        public float Commercial_Wh { get; set; }

        public float Industrial_Wh { get; set; }
    }
}
