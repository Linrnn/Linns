namespace Linns
{
    using UnityEngine;

    /// <summary>
    /// 多维向量类：包含二维向量，三维向量，四维向量
    /// </summary>
    public static class Vector_2_3_4
    {
        /// <summary>
        /// 二维向量的钳制
        /// </summary>
        /// <param name="v2">被钳制二维向量</param>
        /// <param name="start">向量一</param>
        /// <param name="end">向量二</param>
        /// <returns></returns>
        public static Vector2 Clamp(Vector2 v2, Vector2 start, Vector2 end)
        {
            return Clamp(v2, start.x, end.x, start.y, end.y);
        }
        /// <summary>
        /// 二维向量的钳制
        /// </summary>
        /// <param name="v2">被钳制二维向量</param>
        /// <param name="minX">x的最小值</param>
        /// <param name="maxX">x的最小值</param>
        /// <param name="minY">y的最小值</param>
        /// <param name="maxY">y的最大值</param>
        /// <returns></returns>
        public static Vector2 Clamp(Vector2 v2, float minX = 0f, float maxX = 1f, float minY = 0f, float maxY = 1f)
        {
            v2.Set(Mathf.Clamp(v2.x, minX, maxX), Mathf.Clamp(v2.y, minY, maxY));
            return v2;
        }
        /// <summary>
        /// 三维向量的钳制
        /// </summary>
        /// <param name="v3">被钳制三维向量</param>
        /// <param name="start">向量一</param>
        /// <param name="end">向量二</param>
        /// <returns></returns>
        public static Vector3 Clamp(Vector3 v3, Vector3 start, Vector3 end)
        {
            return Clamp(v3, start.x, end.x, start.y, end.y, start.z, end.z);
        }
        /// <summary>
        /// 三维向量的钳制
        /// </summary>
        /// <param name="v3">被钳制三维向量</param>
        /// <param name="minX">x的最小值</param>
        /// <param name="maxX">x的最小值</param>
        /// <param name="minY">y的最小值</param>
        /// <param name="maxY">y的最大值</param>
        /// <param name="minZ">z的最小值</param>
        /// <param name="maxZ">z的最大值</param>
        /// <returns></returns>
        public static Vector3 Clamp(Vector3 v3, float minX = 0f, float maxX = 1f, float minY = 0f, float maxY = 1f, float minZ = 0f, float maxZ = 1f)
        {
            v3.Set(Mathf.Clamp(v3.x, minX, maxX), Mathf.Clamp(v3.y, minY, maxY), Mathf.Clamp(v3.z, minZ, maxZ));
            return v3;
        }

        /// <summary>
        /// 二维向量+常数
        /// </summary>
        /// <param name="v2">二维向量</param>
        /// <param name="f">常数</param>
        /// <returns></returns>
        public static Vector2 Add(Vector2 v2, float f = 1f)
        {
            v2.Set(v2.x + f, v2.y + f);
            return v2;
        }
        /// <summary>
        /// 三维向量+常数
        /// </summary>
        /// <param name="v3">三维向量</param>
        /// <param name="f">常数</param>
        /// <returns></returns>
        public static Vector3 Add(Vector3 v3, float f = 1f)
        {
            v3.Set(v3.x + f, v3.y + f, v3.z + f);
            return v3;
        }

        /// <summary>
        /// 二维向量/二维向量
        /// </summary>
        /// <param name="v2a">二维向量A</param>
        /// <param name="v2b">二维向量B</param>
        /// <returns></returns>
        public static Vector2 DividBy(Vector2 v2a, Vector2 v2b)
        {
            try { v2a.Set(v2a.x / v2b.x, v2a.y / v2a.y); }
            catch { Debug.LogError("被除二维向量含0，请重新输入被除数。"); }
            return v2a;
        }
        /// <summary>
        /// 三维向量/三维向量
        /// </summary>
        /// <param name="v3a">三维向量A</param>
        /// <param name="v3b">三维向量B</param>
        /// <returns></returns>
        public static Vector3 DividBy(Vector3 v3a, Vector3 v3b)
        {
            try { v3a.Set(v3a.x / v3b.x, v3a.y / v3a.y, v3a.z / v3b.z); }
            catch { Debug.LogError("被除三维向量含0，请重新输入被除数。"); }
            return v3a;
        }
    }
}