namespace UserManagementAPI.Logic.Dtos
{
    public class AddUserRequest
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }
    }
}
