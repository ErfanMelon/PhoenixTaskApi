﻿using System.Globalization;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Tasks.Get;

internal sealed class GetTasksQueryHandler(IApplicationDbContext context, IUserContext userContext) : IQueryHandler<GetTasksQuery, List<TaskResponse>>
{
    public async Task<Result<List<TaskResponse>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        Guid userId = userContext.UserId;

        return await context.Tasks
            .AsNoTracking()
            .Where(x => x.Board.Id == request.BoardId && x.CreatedById == userId)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new TaskResponse
            {
                Id = x.Id,
                DeadLine = x.DeadLine.ToString(new CultureInfo("en-US")),
                Description = x.Description,
                Name = x.Name,
                Order = x.Order,
                Priority = x.Priority
            }).ToListAsync(cancellationToken);
    }
}
