using AutoMapper;
using Game.Forum.Application.Behaviors;
using Game.Forum.Application.Exceptions;
using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.RequestModels.Users;
using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Application.Validators.Users;
using Game.Forum.Application.Wrapper;
using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;
using Game.Forum.Domain.UnitofWork;
using Game.Forum.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Game.Forum.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;
        private readonly IUnitofWork _uWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UserId => GetClaim(ClaimTypes.PrimarySid) != null ? int.Parse(GetClaim(ClaimTypes.PrimarySid)) : null;

        public Roles? Role => GetClaim(ClaimTypes.Role) != null ? (Roles)Enum.Parse(typeof(Roles), GetClaim(ClaimTypes.Role)) : null;

        public string Username => GetClaim(ClaimTypes.Name) != null ? GetClaim(ClaimTypes.Name) : null;

        public string Email => GetClaim(ClaimTypes.Email) != null ? GetClaim(ClaimTypes.Email) : null;



        private string GetClaim(string claimType)
        {
            return _httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;
        }

        public UserService(IMapper mapper, IUserRepository userRepo, IConfiguration configuration)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _configuration = configuration;

        }

        #region Yeni bir kullanıcı oluşturmak için kullanılan metod

        [ValidationBehavior(typeof(RegisterValidator))]


        public async Task<Result<bool>> Register(RegisterVM registerVM)
        {
            var result = new Result<bool>();
            var addUser = _mapper.Map<User>(registerVM);

            var usernameExists = await _uWork.GetRepository<User>().AnyAsync(x => x.UserName.Trim().ToUpper() == registerVM.Username.Trim().ToUpper());
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

            userEntity.Password = CipherUtil
                .EncryptString(_configuration["AppSettings:SecretKey"], userEntity.Password);

            _uWork.GetRepository<User>().Add(userEntity);
            result.Data = await _uWork.CommitAsync();

            return result;
        }
        #endregion

        #region Gönderilen kullanıcı adı ve parola ile login işlemini gerçekleştirir.

        public async Task<Result<User>> Login(LoginVM loginVM)
        {
            var result = new Result<User>();
            var hashedPassword = CipherUtil.EncryptString(_configuration["AppSettings:SecretKey"], loginVM.Password);
            var existsAccount = await _uWork.GetRepository<User>().GetByFilterAsync(x => x.UserName == loginVM.Username && x.Password == hashedPassword, "Customer");

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

            var existsCustomer = await _userRepo.GetRepository<User>().GetById(updateUserVM.Id.Value);

            _mapper.Map(updateUserVM, existsCustomer);

            _userRepo.GetRepository<User>().Update(existsCustomer);
            result.Data = await _userRepo.CommitAsync();

            return result;
        }
        #endregion

        #region Kullanıcı bilgilerini silmek için kullanılan servis metodu.
        public async Task DeleteUser(DeleteDto deleteDto)
        {
            var dbUser = await _userRepo.GetByIdAsync(deleteDto.Id);
            if (dbUser?.IsDeleted == true) { throw new ClientSideException("Kullanıcı bulunamadı"); };
            await _userRepo.RemoveAsync(dbUser);
        }
        
        #endregion

    }
}
