using System;
using System.ComponentModel.DataAnnotations;

namespace HKW.Practices.NHBase.Domain
{
    public abstract class Entity
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        [Key]
        [Display(AutoGenerateField = false)]
        public virtual Guid ID { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [Display(AutoGenerateField = false)]
        public virtual int Version { get; set; }

        /// <summary>
        /// 删除标记
        /// </summary>
        [Display(AutoGenerateField = false)]
        public virtual bool DelFlag { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Display(AutoGenerateField = false)]
        public virtual DateTime? OptDate { get; set; }

        /// <summary>
        /// 操作IPVARCHAR(100)
        /// </summary>
        [Display(AutoGenerateField = false)]
        public virtual string OptTerm { get; set; }

        /// <summary>
        /// 操作人VARCHAR(40)
        /// </summary>
        [Display(AutoGenerateField = false)]
        public virtual string OptUser { get; set; }

        /// <summary>
        /// 判断实体是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Entity);
        }

        private static bool IsTransient(Entity obj)
        {
            return obj != null && Equals(obj.ID, default(Guid));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(Entity other)
        {
            if (other == null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (!IsTransient(this) &&
            !IsTransient(other) &&
            Equals(ID, other.ID))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                otherType.IsAssignableFrom(thisType);
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (Equals(ID, default(Guid)))
                // ReSharper disable BaseObjectGetHashCodeCallInGetHashCode
                return base.GetHashCode();
            // ReSharper restore BaseObjectGetHashCodeCallInGetHashCode
            return ID.GetHashCode();
        }
    }


}
