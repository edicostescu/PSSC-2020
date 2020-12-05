using Access.Primitives.EFCore;
using Access.Primitives.IO;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Domain.Core.Contexts.Question;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion;
using StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion;
using StackUnderflow.EF.Models;
using System;
using System.Threading.Tasks;

namespace StackUnderflow.API.AspNetCore.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly StackUnderflowContext _dbContext;

        public QuestionController(IInterpreterAsync interpreter, StackUnderflowContext dbContext)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
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
    }
}