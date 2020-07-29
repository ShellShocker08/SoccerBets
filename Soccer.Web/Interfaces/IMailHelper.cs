using Soccer.Common.Responses;

namespace Soccer.Web.Interfaces
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }
}
