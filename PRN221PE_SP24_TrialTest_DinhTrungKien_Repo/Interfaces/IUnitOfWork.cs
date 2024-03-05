using PRN221PE_SP24_TrialTest_DinhTrungKien_Repo.Models;

namespace PRN221PE_SP24_TrialTest_DinhTrungKien.Repo.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Eyeglass> EyeglassRepository { get; }
        IGenericRepository<LensType> LensTypeRepository { get; }
        IGenericRepository<StoreAccount> StoreAccountRepository { get; }
        void Save();
    }
}
