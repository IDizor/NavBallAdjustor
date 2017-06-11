using UnityEngine;

namespace NavBallAdjustor
{
    /// <summary>
    /// Contains different classes extension methods.
    /// </summary>
    public static class ModExtensions
    {
        /// <summary>
        /// Adds the specified value to position Z axis.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="value">The increment value.</param>
        /// <returns>New vector with updated value.</returns>
        public static Vector3 AddZ(this Vector3 vector, float value)
        {
            vector.Set(vector.x, vector.y, vector.z + value);

            return vector;
        }

        /// <summary>
        /// Sets the new Z.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="value">The new Z value.</param>
        /// <returns></returns>
        public static Vector3 SetZ(this Vector3 vector, float value)
        {
            vector.Set(vector.x, vector.y, value);

            return vector;
        }
    }
}
