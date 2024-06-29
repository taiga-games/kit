namespace TaigaGames.Kit
{
    public class UintUshortUtility
    {
    	/// <summary>
        /// Converts a ulong to two uint (major and minor).
        /// </summary>
        /// <param name="value">The uint value to convert.</param>
        /// <param name="major">The resulting major ushort.</param>
        /// <param name="minor">The resulting minor ushort.</param>
        public static void Unpack(ulong value, out uint major, out uint minor)
        {
            major = (uint)(value >> 32); 
            minor = (uint)(value & 0xFFFFFFFF);
        }

        /// <summary>
        /// Converts two uint (major and minor) back to a ulong.
        /// </summary>
        /// <param name="major">The major ushort.</param>
        /// <param name="minor">The minor ushort.</param>
        /// <returns>The resulting uint value.</returns>
        public static ulong Pack(uint major, uint minor)
        {
            return ((ulong)major << 32) | minor;
        }
    }
}