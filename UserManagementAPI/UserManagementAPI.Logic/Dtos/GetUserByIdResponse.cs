namespace UserManagementAPI.Logic.Dtos
{
    public class GetUserByIdResponse
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }
    }
}
