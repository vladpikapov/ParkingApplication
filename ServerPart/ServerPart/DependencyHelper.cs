using Microsoft.Extensions.DependencyInjection;
using ServerPart.Data.Context;
using ServerPart.Logic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerPart
{
    public static class DependencyHelper
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<AuthManager>();
            services.AddSingleton<OrderContext>();
            services.AddSingleton<ParkingContext>();
            services.AddSingleton<ParkingRaitingContext>();
            services.AddSingleton<UserContext>();
            services.AddSingleton<OrderManager>();
            services.AddSingleton<ParkingManager>();
        }
    }
}
