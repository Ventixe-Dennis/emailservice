using Azure.Communication.Email;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSingleton(x => new EmailClient(builder.Configuration["Azure:EmailConnectionString"]));
builder.Services.AddSingleton<ICommunicationService, CommunicationService>();


var app = builder.Build();


app.MapOpenApi();
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
