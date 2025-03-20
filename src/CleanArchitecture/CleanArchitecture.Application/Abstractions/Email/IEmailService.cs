using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CleanArchitecture.Application.Abstractions.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(Domain.Users.Email recipient, string subject, string body);
    }
}