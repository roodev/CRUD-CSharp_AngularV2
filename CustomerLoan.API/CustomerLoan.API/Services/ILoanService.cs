namespace CustomerLoan.API.Services
{
    public interface ILoanService<T>
    {
        bool Add(T register);
        bool Delete(int id);
        bool Update(T register);
        T GetLoanById(int id);
        IEnumerable<T> List();
    }
}
