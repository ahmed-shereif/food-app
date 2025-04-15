using Application.CQRS.Users.Login;
using Application.CQRS.Users.Registration;
using Application.DTOS.UserDtos;
using Application.Helpers.MappingProfile;
using Application.ViewModels;
using Azure.Core;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.CQRS.LoginOrchestrator;
using Application.Helpers;
using Presentation.ViewModels.UserViewModels;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("Register")]

        public async Task<ResponseViewModel<UserViewModel>> Register(UserViewModel createUser)
        {
            if(!ModelState.IsValid)
            {
             return   ResponseViewModel<UserViewModel>.Failure(createUser, "ssss", ErrorCodeEnum.AlreadyExist);
            }
            var MappingDto = createUser.Map<CreateUserDto>();
            var result = await _mediator.Send(new RegistrationCommand(MappingDto));
            if (!result.IsSuccess)
               return ResponseViewModel<UserViewModel>.Failure(createUser, "ssss", ErrorCodeEnum.AlreadyExist);
            return ResponseViewModel<UserViewModel>.Success(createUser, "sasasasa");
            ;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ResponseViewModel<LoginOrchestratorDto>> Login(LoginOrchestratorViewModel LoginOrchestratorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return ResponseViewModel<LoginOrchestratorDto>.Failure(null, "Empty Data ", ErrorCodeEnum.BadRequest);
            }
           var MappingDto = LoginOrchestratorViewModel.Map<LoginUserViewModel>();
            var result = await _mediator.Send(new LogingOrchestratorCommand(MappingDto));
               // var mappingToUser = result.Map<LoginOrchestratorViewModel>();

            return ResponseViewModel<LoginOrchestratorDto>.Success(result, "welcome Back");
        }
    }
}
