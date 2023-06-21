using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Foundation.API.Behaviors;
using Foundation.API.Configurations;
using Foundation.API.Extensions;
using Foundation.Core.Extensions;
using Foundation.Core.Interfaces;
using Foundation.Data.Extensions;
using Foundation.Data.Interfaces;
using Foundation.Data.Types;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TodoApp.Data.Context;
using TodoApp.Data.Repositories;
using TodoApp.Service;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.Configuration;
using FluentValidation.AspNetCore;
using Foundation.API.Types;
using Foundation.Core.Types;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Domain.Commands.Todo;
using TodoApp.Domain.Queries.Todo;
using TodoApp.Service.Validations.Commands.Todo;
using TodoApp.Service.Validations.Queries.Todo;

namespace TodoApp
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
            ConfigurationManager builderConfiguration)
        {
            var configuration = builderConfiguration;
            
            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddDbContext<TodoDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(TodoDbContextFactory).Assembly.ToString());
                        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30),
                            Array.Empty<string>());
                        sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory",
                            DbContextExtensions.GetDefaultSchema<TodoDbContext>());
                    });
            });

            services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        try
                        {
                            var errors = context.ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => JsonSerializer.Deserialize<Error>(e.ErrorMessage));
                            return new BadRequestObjectResult(errors);
                        }
                        catch (Exception e)
                        {
                            return new UnprocessableEntityResult();
                        }
                    };
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            
            services.AddTransient<IValidatorInterceptor, FluentValidatorInterceptorExtension>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddFluentValidationRulesToSwagger();
            
            services.AddEndpointsApiExplorer();
            services.AddMediatRConfiguration();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TodoApp - TodoApp HTTP API",
                    Version = "v1",
                    Description = "The TodoApp HTTP API"
                });
                options.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "time-span"
                });
                options.DescribeAllParametersInCamelCase();
                options.AddCustomizations();
                options.UseOneOfForPolymorphism();
                options.UseAllOfForInheritance();
                options.SelectDiscriminatorNameUsing(_ => "discriminator");
            });
            services.Scan(scan =>
            {
                scan.FromAssembliesOf(typeof(TodoService))
                    .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>))).AsImplementationOfInterface(typeof(IRequestHandler<,>)).WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IMap<,>))).AsImplementationOfInterface(typeof(IMap<,>)).WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>))).AsImplementationOfInterface(typeof(IValidator<>)).WithScopedLifetime();
            });
            
            // Commands Validators
            services.AddScoped<IValidator<CreateTodoCommand>, CreateTodoCommandValidator>();
            services.AddScoped<IValidator<UpdateTodoCommand>, UpdateTodoCommandValidator>();
            services.AddScoped<IValidator<DeleteTodoCommand>, DeleteTodoCommandValidator>();
            
            // Queries Validators
            services.AddScoped<IValidator<GetTodoByIdQuery>, GetTodoByIdQueryValidator>();

            // Repositories
            // =========================================================================================================================================
            services.AddScoped<ITodoRepository, TodoRepository>();
            // =========================================================================================================================================

            services.AddScoped<IUnitOfWork, UnitOfWork<TodoDbContext>>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            services.AddMemoryCache();

            services.Configure<KestrelServerLimits>(options =>
            {
                options.MaxRequestBodySize = null;
                options.KeepAliveTimeout = TimeSpan.FromMinutes(10);
            });

            // Enabling Cors for all incoming requests
            services.AddCors(options =>  
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
            services.AddLogging();
            return services;
        }
    }
}