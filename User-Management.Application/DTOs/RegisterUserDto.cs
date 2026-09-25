public record RegisterUserDto(
    string username,
    string? password,
    string? nationalCode,
    string phoneNumber
);