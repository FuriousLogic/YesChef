using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public static class QuantityTypeHandler
    {
        public static QuantityType CreateQuantityType(string name)
        {
            var db = new YesChefContext();
            var qt = db.QuantityTypes.Add(new QuantityType { Name = name });
            db.SaveChanges();

            return qt;
        }

        public static List<QuantityType> GetAllQuantityTypes()
        {
            var db = new YesChefContext();
            return db.QuantityTypes.ToList();
        }
    }
}
