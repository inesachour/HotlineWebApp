using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotline.Services
{
    public interface IMailingService
    {
        Task SendEmail(string mailTo, string subject, string body);
        
    }
}
