
using CultureWeb.Service.Models;

namespace CultureWeb.Service.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
