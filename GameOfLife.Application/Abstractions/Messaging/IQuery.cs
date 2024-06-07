using GameOfLife.SharedKernel;
using MediatR;

namespace GameOfLife.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
