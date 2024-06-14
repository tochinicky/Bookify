﻿using Bookify.Domain;

namespace Bookify.Application;

public interface IEmailService
{
    Task SendAsync(Email recipient, string subject, string body);
}
