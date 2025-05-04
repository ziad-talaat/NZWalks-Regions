using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.EF.Service
{
    public class JWTOptions
    {
        public string issuer { get; set; } = string.Empty;
        public string audience { get; set; } = string.Empty;
        public int expiration { get; set; }
        public string secretKey { get; set; } = string.Empty;
    }
}
