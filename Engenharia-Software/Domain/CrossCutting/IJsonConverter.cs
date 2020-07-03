using System;
using System.Collections.Generic;
using System.Text;

namespace Engenharia_Software.Domain.CrossCutting
{
    public interface IJsonConverter
    {
        string SerializeObject(object value);
        T DeserializeObject<T>(string json);
    }
}
