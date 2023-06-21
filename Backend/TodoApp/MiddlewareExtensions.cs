using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Foundation.API.Extensions;

namespace TodoApp
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureMiddlewares(this WebApplication app)
        {
            var configuration = app.Configuration;
            var env = app.Environment;

            if (!string.IsNullOrEmpty(configuration["BasePath"]))
            {
                app.UsePathBase(configuration["BasePath"]);
            }
            
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint( "/swagger/v1/swagger.json", "Todo API V1", configuration["BasePath"]);
            });
            
            if (configuration.GetValue<bool>("UseTls"))
            {
                if (env.IsProduction())
                {
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }
                app.UseHttpsRedirection();
            }
            
            app.UseCors();
            
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseHttpLogging();
            
            app.MapControllers();

            app.Run();
        }
    }
}