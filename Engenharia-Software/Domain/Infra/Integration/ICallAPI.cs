using System;
using System.Collections.Generic;
using System.Text;

namespace Engenharia_Software.Domain.Infra.Integration
{
    public interface ICallAPI
    {
        T Get<T>(string resource);
    }
}
