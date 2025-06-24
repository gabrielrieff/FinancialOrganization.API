using System;
using FinancialOrganization.API.Domain.Security.Tokens;

namespace FinancialOrganization.API.Token;

public class HttpContextTokenValeu : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextTokenValeu(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string TokenOnRequest()
    {
        var authorization = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        var token = authorization.Trim().Split(" ").LastOrDefault();

        return token;
    }
}
