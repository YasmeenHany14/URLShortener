using System.Security.Claims;
using URLShortener;
using URLShortener.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAntiforgery();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityData.AdminUserPolicyName, p =>
    {
        p.RequireClaim(ClaimTypes.Role, "Admin");
    });
    options.AddPolicy(IdentityData.OwnerUserPolicyName, policy => policy.RequireRole("User"));
});

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowLocalhost4200",
//         policyBuilder => policyBuilder.WithOrigins("http://localhost:4200") // Allow localhost:4200
//             .AllowAnyHeader() // Allow any headers
//             .AllowAnyMethod()); // Allow any methods
// });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseCors("AllowLocalhost4200");
app.UseExceptionHandler();
app.UseAntiforgery();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();