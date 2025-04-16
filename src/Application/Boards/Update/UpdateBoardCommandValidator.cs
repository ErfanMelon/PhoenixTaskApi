﻿using FluentValidation;

namespace Application.Boards.Update;

internal sealed class UpdateBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
{
    public UpdateBoardCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Color).NotEmpty();
        RuleFor(x => x.Order).ExclusiveBetween(-100, 100);
    }
}
