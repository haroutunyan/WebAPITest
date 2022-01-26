using Domain.Categories;
using Domain.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public static class Extensions
    {
        public static IServiceCollection AddDomainLayerServices(this IServiceCollection services)
        {
            return services.AddScoped<IUserManager, UserManager>()
                .AddScoped<ICategoryManager, CategoryManager>();
        }
    }
}
