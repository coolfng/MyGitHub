using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HKW.Practices.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.MappingFluently.SYS
{
    public class EnumDetailMapping : EntityMapping<EnumDetail>
    {
        public EnumDetailMapping()
        {
            Property(entity => entity.Code);
            Property(entity => entity.Name);
            Property(entity => entity.Seq);
            ManyToOne(e => e.EnumHeader, map => map.Column("EnumHeader_ID"));
        }
    }
}
