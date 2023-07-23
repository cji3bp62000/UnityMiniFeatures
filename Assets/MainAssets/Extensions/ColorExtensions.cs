using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
    public static class ColorExtensions
    {
        private const MethodImplOptions DefaultMethodImpl = MethodImplOptions.AggressiveInlining;

        [MethodImpl(DefaultMethodImpl)]
        public static Color WithA(this Color source, float newAlpha)
        {
            source.a = newAlpha;
            return source;
        }
    }
}
