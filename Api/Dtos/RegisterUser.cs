namespace ApiGateway.Dtos
{
    public class RegisterUserDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
    }
}

public class ApiResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
}
