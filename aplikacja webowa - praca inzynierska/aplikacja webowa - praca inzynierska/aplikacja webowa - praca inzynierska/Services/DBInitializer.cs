using aplikacja_webowa___praca_inzynierska.Model;
using LiteDB;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace aplikacja_webowa___praca_inzynierska.Services
{
    public class DBInitializer : IHostedService
    {
        private readonly ILiteDatabase _liteDatabase;

        public DBInitializer(ILiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            InitializeRegistriesCollection();
            InitializeTaxonomyCodesCollection();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _liteDatabase.Dispose();
            return Task.CompletedTask;
        }

        private void InitializeRegistriesCollection()
        {
            var registries = _liteDatabase.GetCollection<RegisterEntry>();

            //szybsze wyszukiwanie po id
            registries.EnsureIndex(x => x.Id);

            //szybsze wyszukiwanie po id taxonomy
            registries.EnsureIndex(x => x.TaxonomyID);
        }

        private void InitializeTaxonomyCodesCollection()
        {
            _liteDatabase.Mapper.Entity<TaxonomyCode>().Id(x => x.Code);

            var codes = _liteDatabase.GetCollection<TaxonomyCode>();

            codes.EnsureIndex(x => x.Code);
        }
    }
}
