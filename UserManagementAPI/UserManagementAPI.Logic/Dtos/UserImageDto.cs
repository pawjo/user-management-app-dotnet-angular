using System;

namespace UserManagementAPI.Logic.Dtos
{
    public class UserImageDto
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public DateTime ExpiresOn { get; set; }
    }
}
