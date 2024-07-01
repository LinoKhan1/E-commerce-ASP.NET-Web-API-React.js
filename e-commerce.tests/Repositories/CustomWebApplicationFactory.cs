using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using e_commerce.Server.Services.Interfaces;
using e_commerce.Server.Repositories.Interfaces;
using Moq;
using e_commerce.Server.Repositories;
using e_commerce.Server.Services;
using e_commerce.Server.Data;
using Microsoft.EntityFrameworkCore;
using e_commerce.Server.uniOfWork;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the app's ApplicationDbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add ApplicationDbContext using in-memory database for testing
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Add mock implementations for testing
            var cartRepositoryMock = new Mock<ICartRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var cartServiceMock = new Mock<ICartService>();

            // Setup mock methods if needed
            services.Replace(ServiceDescriptor.Singleton(cartRepositoryMock.Object));
            services.Replace(ServiceDescriptor.Singleton(unitOfWorkMock.Object));
            services.Replace(ServiceDescriptor.Singleton(cartServiceMock.Object));
        });
    }
}
