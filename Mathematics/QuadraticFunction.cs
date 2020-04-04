namespace Linns.Mathematics
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public struct QuadraticFunction : IDisposable
    {
        public float a;
        public Vector3 oPos;
        public Vector2 range;
        public int distributionFactor;

        private List<Vector4> m_midPositions;
        private List<Vector4> m_positions;
        private List<float> m_xList;

        public static Vector2 maxRange
        {
            get { return new Vector2(float.MinValue, float.MaxValue); }
        }

        public float b
        {
            get { return -2f * a * oPos.x; }
            set
            {
                ClearPositions();
                oPos.x = -0.5f * value / a;
            }
        }
        public float c
        {
            get { return a * oPos.x * oPos.x + oPos.y; }
            set
            {
                ClearPositions();
                float b = this.b;
                oPos.y = value - 0.25f * b * b / a;
            }
        }

        public IEnumerable<Vector4> midPositions
        {
            get
            {
                if (m_midPositions != null) { m_midPositions.Clear(); }
                else { m_midPositions = new List<Vector4>(); }

                float start = range.x, delta = (range.y - range.x) / distributionFactor;
                for (int i = distributionFactor; i != 0; --i)
                {
                    Vector3 pos = GetPosMid(start, start += delta);
                    m_midPositions.Add(new Vector4(pos.x, pos.y, pos.z, GetPosK(pos.x)));
                }
                return m_midPositions;
            }
        }
        public IEnumerable<Vector4> positions
        {
            get
            {
                if (m_positions != null) { m_positions.Clear(); }
                else { m_positions = new List<Vector4>(); }

                float start = range.x, delta = (range.y - range.x) / distributionFactor;
                for (int i = distributionFactor; i != -1; --i, start += delta)
                {
                    m_positions.Add(new Vector4(start, GetPosY(start), oPos.z, GetPosK(start)));
                }
                return m_positions;
            }
        }
        public IEnumerable<float> xList
        {
            get
            {
                if (m_xList != null) { m_xList.Clear(); }
                else { m_xList = new List<float>(); }

                float start = range.x, delta = (range.y - range.x) / distributionFactor;
                for (int i = distributionFactor; i != -1; --i, start += delta) { m_xList.Add(start); }
                return m_xList;
            }
        }
        public Vector3 ax2_bx_c
        {
            get
            {
                float f = a * oPos.x;
                return new Vector3(a, -2f * f, f * oPos.x + oPos.y);
            }
        }

        private static string GetStr(float num, bool hasX = false)
        {
            return num == 0f ? null : $"{(num > 0f ? '+' : '-')} {Math.Abs(num)}{(hasX ? 'x' : char.MaxValue)}";
        }

        public void Set(float b, float c)
        {
            ClearPositions();
            oPos.x = -0.5f * b / a;
            oPos.y = c + 0.5f * oPos.x * b;
        }
        public void Set(float a, float b, float c, float z = 0f)
        {
            ClearPositions();
            this.a = a;
            float _b_2a = -0.5f * b / a;
            oPos.Set(_b_2a, c + 0.5f * _b_2a * b, z);
        }
        public void Set(float a, Vector3 oPos)
        {
            ClearPositions();
            this.a = a;
            this.oPos = oPos;
        }
        public void Instantiate(float a, Vector3 oPos, Vector2 range, int distributionFactor)
        {
            this.a = a;
            this.oPos.Set(oPos.x, oPos.y, oPos.z);
            if (range.x < range.y) { this.range.Set(range.x, range.y); }
            else { this.range.Set(range.y, range.x); }
            this.distributionFactor = distributionFactor;
        }
        public void Dispose()
        {
            ClearPositions();
            m_midPositions = null;
            m_positions = null;
            m_xList = null;
        }
        public override string ToString()
        {
            object a;
            switch (this.a)
            {
                case 1f: a = null; break;
                case -1f: a = '-'; break;
                default: a = this.a; break;
            }
            Vector3 ax2_bx_c = this.ax2_bx_c;

            return string.Format("{0}\t{2}\n{1}\t{2}",
                string.Format("y = {0}{1}",
                  oPos.x == 0f ? string.Format("{0}x²", a) :
                    string.Format("{0}(x {1} {2})²", a, oPos.x > 0f ? '-' : '+', Math.Abs(oPos.x)),
                  GetStr(oPos.y)),
                string.Format("y = {0}x²{1}{2}", a, GetStr(ax2_bx_c.y, true), GetStr(ax2_bx_c.z)),
                string.Format("({0} ≤ x ≤ {1})", range.x, range.y));
        }

        public float GetPosK(float x)
        {
            return 2f * a * (x - oPos.x);
        }
        public float GetPosK(int index)
        {
            return GetPosK(GetPosX(index));
        }
        public float GetPosX(int index)
        {
            return range.x + (range.y - range.x) / distributionFactor * index;
        }
        public float GetPosY(float x)
        {
            x -= oPos.x;
            return a * x * x + oPos.y;
        }
        public float GetPosY(int index)
        {
            return GetPosY(GetPosX(index));
        }
        public Vector3 GetPos(float x)
        {
            return new Vector3(x, GetPosY(x), oPos.z);
        }
        public Vector3 GetPos(int index)
        {
            return new Vector3(GetPosX(index), GetPosY(index), oPos.z);
        }
        public Vector3 GetPosMid(int index)
        {
            float average = (range.y - range.x) / distributionFactor;
            float left = range.x + average * index;
            return GetPosMid(left, left + average);
        }
        public Vector3 GetPosMid(float left, float right)
        {
            Vector2 leftPos = GetPos(left), rightPos = GetPos(right);
            float mid;
            do
            {
                mid = (left + right) / 2f;
                Vector2 midPos = GetPos(mid);
                if ((leftPos - midPos).SqrMagnitude() < (rightPos - midPos).SqrMagnitude()) { left = mid; }
                else { right = mid; }
            } while (right > left + 0.01f);
            return GetPos(mid);
        }
        public float GetAverageLength(List<float> xList)
        {
            float len = 1f;
            for (float lrLen = range.y - range.x, left = 0f, right = lrLen, end = range.x; Data.IsAbsGreaterMin(left - right) && Data.IsAbsGreaterMin(range.y - end);)
            {
                xList.Clear();
                len = 0.5f * (left + right);
                float x = range.x, len2 = len * len;
                for (int i = distributionFactor; i != 0; --i)
                {
                    for (float lx = x, rx = x + lrLen; Data.IsAbsGreaterMin(lx - rx);)
                    {
                        float m = 0.5f * (lx + rx);
                        float sqrMagnitude = new Vector2(m - x, GetPosY(m) - GetPosY(x)).sqrMagnitude;

                        if (sqrMagnitude > len2) { rx = m; }
                        else if (sqrMagnitude < len2) { lx = m; }
                        else { x = m; break; }
                        x = m;
                    }
                    xList.Add(x);
                }
                end = x;
                xList.Insert(0, range.x);

                if (end > range.y) { right = len; }
                else if (end < range.y) { left = len; }
                else { break; }
            }

            return len;
        }

        private void ClearPositions()
        {
            if (m_midPositions != null) { m_midPositions.Clear(); }
            if (m_positions != null) { m_positions.Clear(); }
            if (m_xList != null) { m_xList.Clear(); }
        }
    }
}