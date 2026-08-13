public record LoginUserResponseDto(
    string accessToken,
    string refreshToken,
    long expirationDate
);