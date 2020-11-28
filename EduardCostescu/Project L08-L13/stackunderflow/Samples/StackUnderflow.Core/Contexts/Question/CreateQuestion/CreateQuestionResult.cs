using Access.Primitives.Extensions.Cloning;
using StackUnderflow.EF.Models;
using CSharp.Choices;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult : IDynClonable { }

        public class QuestionCreated : ICreateQuestionResult
        {
            public Post question_ { get; }

            public QuestionCreated(Post question)
            {
                question_ = question;
            }
            public object Clone() => this.ShallowClone();
        }

        public class QuestionNotCreated : ICreateQuestionResult
        {
            public string reason_ { get; private set; }
            public QuestionNotCreated(string reason)
            {
                reason_ = reason;
            }
            public object Clone() => this.ShallowClone();
        }

        public class InvalidRequest : ICreateQuestionResult
        {
            public string message_ { get; }
            public InvalidRequest(string message)
            {
                message_ = message;
            }
            public object Clone() => this.ShallowClone();
        }
    }
}
