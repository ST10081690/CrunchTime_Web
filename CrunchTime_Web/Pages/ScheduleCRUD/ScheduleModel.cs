using CrunchTime_Web.Pages.ModuleCRUD;
using CrunchTime_Web.Pages.SemesterCRUD;
using System.ComponentModel.DataAnnotations;

namespace CrunchTime_Web.Pages.ScheduleCRUD
{
    public class ScheduleModel
    {
        //creating model properties
        [Required]
        public int ScheduleModelID { get; set; } //id of module

        public string ScheduleName { get; set; } = string.Empty; //name of module

        public int HoursStudied { get; set; } //number of hours user studied for module

        [DataType(DataType.Date)]
        public DateTime DateOfStudy { get; set; } //date when user studied

        public double? RemainingStudyHours { get; set; } //remaining self-study hours for module

        public string? ModuleWeekDay { get; set; } //setting aside weekday for this module


        public string? UserID { get; set; } //user that saved the schedule

        public int ModuleModelID { get; set; }//id of corresponding module

        public virtual ModuleModel? Module { get; set; } //relation to module model


    }
}
