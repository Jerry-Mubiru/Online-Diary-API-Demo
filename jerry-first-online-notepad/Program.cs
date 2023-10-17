using jerry_first_online_notepad.Services;
using System;

namespace jerry_first_online_notepad
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Get port from environment variable or default to 8080
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(int.Parse(port));
            });

            // Add services to the container.
            //Register Client Creator
            builder.Services.AddSingleton<ClientCreator>();
            //Register Storage Client
            builder.Services.AddSingleton(x => x.GetRequiredService<ClientCreator>().CreateClient());

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

            // Comment out the HTTPS Redirection for App Engine deployment
            //app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
