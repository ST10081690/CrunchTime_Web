using CrunchTime_Web.Pages.ScheduleCRUD;
using CrunchTime_Web.Pages.SemesterCRUD;
using System.ComponentModel.DataAnnotations;

namespace CrunchTime_Web.Pages.ModuleCRUD
{
    public class ModuleModel
    {
        //creating model properties
        [Required]
        public int ModuleModelID { get; set; } //id of module

        public string ModuleCode { get; set; } = string.Empty; //code of the module

        public string ModuleName { get; set; } = string.Empty; //name of module

        public int ModuleCredits { get; set; } //number of credits for module

        public int ClassHours { get; set; } //module class hours per week

        public double? SelfStudyHours { get; set; } //module self study hours

        public string? UserID { get; set; } //user that saved the module

        public int SemesterModelID { get; set; }//id of corresponding semester

        public virtual SemesterModel? Semester { get; set; } //relation to semester model

        public virtual ICollection<ScheduleModel>? Schedules { get; set; } //list of corresponding schedules
        
    }
}
