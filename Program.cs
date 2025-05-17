var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("UserList"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<LoggingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();