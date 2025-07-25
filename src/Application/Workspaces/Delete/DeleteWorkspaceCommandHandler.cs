﻿using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Workspaces;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Workspaces.Delete;

internal sealed class DeleteWorkspaceCommandHandler(IApplicationDbContext context, IUserContext userContext)
    : ICommandHandler<DeleteWorkspaceCommand>
{
    public async Task<Result> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
    {
        Workspace? workspace = await context.Workspaces
            .Include(x => x.Projects)
            .ThenInclude(x => x.Boards)
            .ThenInclude(x => x.Tasks)
            .SingleOrDefaultAsync(x => x.Id == request.Id && x.CreatedById == userContext.UserId,
            cancellationToken);

        if (workspace is null)
        {
            return Result.Failure(WorkspaceErrors.NotFound(request.Id));
        }
        context.Workspaces.Remove(workspace);
        int rowAffect = await context.SaveChangesAsync(cancellationToken);

        return rowAffect > 0 ? Result.Success() : Result.Failure(Error.None);
    }
}
