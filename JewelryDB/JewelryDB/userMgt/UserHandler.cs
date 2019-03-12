using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryDB.userMgt
{
   public class UserHandler
    {
        public List<User> GetUsers()
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Users select m).ToList();
            }
        }
        public User GetUserById(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Users
                        .Include("Role")
                        where (m.Id==id)
                        select m).FirstOrDefault();
            }
        }
        public User GetUserByLoginId(string id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Users
                        .Include("Role")
                        where (m.LoginId == id)
                        select m).FirstOrDefault();
            }
        }
        public User GetUserByLogIn(string LogId, string Password)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Users
                        .Include(mbox=>mbox.Role)
                        .Include(mbox=>mbox.City)
                        where (m.LoginId.Equals(LogId) && m.Password.Equals(Password))
                        select m).FirstOrDefault();
            }
        }
        public List<Comment> Getallcomments()
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Messages
                        .Include(x=>x.user)
                        select m).ToList();
            }
        }
        public Comment Getcommentbyid(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Messages
                        .Include(x => x.user)
                        where(m.Id==id)
                        select m).FirstOrDefault();
            }
        }
        public List<Comment> GetallcommentsbyUser(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Messages
                        .Include(x => x.user)
                        where(m.user.Id==id)
                        select m).ToList();
            }
        }
        public void AddUser(User user)
        {
            Context con = new Context();
            using (con)
            {
                con.Entry(user.City).State = EntityState.Unchanged;
                con.Entry(user.Role).State = EntityState.Unchanged;
                con.Users.Add(user);
                con.SaveChanges();
            }
        }
        public void Addcomment(Comment com)
        {
            Context con = new Context();
            using (con)
            {

                //con.Entry(com.user).State = EntityState.Unchanged;
                con.Messages.Add(com);
                con.SaveChanges();
            }
        }
        public void Addcommentwithuser(Comment com)
        {
            Context con = new Context();
            using (con)
            {

                con.Entry(com.user).State = EntityState.Unchanged;
                con.Messages.Add(com);
                con.SaveChanges();
            }
        }
    }
}
