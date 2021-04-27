using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template.Core.CustomEntities;
using Template.Core.Entities;
using Template.Core.Exceptions;
using Template.Core.Interfaces;
using Template.Core.QueryFilters;

namespace Template.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public UserService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<User> GetUsers(UserQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var users = _unitOfWork.UserRepository.GetAll();

            if (filters.Query != null)
            {
                users = users.Where(x => x.Email.ToLower().Contains(filters.Query));
            }

            if (filters.Date != null)
            {
                users = users.Where(x => x.CreatedAt.ToShortDateString() == filters.Date?.ToShortDateString());
            }

            var pagedUsers = PagedList<User>.Create(users, filters.PageNumber, filters.PageSize);
            return pagedUsers;
        }



        public async Task<User> LoginUser(User user)
        {
            var userExists = await _unitOfWork.UserRepository.Login(user.Email, user.Password);

            if (userExists.Count() == 1)
            {
                User userS = userExists.FirstOrDefault();

                return userS;
            }
            else
            {
                throw new BusinessException("User Or Password Incorrect");
            }

        }

        public async Task InsertUser(User user)
        {
            var userExists = await _unitOfWork.UserRepository.GetUserByEmail(user.Email);

            if (userExists.Count() == 0)
            {
                user.Status = "0";
                user.Password = "2435345345345";
                user.CreatedAt = DateTime.Now;

                Guid myuuid = Guid.NewGuid();
                Guid myuuid2 = Guid.NewGuid();

                user.Token = myuuid2.ToString();

                await _unitOfWork.UserRepository.Add(user);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new BusinessException("User exist");
            }

        }

        public async Task<bool> DeleteUser(long id)
        {
            await _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
