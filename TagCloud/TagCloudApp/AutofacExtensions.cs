using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Metadata;

namespace TagCloudApp
{
    public static class AutofacExtensions
    {
        private const string OrderString = "WithOrderTag";
        private static int orderCounter;

        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
            WithOrder<TLimit, TActivatorData, TRegistrationStyle>(
                this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder)
        {
            return registrationBuilder.WithMetadata(OrderString, Interlocked.Increment(ref orderCounter));
        }

        public static IOrderedEnumerable<TComponent> ResolveOrdered<TComponent>(this IComponentContext context)
        {
            return
                context.Resolve<IEnumerable<Meta<TComponent>>>()
                       .OrderBy(m => m.Metadata[OrderString])
                       .Select(m => m.Value).OrderBy(c => true);
        }
    }
}