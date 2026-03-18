using URLShortener;
using URLShortener.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAntiforgery();

builder.Services.AddCors(options =>
{
    options.AddPolicy(builder.Configuration["CorsPolicy:PolicyName"]!,
        policyBuilder => policyBuilder.WithOrigins(builder.Configuration["CorsPolicy:Origin"]!)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseCors(builder.Configuration["CorsPolicy:PolicyName"]!);
app.UseAntiforgery();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
