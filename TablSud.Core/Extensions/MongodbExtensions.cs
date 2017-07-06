﻿using System;
using System.Linq;
using MongoDB.Bson;

namespace TablSud.Core.Extensions
{
    public static class MongodbExtensions
    {
        public static Guid AsGuid(this ObjectId oid)
        {
            var bytes = oid.ToByteArray().Concat(new byte[] { 5, 5, 5, 5 }).ToArray();
            Guid gid = new Guid(bytes);
            return gid;
        }

        /// <summary>
        /// Only Use to convert a Guid that was once an ObjectId
        /// </summary>
        public static ObjectId AsObjectId(this Guid gid)
        {
            var bytes = gid.ToByteArray().Take(12).ToArray();
            var oid = new ObjectId(bytes);
            return oid;
        }
    }
}
