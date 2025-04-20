using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.Core.DTO
{
    public class AuthanticatedResponse
    {
        public  string ? Name { get; set; }
        public  string  Email { get; set; }
        public  string  Token { get; set; }
        public  DateTime  ExpirationDate { get; set; }

    }
}
