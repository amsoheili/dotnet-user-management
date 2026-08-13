using System.IO.Pipelines;

public interface IAuthService
{
    public Task<ServiceResult<string>> SendOtpSms(LoginDto data);
    public Task<ServiceResult<string>> Login(LoginDto data);
}

public class AuthService
{
    public async Task<ServiceResult<string>> SendOtpSms(LoginDto data)
    {
        var phoneNumber = data.phoneNumber;
        // check if i have sent the otp before ? checking the related database
        return ServiceResult<string>.Success("helll");
    }

    // public Task<string> Login(LoginDto data)
    // {
    //     // user sends phone number

    //     // we send them a certain otp using bale messenger

    //     // there is a certain time users can enter that otp
    // }
}