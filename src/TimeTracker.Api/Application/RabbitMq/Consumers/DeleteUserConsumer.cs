using Contracts.Users.DeleteUser;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.RabbitMq.Consumers;

public class DeleteUserConsumer(ITaskRepository taskRepository) : IConsumer<DeleteUserMessage>
{
    public async Task Consume(ConsumeContext<DeleteUserMessage> context)
    {
        var queryable = taskRepository.GetQuery();
        await queryable
            .Where(t => t.AssigneeId == context.Message.UserId)
            .ExecuteUpdateAsync(u =>
                u.SetProperty(t => t.AssigneeId, context.Message.UserId));
    }
}