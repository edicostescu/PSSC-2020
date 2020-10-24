using System;
using System.Linq;
using System.Collections.Generic;
using User.Domain.CreateUserWorkflow;

namespace Question.Domain.CreateQuestionWorkflow {
    public interface ICreateQuestion {
        public static ICreateQuestion CreateQuestion(QuestionMO receivedQuestion, UserCreated fromUser) {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(receivedQuestion.title_) || string.IsNullOrWhiteSpace(receivedQuestion.question_)) {
                errors.Add("Empty title");
                errors.Add("Empty question");
            }

            if (receivedQuestion.tags_.Count() < 1 || receivedQuestion.tags_.Count() > 3) {
                errors.Add("Incorrect number of tags");
            }

            if (receivedQuestion.question_.ToCharArray().Count(c => c != ' ' ||
                                                                    c != ',' ||
                                                                    c != '.' ||
                                                                    c != '!' ||
                                                                    c != '?' ||
                                                                    c != '-') > 1000) {
                errors.Add("Number of characters exceeds 1000");
            } 

            /*
            if (new Random().Next(10) > 1) {
                return new QuestionNotPublished("Question or title could not be verified.");
            }
            */

            if (errors.Count() > 0) {
                return new QuestionValidationFailed(errors.AsEnumerable());
            }

            return new QuestionPublished(receivedQuestion, Guid.NewGuid(), fromUser);
        }
    }
}