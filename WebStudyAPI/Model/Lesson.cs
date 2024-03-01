using System.ComponentModel.DataAnnotations;

namespace WebStudyAPI.Model
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        public int Year_gradation_id { get; set; }
        public int Less_week { get; set; }
        public int Subject_id { get; set; }
        public string Lesson_name { get; set; }
        public string Less_query { get; set; }
        public string Url_query { get; set; }
    }
}
