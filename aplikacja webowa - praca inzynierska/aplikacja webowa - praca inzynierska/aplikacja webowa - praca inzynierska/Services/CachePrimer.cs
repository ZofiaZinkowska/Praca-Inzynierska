using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace aplikacja_webowa___praca_inzynierska.Services
{
    public class CachePrimer : IHostedService
    {
        private readonly ITaxonomyProvider _taxonomyProvider;

        public CachePrimer(ITaxonomyProvider taxonomyProvider)
        {
            _taxonomyProvider = taxonomyProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => _taxonomyProvider.GetTaxonomy());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
