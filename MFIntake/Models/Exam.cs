using System;
using System.ComponentModel.DataAnnotations;

namespace MFIntake.Models
{
    public class Exam
    {
        public int ExamID { get; set; }

        public int IntakeID { get; set; }

        public string ExamType { get; set; }

        public string ExamStatus { get; set; }

        public string Analyst { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExamDate { get; set; }

        public string ExamFileLocation { get; set; }

        public bool AddlEquipNeeded { get; set; }

        [StringLength(250)]
        public string ExamNote { get; set; }

        public virtual Intake Intake { get; set; }

        public virtual Status Statuses { get; set; }

        public virtual Person Persons { get; set; }

    }
}