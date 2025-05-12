using WlChallenge.Api.Shared;
using WlChallenge.Api.Shared.Request;
using WlChallenge.Domain.Enums;

namespace WlChallenge.Api.Endpoints.User.Query.Dtos;

public record TransactionDto(
    Guid Sender,
    string SenderName,
    Guid Receiver,
    string ReceiverName,
    uint Amount,
    ETransactionType type,
    DateTime Date);

public class TransactionRequest(
    int page = Constants.DefaultPageNumber,
    int pageSize = Constants.DefaultPageSize,
    DateTime? startDate = null,
    DateTime? endDate = null)
    : PaginationRequest(page, pageSize)
{
    public DateTime? StartDate { get; } = startDate;
    public DateTime? EndDate { get; } = endDate;
};