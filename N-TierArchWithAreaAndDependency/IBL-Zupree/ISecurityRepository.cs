using Model_Zupree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL_Zupree
{
    public interface IAccountRepository
    {
        string[] GetRolesForUser(string username);
        int UserVilidationCheck(UserModelView obj);
        bool isUserExists(UserModelView obj);
        IEnumerable<UserModelView> GetAll();
        UserModelView GetById(int id);
        IEnumerable<UserModelView> GetByUserName(string userName);
        void Insert(UserModelView objModel);
        void Update(UserModelView updateObj);
        void Delete(int id);
        void Save();

    }
    public interface IRoleRepository
    {
        IEnumerable<RoleViewModel> GetAll();
        RoleViewModel GetById(int id);
        void Insert(RoleViewModel objModel);
        void Update(RoleViewModel updateObj);
        void Delete(int id);
        void Save();

    }
    public interface IUserWiseRoleRepository
    {
        IEnumerable<UserWiseRoleViewModel> GetAll();
        UserWiseRoleViewModel GetById(int id);
        void Insert(UserWiseRoleViewModel objModel);
        void Update(UserWiseRoleViewModel updateObj);
        void Delete(int id);
        void Save();
        object UserWiseRoles();

    }
}
