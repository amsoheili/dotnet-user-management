public record CreateUserDto(
    string phoneNumber,
    string? firstname,
    string? lastname,
    string? address,
    DateTime? birthDate,
    string? username,
    string? password,
    string? nationalCode
);