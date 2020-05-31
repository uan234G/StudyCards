using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyCards.Models {
    public class Book {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public List<Chapter> Chapters { get; set; }
        public DateTime CreatedAt = DateTime.Now;
        public DateTime UpdatedAt = DateTime.Now;
    }
}