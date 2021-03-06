﻿using Microsoft.Extensions.DependencyInjection;
using SlimFormaturas.Infra.CrossCutting.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlimFormaturas.Api.Configurations {
    public static class DependencyInjectionConfig {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services) {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
