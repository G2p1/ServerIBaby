using System.ComponentModel.DataAnnotations;

namespace WebStudyAPI.Model
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Subject_name { get; set; }
    }
}
