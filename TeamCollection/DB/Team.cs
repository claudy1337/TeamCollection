using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamCollection.DB
{
    public class Team
    {
        public Team(string name)
        {
            Name = name;
        }
        public ObjectId _id { get; set; }
        public string Name { get; set; }

    }
}
