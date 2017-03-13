using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MFIntake.Models
{
    public class Intake
    {
        public int ID { get; set; }

        public string CaseNumber { get; set; }

        public string DeviceModel { get; set; }

        [Display(Name="Case Agent")]
        public string FullName { get; set; }

        [Display(Name = "Custodian")]
        public string Custodian { get; set; }

        public string WarrantNumber { get; set; }

        [Required]
        public string StorageLocation { get; set; }

        [Display(Name = "Intake Status")]
        public string IntakeStatus { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReceivedDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RequestedByDate { get; set; }

        [StringLength(250)]
        public string IntakeNote { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }

        public virtual Agent Agents { get; set; }

        public virtual Custodian Custodians { get; set; }

        public virtual Status Statuses { get; set; }

    }
}