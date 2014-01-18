using System.Collections.Generic;
using System.Data.Linq;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.Entities
{
    public static class EntityCollectionHelper
    {
        public static EntitySet<T> ToEntitySet<T>(this IEnumerable<T> source) where T : Entity
        {
            var set = new EntitySet<T>();
            set.AddRange(source);
            return set;
        }
    }
}
