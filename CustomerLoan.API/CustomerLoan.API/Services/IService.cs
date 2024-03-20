namespace CustomerLoan.API.Services
{
    public interface IService<T>
    {
        bool Add(T register);
        bool Delete(int id);
        bool Update(T register);
        T GetCustomerById(int id);
        IEnumerable<T> List();
    }
}
