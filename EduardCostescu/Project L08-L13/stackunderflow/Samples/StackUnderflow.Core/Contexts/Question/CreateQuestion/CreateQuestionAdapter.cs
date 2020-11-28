using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using Access.Primitives.IO.Mocking;
using StackUnderflow.EF.Models;
using System.Linq;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion.CreateQuestionResult;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion
{
    public partial class CreateQuestionAdapter : Adapter<CreateQuestionCmd, ICreateQuestionResult, QuestionWriteContext, QuestionDependencies>
    {
        private readonly IExecutionContext ex_;

        public CreateQuestionAdapter(IExecutionContext ex)
        {
            ex_ = ex;
        }

        public override async Task<ICreateQuestionResult> Work(CreateQuestionCmd cmd, QuestionWriteContext state, QuestionDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = AddQuestionIfMissing(state, CreateQuestionFromCommand(cmd))
                           select t;

            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new InvalidRequest(ex.ToString()));

            return result;
        }

        public ICreateQuestionResult AddQuestionIfMissing(QuestionWriteContext state, Post question)
        {
            if (state.questions_.Any(obj => obj.Title.Equals(question.Title)))
                return new QuestionNotCreated("Title already used");

            if (state.questions_.All(obj => obj.PostId != question.PostId))
                state.questions_.Add(question);

            return new QuestionCreated(question);
        }

        public Post CreateQuestionFromCommand(CreateQuestionCmd cmd)
        {
            var question = new Post();  // <=== data from cmd should go in
            return question;
        }

        public override Task PostConditions(CreateQuestionCmd cmd, ICreateQuestionResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }
    }
}