using Application.DTOS.UserDtos;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Token
{
    public record TokenGenerationCommand(User user) : IRequest<string>;

    
}
