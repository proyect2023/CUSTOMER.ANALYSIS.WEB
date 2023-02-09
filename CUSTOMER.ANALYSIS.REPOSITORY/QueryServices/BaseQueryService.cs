

using Microsoft.Extensions.DependencyInjection;

namespace CUSTOMER.ANALYSIS.INFRA.QUERY.QueryServices
{
    public abstract class BaseQueryService
    {
        protected readonly IServiceScopeFactory serviceScopeFactory;

        internal BaseQueryService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
    }
}
