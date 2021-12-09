using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using DAL_Zupree;
using IBL_Zupree;
using Model_Zupree;

namespace BLL_Zupree
{
    public class AccountRepository : IAccountRepository
    {
        private ZupreeDBEntities dbObj;
        public AccountRepository()
        {
            dbObj = new ZupreeDBEntities();
        }

        public void CreateInitialUserAndRole()
        {
            int count = dbObj.tblUsers.Count();
            if (count == 0)
            {
                tblUser user = new tblUser();
                user.FirstName = "SOHAN";
                user.LastName = "HASAN";
                user.UserName = "sohan";
                user.Phone = "01751050039";
                user.Email = "sohan.hasan9021@gmail.com";
#pragma warning disable CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
                string password = FormsAuthentication.HashPasswordForStoringInConfigFile("S1009021", "SHA1");
#pragma warning restore CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
                user.Password = password;
                user.ImageUrl = "~/Images/UserImages/sohan.jpg";
                user.ImageName = "sohan.jpg";
                dbObj.tblUsers.Add(user);
                Save();
                int userId = (from record in dbObj.tblUsers orderby record.UserId descending select record.UserId).First();
                tblRole role = new tblRole();
                role.RoleName = "Admin";
                dbObj.tblRoles.Add(role);
                Save();
                int roleId = (from record in dbObj.tblRoles orderby record.RoleId descending select record.RoleId).First();
                UserWiseRole ur = new UserWiseRole();
                ur.RoleId = roleId;
                ur.UserId = userId;
                dbObj.UserWiseRoles.Add(ur);
                Save();
            }
        }

        public string[] GetRolesForUser(string username)
        {
            var roles = (from user in dbObj.tblUsers
                         join ur in dbObj.UserWiseRoles on user.UserId equals ur.UserId
                         join role in dbObj.tblRoles on ur.RoleId equals role.RoleId
                         where user.UserName == username
                         select role.RoleName).ToArray();
            return roles;
        }


        public int UserVilidationCheck(UserModelView obj)
        {
            int count;
            if (obj.Password!= null && obj.UserName !=null)
            {
#pragma warning disable CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
                string password = FormsAuthentication.HashPasswordForStoringInConfigFile(obj.Password, "SHA1");
#pragma warning restore CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
                count = dbObj.tblUsers.Where(u => u.UserName == obj.UserName && u.Password == password).Count();
                return count;
            }
            return count = 0;

        }
        public bool isUserExists(UserModelView obj)
        {
            bool isExists = dbObj.tblUsers.Any(u => u.UserName == obj.UserName);
            return isExists;
        }
        public IEnumerable<UserModelView> GetAll()
        {
            IEnumerable<UserModelView> listOfUser = dbObj.tblUsers.Select(e => new UserModelView
            {
                UserId = e.UserId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                UserName = e.UserName,
                Phone = e.Phone,
                Email = e.Email,
                Password = e.Password,
                ImageName = e.ImageName,
                ImageUrl = e.ImageUrl
            }).ToList();
            return listOfUser;
        }

        public UserModelView GetById(int id)
        {
            tblUser obj1 = dbObj.tblUsers.Find(id);
            UserModelView obj = new UserModelView();
            obj.UserId = obj1.UserId;
            obj.FirstName = obj1.FirstName;
            obj.LastName = obj1.LastName;
            obj.UserName = obj1.UserName;
            obj.Phone = obj1.Phone;
            obj.Email = obj1.Email;
            obj.Password = obj1.Password;
            obj.ImageName = obj1.ImageName;
            obj.ImageUrl = obj1.ImageUrl;
            return obj;
        }

        public IEnumerable<UserModelView> GetByUserName(string userName)
        {
            IEnumerable<UserModelView> listOfUser = dbObj.tblUsers.Where(e=>e.UserName == userName).Select(e => new UserModelView
            {
                UserId = e.UserId,
            }).ToList();
            return listOfUser;
        }
        public void Insert(UserModelView e)
        {
#pragma warning disable CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
            string password = FormsAuthentication.HashPasswordForStoringInConfigFile(e.Password, "SHA1");
#pragma warning restore CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
            tblUser objUser = new tblUser()
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                UserName = e.UserName,
                Phone = e.Phone,
                Email = e.Email,
                Password = password,
                ImageName = e.ImageName,
                ImageUrl = e.ImageUrl
            };
            dbObj.tblUsers.Add(objUser);
            Save();
        }

        public void Update(UserModelView updateObj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            tblUser user = dbObj.tblUsers.Find(id);
            dbObj.tblUsers.Remove(user);
            Save();
        }
        public void Save()
        {
            dbObj.SaveChanges();
        }

    }
    public class RoleRepository : IRoleRepository
    {
        private ZupreeDBEntities dbObj;
        public RoleRepository()
        {
            dbObj = new ZupreeDBEntities();
        }
        public IEnumerable<RoleViewModel> GetAll()
        {
            IEnumerable<RoleViewModel> listOfRoles = dbObj.tblRoles.Select(e => new RoleViewModel
            {
                RoleId = e.RoleId,
                RoleName = e.RoleName,
            }).ToList();
            return listOfRoles;
        }

        public RoleViewModel GetById(int id)
        {
            tblRole obj1 = dbObj.tblRoles.Find(id);
            RoleViewModel obj = new RoleViewModel();
            obj.RoleId = obj1.RoleId;
            obj.RoleName = obj1.RoleName;
            return obj;
        }

        public void Insert(RoleViewModel e)
        {
            tblRole objRoles = new tblRole()
            {
                RoleId = e.RoleId,
                RoleName = e.RoleName,
            };
            dbObj.tblRoles.Add(objRoles);
            Save();
        }


        public void Update(RoleViewModel obj1)
        {
            tblRole obj = new tblRole();
            obj.RoleId = obj1.RoleId;
            obj.RoleName = obj1.RoleName;
            dbObj.Entry(obj).State = EntityState.Modified;
            Save();
        }
        public void Delete(int id)
        {
            tblRole role = dbObj.tblRoles.Find(id);
            dbObj.tblRoles.Remove(role);
            Save();
        }

        public void Save()
        {
            dbObj.SaveChanges();
        }
    }
    public class UserWiseRoleRepository : IUserWiseRoleRepository
    {
        private ZupreeDBEntities dbObj;
        public UserWiseRoleRepository()
        {
            dbObj = new ZupreeDBEntities();
        }

        public IEnumerable<UserWiseRoleViewModel> GetAll()
        {
            IEnumerable<UserWiseRoleViewModel> listOfUserWiseRole = dbObj.UserWiseRoles.Select(e => new UserWiseRoleViewModel
            {
                uwrId = e.uwrId,
                UserId = e.UserId,
                RoleId = e.RoleId,
                RoleName = e.tblRole.RoleName,
                FirstName=e.tblUser.FirstName,
                LastName=e.tblUser.LastName,
                Email=e.tblUser.Email,
                UserName = e.tblUser.UserName,
                ImageName=e.tblUser.ImageName,
                ImageUrl=e.tblUser.ImageUrl
            }).ToList();
            return listOfUserWiseRole;
        }

        public UserWiseRoleViewModel GetById(int id)
        {
            UserWiseRole obj1 = dbObj.UserWiseRoles.Find(id);
            UserWiseRoleViewModel obj = new UserWiseRoleViewModel();
            obj.uwrId = obj1.uwrId;
            obj.UserId = obj1.UserId;
            obj.RoleId = obj1.RoleId;
            return obj;
        }

        public void Insert(UserWiseRoleViewModel e)
        {
            UserWiseRole objRoles = new UserWiseRole()
            {
                uwrId = e.uwrId,
                UserId = e.UserId,
                RoleId = e.RoleId,
            };
            dbObj.UserWiseRoles.Add(objRoles);
            Save();
        }

        public void Update(UserWiseRoleViewModel obj1)
        {
            UserWiseRole obj = new UserWiseRole();
            obj.uwrId = obj1.uwrId;
            obj.UserId = obj1.UserId;
            obj.RoleId = obj1.RoleId;
            dbObj.Entry(obj).State = EntityState.Modified;
            Save();
        }
        public void Delete(int id)
        {
            UserWiseRole role = dbObj.UserWiseRoles.Find(id);
            dbObj.UserWiseRoles.Remove(role);
            Save();
        }
        public void Save()
        {
            dbObj.SaveChanges();
        }

        public object UserWiseRoles()
        {
            var userWiseRoles = dbObj.UserWiseRoles.Include(u => u.tblRole).Include(u => u.tblUser);
            return userWiseRoles.ToList();
        }
        //public IEnumerable<UserWiseRoleViewModel> UserWiseRoles()
        //{
        //    IEnumerable<UserModelView> obj = dbObj.UserWiseRoles.Include(u => u.tblRole).Include(u => u.tblUser);
        //    UserWiseRoleViewModel obj1 = obj.Select(e => new UserWiseRoleViewModel
        //    {
        //        uwrId = e.uwrId,
        //        RoleId = e.RoleId,
        //        UserId = e.UserId
        //    }).ToList();
        //}
    }
}
