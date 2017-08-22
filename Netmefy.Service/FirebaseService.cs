using Netmefy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netmefy.Service
{
    public class FirebaseService
    {
        private NETMEFYEntities db = new NETMEFYEntities();


        public void registerToken(token token)
        {
            db.tokens.Add(token);

            db.SaveChanges();
        }


    }
}