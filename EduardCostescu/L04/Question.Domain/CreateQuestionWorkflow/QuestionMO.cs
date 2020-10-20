using System.ComponentModel.DataAnnotations;

namespace Question.Domain.CreateQuestionWorkflow {
    public struct QuestionMO {
        [Required]
        public string title_ { get; set; }
        
        [Required]
        public string category_ { get; set; }
        
        [Required]
        public string tags_ { get; set; }
        
        [Required]
        public string question_ { get; set; }

        public QuestionMO (string title, string category, string tags, string question) {
            title_ = title;
            category_ = category;
            tags_ = tags;
            question_ = question;
        }
    };
}