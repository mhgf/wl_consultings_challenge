using Microsoft.EntityFrameworkCore;
using WlChallenge.Api.Endpoints.User.Query.Dtos;
using WlChallenge.Api.Shared.Request;
using WlChallenge.Api.Shared.Response;
using WlChallenge.Infra.Data;

namespace WlChallenge.Api.Endpoints.User.Query;

public static class GetAllUser
{
    public static async Task<IResult> Execute(
        [AsParameters] PaginationRequest request,
        AppDbContext context,
        CancellationToken token = default)
    {
        var query = context
            .Users
            .AsNoTracking();


        var totalCount = await query.CountAsync(token);

        var users = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(user => new UserDto(
                user.Id,
                user.Name,
                user.Email.Address,
                new DocumentDto(user.Document.Number, user.Document.Type)))
            .ToListAsync(token);

        return PaginationResponse<UserDto>
            .Create(users, request.Page, request.PageSize, totalCount)
            .ToResult();
    }
}