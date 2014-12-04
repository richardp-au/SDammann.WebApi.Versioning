﻿namespace SDammann.WebApi.Versioning.Internal {
    using System;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dependencies;
    using Configuration;
    using Discovery;
    using Request;

    internal static class ServicesExtensions {
        public static IControllerIdentificationDetector GetControllerIdentificationDetector(this IDependencyResolver servicesContainer) {
            return servicesContainer.GetServiceOrThrow<IControllerIdentificationDetector>(ApiVersioning.Configuration.ControllerIdentificationDetectorType);
        }

        public static IControllerNameDetector GetControllerNameDetector(this IDependencyResolver servicesContainer)
        {
            return servicesContainer.GetServiceOrThrow<IControllerNameDetector>(ApiVersioning.Configuration.ControllerNameDetectorType);
        }

        public static IControllerVersionDetector GetControllerVersionDetector(this IDependencyResolver servicesContainer)
        {
            return servicesContainer.GetServiceOrThrow<IControllerVersionDetector>(ApiVersioning.Configuration.ControllerVersionDetectorType);
        }

        public static IRequestControllerNameDetector GetRequestControllerNameDetector(this IDependencyResolver servicesContainer)
        {
            return servicesContainer.GetServiceOrThrow<IRequestControllerNameDetector>(ApiVersioning.Configuration.RequestControllerNameDetectorType);
        }

        public static IRequestVersionDetector GetRequestControllerVersionDetector(this IDependencyResolver servicesContainer)
        {
            return servicesContainer.GetServiceOrThrow<IRequestVersionDetector>(ApiVersioning.Configuration.RequestVersionDetector);
        }

        public static IRequestControllerIdentificationDetector GetRequestControllerIdentificationDetector(this IDependencyResolver servicesContainer)
        {
            return servicesContainer.GetServiceOrThrow<IRequestControllerIdentificationDetector>(ApiVersioning.Configuration.RequestControllerIdentificationDetectorType);
        }

        private static TService GetServiceOrThrow<TService>(this IDependencyResolver servicesContainer, Type clrType) where TService : class
        {
            clrType = clrType ?? typeof (TService);

            // get by DI
            var service = servicesContainer.GetService(clrType) as TService;
            if (service == null) {
                // get by activation of parameterless constructor
                try {
                    service = Activator.CreateInstance(clrType) as TService;
                }
                catch (Exception) {
                    throw new InvalidOperationException(
                        String.Format(ExceptionResources.DependencyContainerNotConfigured, clrType.FullName));
                }
            }

            return service;
        }
    }
}