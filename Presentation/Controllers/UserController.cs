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
using Application.CQRS.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Application.CQRS.RegistrationUserOrchestrator;
using Application.ViewModels.UserViewModels;
using Application.CQRS.Users.VerifyEmail;
using Application.CQRS.VerifyEmailOrchestratort;

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
        //[HttpPost]
        //[Route("Register")]
        //public async Task<ResponseViewModel<UserViewModel>> Register(UserViewModel createUser)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //     return   ResponseViewModel<UserViewModel>.Failure(createUser, "ssss", ErrorCodeEnum.AlreadyExist);
        //    }
        //    var MappingDto = createUser.Map<CreateUserDto>();
        //    var result = await _mediator.Send(new RegistrationCommand(MappingDto));
        //    if (!result.IsSuccess)
        //       return ResponseViewModel<UserViewModel>.Failure(createUser, "ssss", ErrorCodeEnum.AlreadyExist);
        //    return ResponseViewModel<UserViewModel>.Success(createUser, "sasasasa");
        //    ;
        //} 

        [HttpPost]
        [Route("Register")]

        public async Task<ResponseViewModel<bool>> Register(RegistratioUserOrchestratorViewModel RegistratioUserOrchestratorViewModel)
        {
            if(!ModelState.IsValid)
            {
             return   ResponseViewModel<bool>.Failure(false, "complete field", ErrorCodeEnum.AlreadyExist);
            }
            var MappingDto = RegistratioUserOrchestratorViewModel.Map<CreateUserViewModel>();
            var result = await _mediator.Send(new RegistrationUserOrchestratorCommand(MappingDto));
            if (!result.IsSuccess)
               return ResponseViewModel<bool>.Failure(false, "ssss", ErrorCodeEnum.AlreadyExist);
            return ResponseViewModel<bool>.Success(true, "sasasasa");
            ;
        }  
        [HttpGet("VerifyEmail")]

        public async Task<ResponseViewModel<bool>> VerifyEmail([FromQuery]Guid token)
        {
       
            var result = await _mediator.Send(new VerifyEmailOrchestratortCommand(token));
            if (!result.IsSuccess)
               return ResponseViewModel<bool>.Failure(false, "Email Verfication Expierd", ErrorCodeEnum.AlreadyExist);
            return ResponseViewModel<bool>.Success(true, "successfull Registration");
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


        #region Forget Password

       // [Authorize]
        [HttpPost("forget-password")]
        public async Task<ResponseViewModel<string>> ForgetPassword(string Email)
        {

            if (string.IsNullOrWhiteSpace(Email))
            {
                return ResponseViewModel<string>.Failure(
                    null,
                    "Email is required.",
                    ErrorCodeEnum.BadRequest);
            }



            var result = await _mediator.Send(new ForgetPasswordCommand(Email));


            if (result.Data == null)
            {
                return ResponseViewModel<string>.Failure(
                    null,
                    "Email not registered.",
                    ErrorCodeEnum.NotFound);
            }

            return ResponseViewModel<string>.Success(result.Data, "OTP sent.");
        }

        #endregion



        #region Reset Password

        [HttpPost("reset-password")]
        public async Task<ResponseViewModel<bool>> ResetPassword( ResetPasswordViewModel model)
        {
            var result = await _mediator.Send(new ResetPasswordCommand(model.Email, model.OTP, model.NewPassword));
            if (result==null)
            {
                return ResponseViewModel<bool>.Failure(false, "Failed To rest Password");

            }
            return ResponseViewModel<bool>.Success(true, "Password reset successfully");
        }
        #endregion

    }
}
