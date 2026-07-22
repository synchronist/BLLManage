using BLLManage.Application.Interfaces;
using MediatR;

namespace BLLManage.Application.Features.Authentication.Login;

public sealed class LoginHandler
    : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginHandler(IJwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    public Task<LoginResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Email != "admin@bllmanage.com"
            || request.Password != "123456")
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var token = _tokenGenerator.GenerateToken(
            Guid.NewGuid().ToString(),
            request.Email);

        return Task.FromResult(new LoginResponse(token));
    }
}