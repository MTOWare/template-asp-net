using System.Threading.Tasks;

namespace Template.Core.Interfaces
{
    public interface ISendGridService
    {
        Task SendMailConfirmation(string subject, string token, string email, string name);

        Task SendMailPassword(string subject, string password, string email, string name);
    }
}
