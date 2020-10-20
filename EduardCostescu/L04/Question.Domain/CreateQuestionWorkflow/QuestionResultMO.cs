using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Question.Domain.CreateQuestionWorkflow {
    public class QuestionPublished : ICreateQuestion {
        [Required]
        public QuestionMO question_ { get; private set; }

        [Required]
        public Guid questionID_ { get; private set; }

        public QuestionPublished(QuestionMO question, Guid questionID) {
            question_ = question;
            questionID_ = questionID;
        }
    };

    public class QuestionNotPublished : ICreateQuestion {
        [Required]
        public string reason_ { get; private set; }

        public QuestionNotPublished(string reason) {
            reason_ = reason;
        }
    };

    public class QuestionValidationFailed : ICreateQuestion {
        [Required]
        public IEnumerable<string> validationErrors_ { get; private set; }

        public QuestionValidationFailed(IEnumerable<string> validationErrors) {
            validationErrors_ = validationErrors.AsEnumerable();
        }
    };
}