using System;

namespace HKW.ATE.Domain.Enum
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class Desc : Attribute
    {
        public Desc(string info) { value = info; }

        public string value { get; private set; }
    }

    public static class EnumExtension
    {
        public static string value(this System.Enum em)
        {
            System.Reflection.FieldInfo fi = em.GetType().GetField(em.ToString());
            if (fi.IsDefined(typeof(Desc), true))
            {
                Desc desc = (Desc)fi.GetCustomAttributes(typeof(Desc), true)[0];
                return desc.value;
            }
            return null;
        }
        public static T key<T>(this System.Enum em)
        {
            return (T)(object)em;
        }
    }
}
