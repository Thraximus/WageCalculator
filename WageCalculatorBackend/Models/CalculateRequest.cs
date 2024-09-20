using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WageCalculatorBackend.Models
{
    public class DayRange
    {
        [Required(ErrorMessage = "Start is required.")]
        [Range(0, 23, ErrorMessage = "Start must be between 0 and 23.")]
        public int Start { get; set; }

        [Required(ErrorMessage = "End is required.")]
        [Range(0, 23, ErrorMessage = "End must be between 0 and 23.")]
        public int End { get; set; }
    }

    public class CalculateRequest
    {
        [Required(ErrorMessage = "RegularRate is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Regular rate must be a non-negative integer.")]
        [JsonPropertyName("regular-rate")]
        public int RegularRate { get; set; }

        [Required(ErrorMessage = "NightTimeRate is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Night time rate must be a non-negative integer.")]
        [JsonPropertyName("night-time-rate")]
        public int NightTimeRate { get; set; }

        [Required(ErrorMessage = "MidnightRate is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Midnight rate must be a non-negative integer.")]
        [JsonPropertyName("midnight-rate")]
        public int MidnightRate { get; set; }

        [Required(ErrorMessage = "NumberOfDays is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of days must be a positive integer.")]
        [JsonPropertyName("number-of-days")]
        public int NumberOfDays { get; set; }

        [Required(ErrorMessage = "Days are required.")]
        public List<DayRange> Days { get; set; } = new List<DayRange>();

        [Required(ErrorMessage = "TimeRule is required.")]
        [JsonPropertyName("time-rule")]
        public TimeRule TimeRule { get; set; } = new TimeRule();
    }

    public class CalculateRequestStandard
    {
        [Required(ErrorMessage = "RegularRate is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Regular rate must be a non-negative integer.")]
        [JsonPropertyName("regular-rate")]
        public int RegularRate { get; set; }

        [Required(ErrorMessage = "NightTimeRate is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Night time rate must be a non-negative integer.")]
        [JsonPropertyName("night-time-rate")]
        public int NightTimeRate { get; set; }

        [Required(ErrorMessage = "MidnightRate is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Midnight rate must be a non-negative integer.")]
        [JsonPropertyName("midnight-rate")]
        public int MidnightRate { get; set; }

        [Required(ErrorMessage = "NumberOfDays is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of days must be a positive integer.")]
        [JsonPropertyName("number-of-days")]
        public int NumberOfDays { get; set; }

        [Required(ErrorMessage = "Days are required.")]
        public List<DayRange> Days { get; set; } = new List<DayRange>();
    }
}
