using Access.Primitives.EFCore;
using Access.Primitives.IO;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Domain.Core.Contexts.Question;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion;
using StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion;
using StackUnderflow.EF.Models;
using System;
using System.Threading.Tasks;
using Orleans;
using StackUnderflow.API.AspNetCore.Grain;
using LanguageExt;

namespace StackUnderflow.API.AspNetCore.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly StackUnderflowContext _dbContext;
        private readonly IClusterClient _client;

        public QuestionController(IInterpreterAsync interpreter, StackUnderflowContext dbContext, IClusterClient client)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
            _client = client;
        }

        [HttpPost("question")]
        public async Task<ActionResult> CreateQuestion([FromBody] CreateQuestionCmd cmd)
        {
            QuestionWriteContext ctx = new QuestionWriteContext(new EFList<Post>(_dbContext.Post));
            var dependencies = new QuestionDependencies();
            dependencies.SendConfirmationEmail = (ConfirmationLetter response) => async () => new ConfirmationAcknowledgement(Guid.NewGuid().ToString());

            var expr = from questionResult in QuestionDomain.CreateQuestion(cmd)
                       select questionResult;

            var result = await _interpreter.Interpret(expr, ctx, dependencies);

            _dbContext.SaveChanges();

            return result.Match(
                created => Ok(created),
                notCreated => (ActionResult)BadRequest("Question not created"),
                invalidRequest => BadRequest("Invalid request"));
        }

        private TryAsync<ConfirmationAcknowledgement> SendEmail(ConfirmationLetter letter) => async () =>
        {
            var emailSender = _client.GetGrain<IEmailSender>(0);
            await emailSender.SendEmailAsync(letter.letter_);
            return new ConfirmationAcknowledgement(Guid.NewGuid().ToString());
        };
    }
}