using EquityAfia.EmailsNotifications.Application.interfaces;
using EquityAfia.EmailsNotifications.Application.SendEmail.Command;
using EquityAfia.EmailsNotifications.Domain.EmailRequestAndResponse;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using TeleAfiaPersonal.Contracts.Email;
using TeleAfiaPersonal.Infrastructure.EmailSender;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Email Configuration
var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

// Add MediatR for handling commands and queries
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Register command and query handlers
builder.Services.AddTransient<IRequestHandler<SendEmailCommand, SendEmailResponse>, SendEmailCommandHandler>();


//Services
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailService, EmailService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
