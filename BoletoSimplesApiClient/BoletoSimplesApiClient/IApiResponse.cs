using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient
{
    interface IApiResponse<T> where T : new()
    {
        bool IsSuccess { get; }
        T Content { get; }
    }
}
