using Orleans;
using System.Threading.Tasks;

namespace StackUnderflow.API.AspNetCore.Grain
{
    public interface IEmailSender : IGrainWithIntegerKey
    {
        Task<string> SendEmailAsync(string message);
    }
}
