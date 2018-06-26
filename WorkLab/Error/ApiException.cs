using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkLab.Model;

namespace WorkLab.Error
{
    class ApiException : Exception
    {
        public string Type { get; private set; }

        public ApiException(ErrorModel error) : base(error.Message)
        {
            Type = error.Type;
        }

    }
}
