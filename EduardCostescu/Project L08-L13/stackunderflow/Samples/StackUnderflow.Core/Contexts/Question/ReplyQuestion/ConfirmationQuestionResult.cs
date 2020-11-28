using StackUnderflow.EF.Models;

namespace StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion
{
    public static partial class ConfirmationQuestionResult
    {
        public interface IConfirmationQuestionResult { }
        public class QuestionConfirmed : IConfirmationQuestionResult
        {
            public User questionUser_ { get; }
            public string confirmationAck_ { get; set; }

            public QuestionConfirmed(User questionUser, string confirmationAck)
            {
                questionUser_ = questionUser;
                confirmationAck_ = confirmationAck;
            }
        }
        public class QuestionNotConfirmed : IConfirmationQuestionResult
        {
            public string reason_ { get; }
            public QuestionNotConfirmed(string reason)
            {
                reason_ = reason_;
            }

        }
        public class InvalidRequest : IConfirmationQuestionResult
        {
            public InvalidRequest(string message)
            {
                message_ = message;
            }
            public string message_ { get; }
        }
    }
}
