using System;
using System.Runtime.InteropServices;

namespace Red_Sky
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (RedSkyGame game = new RedSkyGame())
            {
                game.Run();
            }
        }

        public static bool IsInstanceOfGenericType(Type genericType, object instance)
        {
            Type type = instance.GetType();
            while (type != null)
            {
                if (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }
    }
#endif

#if DEBUG
    public class User32
    {
        [DllImport("user32.dll")]
        public static extern void SetWindowPos(uint Hwnd, uint Level, int X, int Y, int W, int H, uint Flags);
    }
#endif

}

