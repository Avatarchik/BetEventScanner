using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace BetEventScanner.DataAccess.MongoEntities
{
    public class Ent1
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }
    }
}
