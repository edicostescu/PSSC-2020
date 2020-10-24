using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Question.Domain.CreateQuestionWorkflow {
    public struct QuestionMO {
        [Required]
        public string title_ { get; set; }
        
        [Required]
        public string category_ { get; set; }
        
        [Required]
        public IEnumerable<string> tags_ { get; set; }
        
        [Required]
        public string question_ { get; set; }

        public QuestionMO (string title, string category, List<string> tags, string question) {
            title_ = title;
            category_ = category;
            tags_ = tags.AsEnumerable();
            question_ = question;
        }
    };
}