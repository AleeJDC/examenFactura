namespace examen_app.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
