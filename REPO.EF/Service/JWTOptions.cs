﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.EF.Service
{
    public class JWTOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int Expiration { get; set; }
        public string SecretKey { get; set; } = string.Empty;
    }
}
