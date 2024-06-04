using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Studieforeningskalender_Backend.Domain.Interfaces.Repositories;
using Studieforeningskalender_Backend.Domain.Interfaces.Services;
using Studieforeningskalender_Backend.Infrastructure;
using Studieforeningskalender_Backend.Infrastructure.Helpers;
using Studieforeningskalender_Backend.Infrastructure.Repositories;
using Studieforeningskalender_Backend.Service.ErrorFilters;
using Studieforeningskalender_Backend.Service.Helpers;
using Studieforeningskalender_Backend.Service.Interceptors;
using Studieforeningskalender_Backend.Service.Middleware;
using Studieforeningskalender_Backend.Service.Services;
using Studieforeningskalender_Backend.Service.Validators.Event;
using Studieforeningskalender_Backend.Service.Validators.EventTag;
using Studieforeningskalender_Backend.Service.Validators.EventUser;
using Studieforeningskalender_Backend.Service.Validators.Role;
using Studieforeningskalender_Backend.Service.Validators.Tag;
using Studieforeningskalender_Backend.Service.Validators.User;
using Studieforeningskalender_Backend.Service.Validators.UserRole;
using System.Security.Claims;

string corsPolicyName = "corsPolicy";

var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsDevelopment())
	await Secrets.SetSecrets(builder);

string AllowedOrigin = builder.Configuration.GetValue<string>("AllowedOrigin");

builder.Services.AddDbContext<AppDbContext>(options =>
		options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))
		.AddInterceptors(new DbSaveInterceptor()));

builder.Services.AddControllers();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddTransient<IEventTagRepository, EventTagRepository>();
builder.Services.AddTransient<IEventUserRepository, EventUserRepository>();
builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IEmailReputationRepository, EmailReputationRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRoleService, UserRoleService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<IEventTagService, EventTagService>();
builder.Services.AddTransient<IEventUserService, EventUserService>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IEmailReputationService, EmailReputationService>();
builder.Services.AddTransient<IReCaptchaService, ReCaptchaService>();
builder.Services.AddTransient<IChatGPTService, ChatGPTService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpResponseFormatter<HttpResponseFormatter>();
builder.Services.AddHttpClient();

builder.Services.AddGraphQLServer()
		.AddAuthorization()
		.AddInMemorySubscriptions()
		.AddPresentationTypes()
		.AddQueryType(q => q.Name(OperationTypeNames.Query))
		.AddMutationType(q => q.Name(OperationTypeNames.Mutation))
		.AddProjections()
		.AddFiltering()
		.AddSorting()
		.AddGlobalObjectIdentification()
		.UseField<ErrorHandlingMiddleware>()
		.AddFairyBread()
		.AddErrorFilter<AuthErrorFilter>()
		.AddType<UploadType>();

// User validators
builder.Services.AddValidatorsFromAssemblyContaining<LoginInputValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserInputValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserInputValidator>();
// UserRole validators
builder.Services.AddValidatorsFromAssemblyContaining<RoleAndUserInputValidator>();
// Role validators
builder.Services.AddValidatorsFromAssemblyContaining<RoleInputValidator>();
// Event validators
builder.Services.AddValidatorsFromAssemblyContaining<CreateEventInputValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetEventsInputValidator>();
// EventTag validators
builder.Services.AddValidatorsFromAssemblyContaining<EventAndTagInputValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EventAndTagsInputValidator>();
// EventUser validators
builder.Services.AddValidatorsFromAssemblyContaining<AddSelfToEventInputValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddUserToEventInputValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RemoveUserFromEventInputValidator>();
// Tag validators
builder.Services.AddValidatorsFromAssemblyContaining<CreateTagInputValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTagsInputValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DeleteTagInputValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetTagsInputValidator>();

// CORS
builder.Services.AddCors(option =>
{
	option.AddPolicy(name: corsPolicyName, builder => builder.WithOrigins(AllowedOrigin)
													  .AllowAnyMethod()
													  .AllowAnyHeader()
													  .AllowCredentials());
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.Cookie.HttpOnly = true;
		options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
		options.SlidingExpiration = true;
		options.Cookie.SameSite = SameSiteMode.None;
	});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("verified", policy => policy.RequireClaim(ClaimTypes.Role, "user", "studieforening", "admin"));
});

var app = builder.Build();

app.UseCors(corsPolicyName);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseWebSockets();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.MapGraphQL();
app.Run();