using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public class SousChefHandler
    {
        public static SousChef CreateSousChef(string Name, string Email, string PlainTextPassword)
        {
            var db = new YesChefContext();
            var sousChef = db.SousChefs.Add(new SousChef
            {
                Name = Name,
                EmailAddress = Email,
                PasswordHash = "spaceSaver"
            });
            db.SaveChanges();

            sousChef.PasswordHash = EncryptionHandler.CreateHash(PlainTextPassword, sousChef.Id);
            db.SaveChanges();

            return sousChef;
        }
    }
}
