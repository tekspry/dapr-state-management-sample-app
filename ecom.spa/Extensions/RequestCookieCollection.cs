using ecom.spa.Models;

namespace ecom.spa.Extensions
{
    public static class RequestCookieCollection
    {
        public static Guid GetCurrentCartId(this IRequestCookieCollection cookies, Configurations config)
        {
            Guid.TryParse(cookies[config.CartIdCookieName], out Guid cartId);
            return cartId;
        }
    }
}
