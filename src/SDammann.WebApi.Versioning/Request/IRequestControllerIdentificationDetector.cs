﻿namespace SDammann.WebApi.Versioning.Request {
    using System.Net.Http;

    /// <summary>
    /// Defines the interface for classes which detect controller name and version from requests
    /// </summary>
    public interface IRequestControllerIdentificationDetector
    {
        /// <summary>
        /// Gets a name for the controller from the specified request message. Returns null if no controller identification could be detected.
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        ControllerIdentification GetIdentification(HttpRequestMessage requestMessage);
    }
}