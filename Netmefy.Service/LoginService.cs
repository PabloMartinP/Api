using Netmefy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Service
{
    public class LoginService
    {
        private NETMEFYEntities db = new NETMEFYEntities();
        public bool login(string username, string password)
        {

            var j = findByUsernameAndPassword(username, password);

            return j != null;
        }

        public VW_Usuarios_App findByUsernameAndPassword(string username, string password)
        {
            var j = db.VW_Usuarios_App.Where(x => x.ID.ToLower().Equals(username.ToLower()) && x.PSW.ToLower().Equals(password.ToLower())).FirstOrDefault();
            return j;
        }


        public VW_Usuarios_App findByUsername(string username)
        {
            var j = db.VW_Usuarios_App.Where(x => x.ID.ToLower().Equals(username.ToLower()) ).FirstOrDefault();
            return j;
        }
    }
}