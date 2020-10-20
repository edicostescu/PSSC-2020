using System;
using System.Collections.Generic;

namespace Question.Domain.CreateQuestionWorkflow {
    public interface ICreateQuestion {
        public static ICreateQuestion CreateQuestion(QuestionMO receivedQuestion) {
            if (string.IsNullOrWhiteSpace(receivedQuestion.title_) || string.IsNullOrWhiteSpace(receivedQuestion.question_)) {
                IEnumerable<string> errors = new List<string>() {
                    "Empty title",
                    "Empty question"
                };
                return new QuestionValidationFailed(errors);
            }

            if (new Random().Next(10) > 1) {
                return new QuestionNotPublished("Question or title could not be verified.");
            }

            return new QuestionPublished(receivedQuestion, Guid.NewGuid());
        }
    }
}