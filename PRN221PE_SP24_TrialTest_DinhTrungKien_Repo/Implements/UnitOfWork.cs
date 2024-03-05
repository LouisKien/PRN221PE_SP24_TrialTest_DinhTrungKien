using Microsoft.EntityFrameworkCore;
using PRN221PE_SP24_TrialTest_DinhTrungKien.Repo.Interfaces;
using PRN221PE_SP24_TrialTest_DinhTrungKien_Repo.Models;

namespace PRN221PE_SP24_TrialTest_DinhTrungKien.Repo.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Eyeglasses2024DBContext _dbContext;
        private IGenericRepository<Eyeglass> _eyeglassRepository;
        private IGenericRepository<LensType> _lensTypeRepository;
        private IGenericRepository<StoreAccount> _storeAccountRepository;

        public UnitOfWork(Eyeglasses2024DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<Eyeglass> EyeglassRepository => _eyeglassRepository ??= new GenericRepository<Eyeglass>(_dbContext);

        public IGenericRepository<LensType> LensTypeRepository => _lensTypeRepository ??= new GenericRepository<LensType>(_dbContext);

        public IGenericRepository<StoreAccount> StoreAccountRepository => _storeAccountRepository ??= new GenericRepository<StoreAccount>(_dbContext);

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
