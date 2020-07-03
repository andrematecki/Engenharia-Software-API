using Engenharia_Software.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engenharia_Software.Domain.Infra.IDP
{
    public interface IIdentityProvider
    {
        string GenerateToken(User user);
    }
}
