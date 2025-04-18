﻿using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Projects;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Projects.Get;

internal sealed class GetProjectsQueryHandler(
    IApplicationDbContext context, IUserContext userContext) : IQueryHandler<GetProjectsQuery, List<ProjectResponse>>
{
    public async Task<Result<List<ProjectResponse>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        Guid userId = userContext.UserId;

        return await context.Projects
            .AsNoTracking()
            .Where(x => x.Workspace.Id == request.WorkspaceId && x.CreatedById == userId)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ProjectResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync(cancellationToken);
    }
}
