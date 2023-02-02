using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    [Table("T_USER_COURSE")]
    public class UserCourse
    {
        [Column("COURSE_ID")]
        public int courseId { get; set; }
        [Column("USER_ID")]
        public int userId { get; set; }
        [Column("SCORE")]
        public float score { get; set; }
    }
}
