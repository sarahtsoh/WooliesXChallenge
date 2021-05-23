using Microsoft.Extensions.Logging;
using WooliesChallenge.Models;
using WooliesChallenge.Service.ApiResources;

namespace WooliesChallenge.Service
{
    public interface ISortMethodFactory
    {
        ISortMethod CreateSortMethod(SortOption.Option sortOption);
    }

    public class SortMethodFactory : ISortMethodFactory
    {
        private IApiResource apiResource;
        private ILogger<SortMethodFactory> logger;
        public SortMethodFactory( IApiResource apiResource, ILogger<SortMethodFactory> logger)
        {
            this.apiResource =  apiResource;
            this.logger = logger;
        }
        public ISortMethod CreateSortMethod(SortOption.Option sortOption)
        {
            ISortMethod sortMethod;
            switch (sortOption)
            {
                case SortOption.Option.Ascending:
                    sortMethod = new AscendingSort(apiResource);
                    break;
                case SortOption.Option.Descending:
                    sortMethod = new DecendingSort(apiResource);
                    break;
                case SortOption.Option.Low:
                    sortMethod = new LowToHighSort(apiResource);
                    break;
                case SortOption.Option.High:
                    sortMethod = new HighToLowSort(apiResource);
                    break;
                case SortOption.Option.Recommended:
                    sortMethod = new RecommendedSort(apiResource);
                    break;

                default:
                    sortMethod = new AscendingSort(apiResource);
                    break;
            }

            return sortMethod;
        }
    }
}