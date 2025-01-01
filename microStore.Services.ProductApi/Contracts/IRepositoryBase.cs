using microStore.Services.ProductApi.Models;
using microStore.Services.ProductApi.Specificatios;

namespace microStore.Services.ProductApi.Contracts
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> ListAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<List<T>> SampleEntities(ISpecification<T> specification);
        Task<T> GetEntityWithSpecification(ISpecification<T> specification);
        Task<List<T>> ListWithSpecificationAsync(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> specification);
        Task<T> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
