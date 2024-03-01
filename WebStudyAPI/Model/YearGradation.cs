using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStudyAPI.Model
{
    [Table("year_gradation")]
    public class YearGradation
    {
        [Key]
        public int Id { get; set; }
        public string Year_name { get; set; }
    }
}
