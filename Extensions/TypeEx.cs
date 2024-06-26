using System.Linq;

namespace TaigaGames.Kit
{
    public static class TypeEx
    {
        public static bool TryGetAttribute<T>(this System.Type type, out T attribute) where T : System.Attribute
        {
            attribute = (T)type.GetCustomAttributes(typeof(T), true).FirstOrDefault();
            return attribute != null;
        }
        
        public static bool Is<T>(this System.Type type)
        {
            return typeof(T).IsAssignableFrom(type);
        }
    }
}