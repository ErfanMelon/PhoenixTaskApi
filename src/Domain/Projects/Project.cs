﻿using Domain.Interfaces;
using Domain.Users;
using Domain.Workspaces;
using SharedKernel;

namespace Domain.Projects;

public sealed class Project : Entity, IAuditableEntity, IBlamableEntity, ISoftDeletableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public Workspace Workspace { get; set; }
    public DateTime? CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool Deleted { get; }
    public User? CreatedBy { get; set; }
    public User? ModifiedBy { get; }
    public User? DeletedBy { get; }
    public Guid? CreatedById { get; }
    public Guid? ModifiedById { get; }

    public ICollection<Board> Boards { get; set; }
}
