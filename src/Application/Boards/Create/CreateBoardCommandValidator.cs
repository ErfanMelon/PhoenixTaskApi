﻿using FluentValidation;

namespace Application.Boards.Create;

internal sealed class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
{
    public CreateBoardCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Color).NotEmpty();
        RuleFor(x => x.Order).ExclusiveBetween(-100, 100);
    }
}
