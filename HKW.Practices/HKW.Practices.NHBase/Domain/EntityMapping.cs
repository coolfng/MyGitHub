using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace HKW.Practices.NHBase.Domain
{
    public class EntityMapping<TEntity> : ClassMapping<TEntity> where TEntity : Entity
    {
        public EntityMapping()
        {
            Table(TableName);
            Lazy(false);
            DynamicInsert(true);
            DynamicUpdate(true);
            Id(entity => entity.ID, map => map.Generator(Generators.GuidComb));
            Version(x => x.Version, map => map.UnsavedValue(0));
            Property(entity => entity.DelFlag); 
            Property(entity => entity.OptDate);
            Property(entity => entity.OptTerm, map => map.Length(100));
            Property(entity => entity.OptUser, map => map.Length(40));
        }

        private string TableName
        {
            get
            {
                string r = typeof(TEntity).Name;
                var ns = typeof(TEntity).Namespace;
                if (ns != null)
                {
                    if (!ns.Equals("Entities"))
                    {
                        var n = ns.Split('.');
                        if (n.Length > 0)
                        {
                            r = string.Format("{0}_{1}", n[n.Length - 1], typeof(TEntity).Name);
                        }
                    }

                }
                return r;
            }
        }
    }
}
