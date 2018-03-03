using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PocketApiV3
{
    public interface IPocketClient : IDisposable
    {
        string ConsumerKey { get; }

        string AccessToken { get; }

        /// <summary>
        /// Pocket has a mobile and desktop version of the authorization page.
        /// Theirs servers will try to detect what type of device the user is
        /// using and redirect accordingly. In cases where we do not detect
        /// screen size (such as on Windows Phone), you can set this to true
        /// to force the mobile version.
        /// </summary>
        bool? IsMobileClient { get; set; }

        /// <summary>
        /// Information on API rate limits.  If set to an instance of PocketLimits,
        /// any server reseponse that contains limit information will set
        /// that information in the PocketLimits instance.
        /// </summary>
        PocketLimits Limits { get; }

        TimeSpan Timeout { get; set; }

        bool? UseInsideWebAuthenticationBroker { get; set; }

        Task<TResponse> SendRequest<TResponse>(Request<TResponse> request, CancellationToken cancellationToken = default)
            where TResponse : Response;

    }
}
