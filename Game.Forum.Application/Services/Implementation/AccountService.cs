using AutoMapper;
using Game.Forum.Application.Behaviors;
using Game.Forum.Application.Exceptions;
using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.RequestModels.Accounts;
using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Application.Validators.Accounts;
using Game.Forum.Application.Wrapper;
using Game.Forum.Domain.Entities;
using Game.Forum.Domain.UnitofWork;
using Game.Forum.Utils;
using Microsoft.Extensions.Configuration;

namespace Game.Forum.Application.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUnitofWork _uWork;


        public AccountService(IMapper mapper, IConfiguration configuration, IUnitofWork uWork)
        {
            _mapper = mapper;
            _configuration = configuration;
            _uWork = uWork;
        }

        #region Yeni bir kullanıcı oluşturmak için kullanılan metod

        [ValidationBehavior(typeof(RegisterValidator))]
        public async Task<Result<bool>> Register(RegisterVM registerVM)
        {
            var result = new Result<bool>();

            var usernameExists = await _uWork.GetRepository<Account>().AnyAsync(x => x.Username.Trim().ToUpper() == registerVM.Username.Trim().ToUpper());
            if (usernameExists)
            {
                throw new AlreadyExistsException($"{registerVM.Username} kullanıcı adı daha önce seçilmiştir. Lütfen farklı bir kullanıcı adı belirleyiniz.");
            }

            var emailExists = await _uWork.GetRepository<User>().AnyAsync(x => x.Email.Trim().ToUpper() == registerVM.Email.Trim().ToUpper());
            if (emailExists)
            {
                throw new AlreadyExistsException($"{registerVM.Email} eposta adresi kullanılmaktadır. Lütfen farklı bir kullanıcı adı belirleyiniz.");
            }

            var userEntity = _mapper.Map<User>(registerVM);
            var accountEntity = _mapper.Map<Account>(registerVM);

            accountEntity.Password = CipherUtil
                .EncryptString(_configuration["AppSettings:SecretKey"], accountEntity.Password);

            accountEntity.User = userEntity;
                        
            _uWork.GetRepository<User>().Add(userEntity);
            _uWork.GetRepository<Account>().Add(accountEntity);
            result.Data = await _uWork.CommitAsync();

            return result;
        }
        #endregion

        #region Gönderilen kullanıcı adı ve parola ile login işlemini gerçekleştirir.

        [ValidationBehavior(typeof(LoginValidator))]
        public async Task<Result<bool>> Login(LoginVM loginVM)
        {
            var result = new Result<bool>();
            var hashedPassword = CipherUtil.EncryptString(_configuration["AppSettings:SecretKey"], loginVM.Password);
            var existsAccount = await _uWork.GetRepository<Account>().GetSingleByFilterAsync(x => x.Username == loginVM.Username && x.Password == hashedPassword, "User");

            if (existsAccount is null)
            {
                throw new NotFoundException($"{loginVM.Username} kullanıcı adına sahip kullanıcı bulunamadı ye da parola hatalıdır.");
            }
         
            return result;
        }

        #endregion

        #region Kullanıcı bilgilerini güncellemek için kullanılan servis metodu.

        [ValidationBehavior(typeof(UpdateUserValidator))]
        public async Task<Result<bool>> UpdateUser(UpdateUserVM updateUserVM)
        {
            var result = new Result<bool>();

            var existsUser = await _uWork.GetRepository<Account>().GetById(updateUserVM.Id.Value);

            _mapper.Map(updateUserVM, existsUser);

            _uWork.GetRepository<Account>().Update(existsUser);
            result.Data = await _uWork.CommitAsync();

            return result;
        }
        #endregion

        #region Kullanıcı bilgilerini silmek için kullanılan servis metodu.

        [ValidationBehavior(typeof(DeleteUserValidator))]
        public async Task DeleteUser(DeleteDto deleteDto)
        {
            var dbUser = await _uWork.GetRepository<Account>().GetById(deleteDto.Id);
            if (dbUser?.IsDeleted == true) { throw new ClientSideException("Kullanıcı bulunamadı"); };
            _uWork.GetRepository<Account>().Delete(dbUser);
        }

        #endregion
        
    }
}
