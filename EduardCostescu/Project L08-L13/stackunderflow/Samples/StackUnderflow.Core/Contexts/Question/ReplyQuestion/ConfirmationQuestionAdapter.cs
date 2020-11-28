using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using Access.Primitives.IO.Mocking;
using StackUnderflow.EF.Models;
using System;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion.ConfirmationQuestionResult;

namespace StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion
{
    public partial class ConfirmationQuestionAdapter : Adapter<ConfirmationQuestionCmd, IConfirmationQuestionResult, QuestionWriteContext, QuestionDependencies>
    {
        private readonly IExecutionContext _ex;
        public ConfirmationQuestionAdapter(IExecutionContext ex)
        {
            _ex = ex;
        }
        public override async Task<IConfirmationQuestionResult> Work(ConfirmationQuestionCmd command, QuestionWriteContext state, QuestionDependencies dependencies)
        {
            var wf = from isValid in command.TryValidate()
                     from user in command.QuestionUser.ToTryAsync()
                     let letter = GenerateConfirmationLetter(user, "random")
                     from confirmationAcknowledgement in dependencies.SendConfirmationEmail(letter)
                     select (user, confirmationAcknowledgement);
            return await wf.Match(
                Succ: r => new QuestionConfirmed(r.user, r.confirmationAcknowledgement.receipt_),
                Fail: Exception => (IConfirmationQuestionResult)new InvalidRequest(Exception.ToString()));
        }
        private ConfirmationLetter GenerateConfirmationLetter(User user, string token)
        {
            var link = $"https://stackunderflow/questions/{token}";
            var letter = $@"Dear {user.DisplayName} your question is posted. For details please click on {link}";
            return new ConfirmationLetter(user.Email, letter, new Uri(link));
        }
        public override Task PostConditions(ConfirmationQuestionCmd cmd, IConfirmationQuestionResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }
    }
}
