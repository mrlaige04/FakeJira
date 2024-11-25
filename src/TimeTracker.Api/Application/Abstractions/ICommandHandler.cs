using ErrorOr;
using MediatR;

namespace TimeTracker.Api.Application.Abstractions;

public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, ErrorOr<TResponse>>
    where TRequest : ICommand<TResponse>;