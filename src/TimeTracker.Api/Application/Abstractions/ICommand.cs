using ErrorOr;
using MediatR;

namespace TimeTracker.Api.Application.Abstractions;

public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>;