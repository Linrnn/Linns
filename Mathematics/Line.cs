namespace Linns.Mathematics
{
    using UnityEngine;
    using Serializable = System.SerializableAttribute;

    [Serializable]
    public struct Line2D
    {
        public Vector2 start;
        public Vector2 end;

        public static bool IsIntersectionPoint(Line2D ab, Line2D cd)
        {
            return IsIntersectionPoint(in ab.start, in ab.end, in cd.start, in cd.end);
        }
        public static bool IsIntersectionPoint(in Vector2 a, in Vector2 b, in Vector2 c, in Vector2 d)
        {
            if (Mathf.Min(a.x, b.x) > Mathf.Max(c.x, d.x)) { return false; }
            if (Mathf.Min(a.y, b.y) > Mathf.Max(c.y, d.y)) { return false; }
            if (Mathf.Min(c.x, d.x) > Mathf.Max(a.x, b.x)) { return false; }
            if (Mathf.Min(c.y, d.y) > Mathf.Max(a.y, b.y)) { return false; }

            Vector2 ac = a - c, p;
            p = a - b; if (Vector3.Dot(Vector3.Cross(a - d, p), Vector3.Cross(ac, p)) > 0f) { return false; }
            p = c - d; if (Vector3.Dot(Vector3.Cross(p, ac), Vector3.Cross(p, b - c)) > 0f) { return false; }

            return true;
        }
        public static Vector2 GetIntersectionPoint(Line2D ab, Line2D cd)
        {
            return GetIntersectionPoint(in ab.start, in ab.end, in cd.start, in cd.end);
        }
        public static Vector2 GetIntersectionPoint(in Vector2 a, in Vector2 b, in Vector2 c, in Vector2 d)
        {
            float ab_x = b.x - a.x, ab_y = b.y - a.y, cd_x = d.x - c.x, cd_y = d.y - c.y;
            float t = (ab_x * (c.y - a.y) + ab_y * (a.x - c.x)) / (cd_x * ab_y - ab_x * cd_y);
            return new Vector2(c.x + t * cd_x, c.y + t * cd_y);
        }

        public Line2D(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }
        public Line2D(Vector3 start, Vector3 end)
        {
            this.start = new Vector2(start.x, start.z);
            this.end = new Vector2(end.x, end.z);
        }
        public Line2D(float p1_x, float p1_y, float p2_x, float p2_y)
        {
            start = new Vector2(p1_x, p1_y);
            end = new Vector2(p2_x, p2_y);
        }
        public void Draw(Color color)
        {
            Debug.DrawLine(new Vector3(start.x, 0f, start.y), new Vector3(end.x, 0f, end.y), color);
        }
        public override string ToString()
        {
            return $"Start:{start}   End:{end}";
        }
    }

    [Serializable]
    public struct Line3D
    {
        public Vector3 start;
        public Vector3 end;

        public static bool IsIntersectionPoint(Line3D ab, Plane plane)
        {
            return false;
        }
        public static bool IsIntersectionPoint(in Vector2 a, in Vector2 b, in Vector2 c, in Plane plane)
        {
            return false;
        }
        public static Vector3 GetIntersectionPoint(Line3D ab, Plane plane)
        {
            return new Vector3();
        }
        public static Vector3 GetIntersectionPoint(in Vector2 a, in Vector2 b, in Vector2 c, in Plane plane)
        {
            return new Vector3();
        }

        public Line3D(Vector3 start, Vector3 end)
        {
            this.start = start;
            this.end = end;
        }
        public Line3D(float p1_x, float p1_y, float p1_z, float p2_x, float p2_y, float p2_z)
        {
            start = new Vector3(p1_x, p1_y, p1_z);
            end = new Vector3(p2_x, p2_y, p2_z);
        }
        public void Draw(Color color)
        {
            Debug.DrawLine(start, end, color);
        }
        public override string ToString()
        {
            return $"Start:{start}   End:{end}";
        }
    }
}