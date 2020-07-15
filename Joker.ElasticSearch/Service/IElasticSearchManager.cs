using System.Collections.Generic;
using System.Threading.Tasks;
using Joker.ElasticSearch.Models;

namespace Joker.ElasticSearch.Service
{
    public interface IElasticSearchManager
    {
        Task CreateIndexAsync<T, TKey>(string indexName) where T : ElasticEntity<TKey>;
        Task DeleteIndexAsync(string indexName);
        Task AddOrUpdateAsync<T, TKey>(string indexName, T model) where T : ElasticEntity<TKey>;
        Task DeleteAsync<T, TKey>(string indexName, string typeName, T model) where T : ElasticEntity<TKey>;
        Task ReIndex<T, TKey>(string indexName) where T : ElasticEntity<TKey>;
        Task CrateIndexAsync(string indexName);
        Task ReBuild<T, TKey>(string indexName) where T : ElasticEntity<TKey>;
        Task CreateIndexSuggestAsync<T, TKey>(string indexName) where T : ElasticEntity<TKey>;
        Task CreateIndexCustomSuggestAsync<T, TKey>(string indexName) where T : ElasticEntity<TKey>;
        Task BulkAddOrUpdateAsync<T, TKey>(string indexName, List<T> list, int bulkNum = 1000) where T : ElasticEntity<TKey>;
        Task BulkDeleteAsync<T, TKey>(string indexName, List<T> list, int bulkNum = 1000) where T : ElasticEntity<TKey>;
    }
}