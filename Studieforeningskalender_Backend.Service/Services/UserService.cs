using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Studieforeningskalender_Backend.Domain.UserRoles;
using Studieforeningskalender_Backend.Domain.Users;
using Studieforeningskalender_Backend.Service.Helpers;
using static Studieforeningskalender_Backend.Domain.Users.UserPayload;

namespace Studieforeningskalender_Backend.Service.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRoleRepository _userRoleRepository;
		private readonly IUserRoleService _userRoleService;
		private readonly IRoleService _roleService;
		private readonly IUserRepository _repository;
		private readonly IEncryptionService _encryptionService;
		private readonly IEmailService _emailService;
		private readonly IReCaptchaService _reCaptchaService;
		private readonly HttpContext _httpContext;

		public UserService(IUserRepository repository, IUserRoleRepository userRoleRepository, IRoleService roleService,
			IEncryptionService encryptionService, IEmailService emailService, IUserRoleService userRoleService,
			IReCaptchaService reCaptchaService, IHttpContextAccessor contextAccessor)
		{
			_repository = repository;
			_userRoleRepository = userRoleRepository;
			_roleService = roleService;
			_encryptionService = encryptionService;
			_emailService = emailService;
			_userRoleService = userRoleService;
			_reCaptchaService = reCaptchaService;
			_httpContext = contextAccessor.HttpContext;
		}

		public IQueryable<User> GetUserInfo()
		{
			var username = _httpContext?.User.Claims.First(c => c.Type == "UserName").Value;

			return _repository.GetUser().Where(user => user.UserName == username);
		}

		public IQueryable<User> GetUsers() =>
			_repository.GetUsers();

		public async Task<CreateUserPayload> CreateUser(CreateUserInput input)
		{
			var reCaptchaVerified = await _reCaptchaService.ValidateReCaptcha(input.RecaptchaToken);
			if (!reCaptchaVerified)
				return new CreateUserPayload(false, "The reCAPTCHA seems to think you are a robot, touch grass and try again");

			if (_repository.UsernameIsInUse(input.UserName))
				return new CreateUserPayload(false, "The username is already in use");
			else if (_repository.EmailIsInUse(input.EmailAddress))
				return new CreateUserPayload(false, "Another account already exists with this email address");

			var item = new User
			{
				Id = Guid.NewGuid(),
				UserName = input?.UserName,
				Password = PasswordHelper.HashPassword(input.Password),
				EmailAddress = input.EmailAddress,
				FirstName = input.FirstName,
				LastName = input.LastName,
			};

			var emailSent = await _emailService.SendRegisterVerificationEmail(input.EmailAddress, item.Id, input.FirstName);

			if (!emailSent.isSuccessful)
				return new CreateUserPayload(false, emailSent.message);

			await _repository.CreateUser(item);
			return new CreateUserPayload(true);
		}

		public async Task<VerifyUserPayload> VerifyUser(string emailAddress, string token)
		{
			var user = _repository
				.GetUsers()
				.Where(user => user.EmailAddress == emailAddress)
				.Select(user => new { user.Id, user.UserName, isVerified = user.UserRoles.Any(ur => ur.Role.Name == "user") })
				.FirstOrDefault();

			if (user == null)
				return new VerifyUserPayload(false, "No user with this email address exists");
			if (user.isVerified)
				return new VerifyUserPayload(false, "User is already verified");

			var plainText = _encryptionService.Decrypt(token);
			var (userId, timestamp) = (plainText[..36], plainText[36..]);

			if (user.Id.ToString() != userId)
				return new VerifyUserPayload(false, "The email provided does not match the email used in the verification");
			if (DateTime.Parse(timestamp) < DateTime.UtcNow.AddHours(-1))
				return new VerifyUserPayload(false, "It seems that the verification token has been tampered with");
			if (DateTime.Parse(timestamp) > DateTime.UtcNow.AddHours(1))
				return new VerifyUserPayload(false, "The verification token has expired");

			var res = await _userRoleService.GrantRoleToUser(new RoleAndUserInput(user.UserName, "user"));
			if (!res.isSuccessful)
				return new VerifyUserPayload(false, "An error occurred while marking the user as verified");

			return new VerifyUserPayload(true);
		}

		public async Task<DeleteUserPayload> DeleteUser()
		{
			var username = _httpContext?.User.Claims.First(c => c.Type == "UserName").Value;
			if (string.IsNullOrEmpty(username))
				return new DeleteUserPayload(false, "An error occurred while reading the user token try logging out and then back in");

			var user = _repository
				.GetUser()
				.Where(user => user.UserName == username)
				.FirstOrDefault();

			if (user == null)
				return new DeleteUserPayload(false, "An error occurred while trying ti find your user, try logging out and then back in and try again");

			await _repository.DeleteUser(user);
			await SignOut();
			return new DeleteUserPayload(true);
		}

		public async Task<UpdateUserPayload> UpdateUser(UpdateUserInput input)
		{
			var username = _httpContext?.User.Claims.First(c => c.Type == "UserName").Value;
			if (string.IsNullOrEmpty(username))
				return new UpdateUserPayload(false, "An error occurred while reading the user token try logging out and then back in");

			var user = _repository.GetUserByUsername(username);

			if (user != null)
			{
				if (!string.IsNullOrEmpty(input.FirstName)) user.FirstName = input.FirstName;
				if (!string.IsNullOrEmpty(input.LastName)) user.LastName = input.LastName;
				if (!string.IsNullOrEmpty(input.UserName))
				{
					if (_repository.GetUserByUsername(input.UserName) != null)
						user.UserName = input.UserName;
					else return new UpdateUserPayload(false, "A user with this username already exists");
				}

				await _repository.SaveChangesAsync();
			}
			else return new UpdateUserPayload(false, "A user with this id was not found");

			return new UpdateUserPayload(true);
		}

		public async Task<ChangePasswordPayload> ChangePassword(string emailAddress, string verificationCode, string password, string reCaptcha)
		{
			var reCaptchaVerified = await _reCaptchaService.ValidateReCaptcha(reCaptcha);
			if (!reCaptchaVerified)
				return new ChangePasswordPayload(false, "The reCAPTCHA seems to think you are a robot, touch grass and try again");

			var user = _repository.GetUserByEmailAddress(emailAddress);

			if (user == null)
				return new ChangePasswordPayload(false, "No user with this email address exists");

			var plainText = _encryptionService.Decrypt(verificationCode);
			var (userId, timestamp) = (plainText[..36], plainText[36..]);

			if (user.Id.ToString() != userId)
				return new ChangePasswordPayload(false, "The email provided does not match the email used in the verification");
			if (DateTime.Parse(timestamp) < DateTime.UtcNow.AddHours(-1))
				return new ChangePasswordPayload(false, "It seems that the verification token has been tampered with");
			if (DateTime.Parse(timestamp) > DateTime.UtcNow.AddHours(1))
				return new ChangePasswordPayload(false, "The verification token has expired");

			user.Password = PasswordHelper.HashPassword(password);
			await _repository.SaveChangesAsync();

			return new ChangePasswordPayload(true);
		}

		public async Task<LoginPayload> Login(LoginInput loginInput)
		{
			var reCaptchaVerified = await _reCaptchaService.ValidateReCaptcha(loginInput.RecaptchaToken);
			if (!reCaptchaVerified)
				return new LoginPayload(false, "The reCAPTCHA seems to think you are a robot, touch grass and try again");

			var user = _repository.GetUserByUsername(loginInput.UserName);

			if (user == null)
				return new LoginPayload(false, "Username or password is incorrect");

			if (!PasswordHelper.ValidatePasswordHash(loginInput.Password, user.Password))
				return new LoginPayload(false, "Username or password is incorrect");

			var roleIds = _userRoleRepository.GetRoleId(user.Id);
			var roles = _roleService.GetRolesById(roleIds);

			TokensHelper.SignInAsync(user, roles, _httpContext, loginInput.RememberMe);

			var userTokenPayload = new LoginPayload(true);

			await _repository.SaveChangesAsync();

			return userTokenPayload;
		}

		public async Task SignOut() =>
			await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
	}
}