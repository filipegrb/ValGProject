using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ValGProject.Models
{
    public class Topic
    {
        public int Id { get; set; }

        [DisplayName("Title:")]
        [Required(ErrorMessage = "Title Field is Required")] 
        public string Title { get; set; }

        [DisplayName("Description:")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Description Field is Required")]
        public string Description { get; set; }

        [DisplayName("Creator:")]
        [Required(ErrorMessage = "Creator Field is Required")]
        public string Creator { get; set; }

        [DisplayName("Creaton Date:")]
        public DateTime CreatonDate { get; set; }
    }
}
