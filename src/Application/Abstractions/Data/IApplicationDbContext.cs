﻿using Domain.Projects;
using Domain.Subscriptions;
using Domain.Tasks;
using Domain.Users;
using Domain.Workspaces;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Tasks.Task;


namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Workspace> Workspaces { get; }
    DbSet<Project> Projects { get; }
    DbSet<Board> Boards { get; }
    DbSet<Task> Tasks { get; }
    DbSet<Invitation> Invitations { get; }
    DbSet<Setting> Settings { get; }
    DbSet<TeamMember> Members { get; }
    DbSet<UserToken> RefreshTokens { get; set; }
    DbSet<Comment> Comments { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
