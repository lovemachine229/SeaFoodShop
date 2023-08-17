using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    [Table("AprioriRule")]
    public partial class AprioriRule
    {
        public int Id { get; set; }

        public string X { get; set; }

        public string Y { get; set; }

        public double Confidence { get; set; }

        [NotMapped]
        public string InputName { get; set; }
        [NotMapped]
        public string OutputName { get; set; }
    }
}
