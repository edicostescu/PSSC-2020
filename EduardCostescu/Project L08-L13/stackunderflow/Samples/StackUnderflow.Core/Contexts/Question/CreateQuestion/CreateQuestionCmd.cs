using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion
{
    public struct CreateQuestionCmd
    {
        [Required]
        public int questionId_ { get; set; }
        [Required]
        public string title_ { get; set; }
        [Required]
        public string body_ { get; set; }
        [Required]
        public IEnumerable<string> tags_ { get; set; }
        public CreateQuestionCmd(int questionId, string title, string body, IEnumerable<string> tags)
        {
            questionId_ = questionId;
            title_ = title;
            body_ = body;
            tags_ = tags;
        }
    }
}
