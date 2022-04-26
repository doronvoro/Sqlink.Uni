namespace Sqlink.Uni.BL
{
    public interface IGenericRepositoryFactory<T> where T : BaseEntity
    {
        public IGenericRepository<T> GetRepository();
    }
}