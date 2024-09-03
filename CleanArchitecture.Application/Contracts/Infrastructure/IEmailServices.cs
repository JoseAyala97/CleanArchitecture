using CleanArchitecture.Application.Models;

namespace CleanArchitecture.Application.Contracts.Infrastructure
{
    public interface IEmailServices
    {
        Task<bool> SendEmail(Email email);
    }
}
