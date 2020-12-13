using System.Threading.Tasks;

namespace StackUnderflow.API.AspNetCore.Grain
{
    public class EmailSenderGrain : Orleans.Grain, IEmailSender
    {
        public Task<string> SendEmailAsync(string message)
        {
            return Task.FromResult(message);
        }
    }
}
