﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPay.BO.DTOs.Auth.Request;
using SPay.BO.DTOs.Auth.Response;
using SPay.Repository.Enum;
using SPay.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPay.API.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IUserService _service;
		
		public AuthenticationController(IUserService service)
		{
			_service = service;
		}

		[AllowAnonymous]
		[HttpPost("/login")]
		[ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
		[ProducesErrorResponseType(typeof(UnauthorizedObjectResult))]
		public async Task<IActionResult> Login(LoginRequest loginRequest)
		{
			var loginResponse = await _service.Login(loginRequest);
			if (loginResponse == null)
			{
				return Unauthorized(new
				{
					StatusCode = StatusCodes.Status401Unauthorized,
					Error = MessageConstant.LoginMessage.InvalidEmailOrPassword,
					TimeStamp = DateTime.Now
				});
			}
			if (loginResponse.Status.Equals(UserStatusEnum.Banned))
				return Unauthorized(new
				{
					StatusCode = StatusCodes.Status401Unauthorized,
					Error = MessageConstant.LoginMessage.AccountIsUnavailable,
					TimeStamp = DateTime.Now
				});
			return Ok(loginResponse);
		}

		//[AllowAnonymous]
		//[HttpPost("/sign-up")]
		//[ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
		//[ProducesErrorResponseType(typeof(UnauthorizedObjectResult))]
		//public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
		//{
		//	try
		//	{
		//		var response = await _accountService.SignUp(signUpRequest);
		//		return Ok(response);
		//	}
		//	catch (Exception ex)
		//	{
		//		return BadRequest(new ErrorResponse()
		//		{
		//			StatusCode = StatusCodes.Status400BadRequest,
		//			Error = MessageConstant.SignUpMessage.EmailHasAlreadyUsed,
		//			TimeStamp = DateTime.Now
		//		});
		//	}
		//}
	}
}
