﻿using Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.VerifyEmailOrchestratort
{
    public record VerifyEmailOrchestratortCommand(Guid TokenId):IRequest<ResponseViewModel<bool>>;

}
