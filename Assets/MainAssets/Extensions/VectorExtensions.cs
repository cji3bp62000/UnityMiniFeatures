using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityEngine
{
    public static class VectorExtensions
    {
        private const MethodImplOptions DefaultMethodImpl = MethodImplOptions.AggressiveInlining;

        #region WithX/Y/Z

        [MethodImpl(DefaultMethodImpl)]
        public static Vector3 WithX(this in Vector3 source, float newX)
        {
            var newVector = source;
            newVector.x = newX;
            return newVector;
        }

        [MethodImpl(DefaultMethodImpl)]
        public static Vector3 WithY(this in Vector3 source, float newY)
        {
            var newVector = source;
            newVector.y = newY;
            return newVector;
        }

        [MethodImpl(DefaultMethodImpl)]
        public static Vector3 WithZ(this in Vector3 source, float newZ)
        {
            var newVector = source;
            newVector.z = newZ;
            return newVector;
        }

        [MethodImpl(DefaultMethodImpl)]
        public static Vector3 WithXZ(this in Vector3 source, float newX, float newZ)
        {
            var newVector = source;
            newVector.x = newX;
            newVector.z = newZ;
            return newVector;
        }

        #endregion

        #region IsMagGreater/LessThan

        [MethodImpl(DefaultMethodImpl)]
        public static bool IsMagGreaterThan(this in Vector3 source, in Vector3 other)
        {
            return source.sqrMagnitude > other.sqrMagnitude;
        }

        [MethodImpl(DefaultMethodImpl)]
        public static bool IsMagGreaterThan(this in Vector3 source, in float length)
        {
            return source.sqrMagnitude > length * length;
        }

        [MethodImpl(DefaultMethodImpl)]
        public static bool IsMagGreaterEqualThan(this in Vector3 source, in Vector3 other)
        {
            return source.sqrMagnitude >= other.sqrMagnitude;
        }

        [MethodImpl(DefaultMethodImpl)]
        public static bool IsMagGreaterEqualThan(this in Vector3 source, in float length)
        {
            return source.sqrMagnitude >= length * length;
        }

        [MethodImpl(DefaultMethodImpl)]
        public static bool IsMagLessThan(this in Vector3 source, in Vector3 other)
        {
            return source.sqrMagnitude < other.sqrMagnitude;
        }

        [MethodImpl(DefaultMethodImpl)]
        public static bool IsMagLessThan(this in Vector3 source, in float length)
        {
            return source.sqrMagnitude < length * length;
        }

        [MethodImpl(DefaultMethodImpl)]
        public static bool IsMagLessEqualThan(this in Vector3 source, in Vector3 other)
        {
            return source.sqrMagnitude <= other.sqrMagnitude;
        }

        [MethodImpl(DefaultMethodImpl)]
        public static bool IsMagLessEqualThan(this in Vector3 source, in float length)
        {
            return source.sqrMagnitude <= length * length;
        }

        #endregion
    }
}
