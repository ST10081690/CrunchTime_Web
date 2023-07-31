using CrunchTime_Web.Pages.ModuleCRUD;
using System.ComponentModel.DataAnnotations;

namespace CrunchTime_Web.Pages.SemesterCRUD
{
    public class SemesterModel
    {
        //creating model properties
        [Required]
        public int SemesterModelID { get; set; } //id of semester

        public string SemesterName { get; set; } = string.Empty; //name of semester

        public double WeeksInSemester { get; set; }//number of weeks in semester

        [DataType(DataType.Date)]
        public DateTime SemesterStartDate { get; set; } //date semester starts

        public string? UserID { get; set; } //user that saved the semester

        public virtual ICollection<ModuleModel>? SemesterModules { get; set; }//list of modules in semester

    }
}
