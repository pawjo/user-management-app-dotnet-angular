using System;

namespace UserManagementAPI.Logic.Dtos
{
    public class SasUrlDto
    {
        public string Url { get; set; }

        public DateTime ExpiresOn { get; set; }
    }
}
