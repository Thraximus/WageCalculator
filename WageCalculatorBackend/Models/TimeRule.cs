using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WageCalculatorBackend.Models
{
    public class TimeRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(600, ErrorMessage = "Description cannot exceed 600 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "RegularStartTime is required.")]
        [Range(0, 23, ErrorMessage = "Regular start time must be between 0 and 23.")]
        public int RegularStartTime { get; set; }

        [Required(ErrorMessage = "NightTimeStartTime is required.")]
        [Range(0, 23, ErrorMessage = "Night time start time must be between 0 and 23.")]
        public int NightTimeStartTime { get; set; }

        [Required(ErrorMessage = "MidnightStartTime is required.")]
        [Range(0, 23, ErrorMessage = "Midnight start time must be between 0 and 23.")]
        public int MidnightStartTime { get; set; }
    }


}
