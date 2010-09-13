using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using TinyBlog.Objects;
using TinyBlog.Interface;

namespace TinyBlog.Data
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IQueryExecutor queryExecutor) : base(queryExecutor){}

        public User GetUser(string userName)
        {
            Query<User> query = session =>
                                    {
                                        var result = from u in session.Linq<User>() where u.UserId == userName select u;
                                        return result.FirstOrDefault();
                                    };

            return QueryExecutor.ExecuteQuery(query);
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
           Query<User> query = session =>
                                    {
                                        var result = from u in session.Linq<User>() 
                                                     where u.UserId == userName && u.Password == oldPassword 
                                                     select u;

                                        return result.FirstOrDefault();
                                    };

            var user = QueryExecutor.ExecuteQuery(query); 

            if (user == null) return false;

            user.Password = newPassword;

            CUDQuery updateQuery = session => session.SaveOrUpdate(user);

            return QueryExecutor.UpdateDelete(updateQuery);
        }


        public bool CreateOrUpdateUser(User user)
        {
            CUDQuery query = session => session.SaveOrUpdate(user); 

            return QueryExecutor.UpdateDelete(query);
        }

        public UserRole? GetRole(string username)
        {
            Query<UserRole?> query = session =>
                                        {
                                            var result = from u in session.Linq<User>()
                                                          where u.UserId == username
                                                          select u.Role;


                                            return result.FirstOrDefault();
                                        };

            return QueryExecutor.ExecuteQuery(query);
        }

        public bool AtLeastOneUserExists()
        {
            Query<bool> query = session =>
                                    {
                                        var result = (from u in session.Linq<User>()
                                                      select u).FirstOrDefault();

                                        return result != null;
                                    };

            return QueryExecutor.ExecuteQuery(query);
        }
    }
}
