using NotificationService.BackgroundServices;
using NotificationService.Services;

var builder =
    WebApplication
        .CreateBuilder(args);

builder.Services
    .AddControllers();

builder.Services
    .AddEndpointsApiExplorer();

builder.Services
    .AddSwaggerGen();

builder.Services
    .AddScoped<
        IEmailService,
        EmailService>();

builder.Services
    .AddHostedService<
        OrderCreatedConsumer>();

var app =
    builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

app.Run();