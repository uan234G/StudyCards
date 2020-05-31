using System;
using System.ComponentModel.DataAnnotations;

namespace StudyCards.Models {
    public class FlashCard {
        [Key]
        public int FlashCardId { get; set; }

        [Required]
        public string Word { get; set; }

        [Required]
        public string Definition { get; set; }
        public bool IsComplete { get; set; }

        [Required]
        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }
        public DateTime CreatedAt = DateTime.Now;
        public DateTime UpdatedAt = DateTime.Now;
    }
}