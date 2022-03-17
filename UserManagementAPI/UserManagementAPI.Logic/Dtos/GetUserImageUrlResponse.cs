using System;

namespace UserManagementAPI.Logic.Dtos
{
    public class GetUserImageUrlResponse
    {
        public int UserId { get; set; }

        public string ImageUrl { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
