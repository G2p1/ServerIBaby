using Microsoft.EntityFrameworkCore;

namespace WebStudyAPI.Model
{
    [Keyless]
    public class UserLessonDone
    {
        public int User_id { get; set; }
        public int Lesson_id { get; set; }
    }
}
