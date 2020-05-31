using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyCards.Models {
    public class Chapter {
        [Key]
        public int ChapterId { get; set; }
        public int ChNumber { get; set; }
        public string ChName { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public List<FlashCard> FlashCards { get; set; }
        public DateTime CreatedAt = DateTime.Now;
        public DateTime UpdatedAt = DateTime.Now;
    }
}