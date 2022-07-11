namespace UserManagementAPI.Logic.Dtos
{
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public bool IsDefaultImage { get; set; }
        public SasUrlDto Image { get; set; }
    }
}
