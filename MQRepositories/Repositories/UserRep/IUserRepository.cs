using MQEntityModal.Modal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQDataService.Repositories.UserRep
{
    public interface IUserRepository
    {
        User Get(Guid userId);

        Task<User> GetAsync(Guid userId);

        void Add(User user);
    }
}