﻿using System;

namespace UserManagementAPI.Logic.Dtos
{
    public class SasUrlDto
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public DateTime ExpiresOn { get; set; }
    }
}
