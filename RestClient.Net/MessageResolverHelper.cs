namespace RestClient.Net
{
    internal static class MessageResolverHelper
    {
        internal static IMessageResolver GetMessageResolver(this IMessageResolver messageResolver, RestClientDefaultSettings configuration)
        {
            IMessageResolver result;

            if (configuration?.DefaultMessageResolver != null && messageResolver == null)
            {
                result = configuration.DefaultMessageResolver;
            }
            else if (messageResolver != null)
            {
                result = messageResolver;
            }
            else
            {
                result = new JsonMessageResolver();
            }

            return result;
        }
    }
}
