namespace Linns.Mathematics
{
    using System.Collections.Generic;
    using Math = System.Math;
    using Complex = System.Numerics.Complex;

    /// <summary>
    /// 解方程类
    /// </summary>
    public static class Equation
    {
        private static readonly Complex s_w = new Complex(-0.5, 0.5 * Math.Sqrt(3.0));
        private static readonly List<Complex> s_list = new List<Complex>();

        /// <summary>
        /// 一元一次方程（一般式）
        /// </summary>
        public static void LinearEquationWithOneUnknown(List<float> solutionSet, float a, float b, bool clear = false)
        {
            if (clear) { solutionSet.Clear(); }
            if (a != 0f) { solutionSet.Add(-b / a); }
        }
        /// <summary>
        /// 一元一次方程（一般式）
        /// </summary>
        public static void LinearEquationWithOneUnknown(List<Complex> solutionSet, Complex a, Complex b, bool clear = false)
        {
            if (clear) { solutionSet.Clear(); }
            if (a != 0.0) { solutionSet.Add(-b / a); }
        }

        /// <summary>
        /// 一元二次方程（一般式）
        /// </summary>
        public static void QuadraticEquationOfOneUnknown(List<float> solutionSet, float a, float b, float c, bool clear = false)
        {
            if (clear) { solutionSet.Clear(); }
            if (a == 0f) { LinearEquationWithOneUnknown(solutionSet, b, c, clear); }
            else
            {
                float delta = b * b - 4f * a * c;
                float _2a = -0.5f / a;
                if (delta > 0f)
                {
                    float sqrt2Delta = (float)Math.Sqrt(delta);
                    solutionSet.Add((b + sqrt2Delta) * _2a);
                    solutionSet.Add((b - sqrt2Delta) * _2a);
                }
                else if (delta == 0f) { solutionSet.Add(b * _2a); }
            }
        }
        /// <summary>
        /// 一元二次方程（一般式）
        /// </summary>
        public static void QuadraticEquationOfOneUnknown(List<Complex> solutionSet, double a, double b, double c, bool clear = false)
        {
            if (clear) { solutionSet.Clear(); }
            if (a == 0.0) { if (b != 0.0) { solutionSet.Add(-c / b); } }
            else
            {
                double delta = b * b - 4f * a * c;
                double _2a = -0.5 / a;
                if (delta != 0.0)
                {
                    Complex sqrt2Delta = Complex.Sqrt(delta);
                    solutionSet.Add((b + sqrt2Delta) * _2a);
                    solutionSet.Add((b - sqrt2Delta) * _2a);
                }
                else if (delta == 0.0) { solutionSet.Add(b * _2a); }
            }
        }
        /// <summary>
        /// 一元二次方程（一般式）
        /// </summary>
        public static void QuadraticEquationOfOneUnknown(List<Complex> solutionSet, Complex a, Complex b, Complex c, bool clear = false)
        {
            if (clear) { solutionSet.Clear(); }
            if (a == 0.0) { if (b != 0.0) { solutionSet.Add(-c / b); } }
            else
            {
                Complex delta = b * b - 4f * a * c;
                Complex _2a = -0.5 / a;
                if (delta != 0.0)
                {
                    Complex sqrt2Delta = Complex.Sqrt(delta);
                    solutionSet.Add((b + sqrt2Delta) * _2a);
                    solutionSet.Add((b - sqrt2Delta) * _2a);
                }
                else if (delta == 0.0) { solutionSet.Add(b * _2a); }
            }
        }

        /// <summary>
        /// 一元三次方程（一般式）
        /// </summary>
        public static void CubicEquationInOneUnknown(List<float> solutionSet, float a, float b, float c, float d, bool clear = false)
        {
            if (clear) { solutionSet.Clear(); }
            s_list.Clear();
            CubicEquationInOneUnknown(s_list, a, b, c, d, clear);
            foreach (Complex complex in s_list)
            {
                if (Data.IsAbsGreaterMin(complex.Imaginary)) { continue; }
                float solution = (float)complex.Real;
                if (!solutionSet.Contains(solution)) { solutionSet.Add(solution); }
            }
            s_list.Clear();
        }
        /// <summary>
        /// 一元三次方程（一般式）
        /// </summary>
        public static void CubicEquationInOneUnknown(List<Complex> solutionSet, double a, double b, double c, double d, bool clear = false)
        {
            if (clear) { solutionSet.Clear(); }
            if (a == 0.0) { QuadraticEquationOfOneUnknown(solutionSet, b, c, d, clear); }
            else
            {
                double p = (3.0 * a * c - b * b) / (3.0 * a * a);
                double q = (27.0 * a * a * d - 9.0 * a * b * c + 2.0 * b * b * b) / (27.0 * a * a * a);
                double delta = p * p * p / 27.0 + 0.25 * q * q;

                double _1_3 = 1.0 / 3.0;
                double _b_3a = -_1_3 * b / a;
                double _q_2 = -0.5 * q;
                Complex sqrt2Delta = Complex.Sqrt(delta);
                Complex w2 = s_w * s_w;
                Complex sqrt1_3_q_2__sqrt2Delta = Complex.Pow(_q_2 + sqrt2Delta, _1_3);
                Complex sqrt1_3_q_2_sqrt2Delta = Complex.Pow(_q_2 - sqrt2Delta, _1_3);

                Complex solution1 = _b_3a + sqrt1_3_q_2__sqrt2Delta + sqrt1_3_q_2_sqrt2Delta;
                Complex solution2 = _b_3a + s_w * sqrt1_3_q_2__sqrt2Delta + w2 * sqrt1_3_q_2_sqrt2Delta;
                Complex solution3 = _b_3a + w2 * sqrt1_3_q_2__sqrt2Delta + s_w * sqrt1_3_q_2_sqrt2Delta;

                solutionSet.Add(solution1);
                if (!solutionSet.Contains(solution2)) { solutionSet.Add(solution2); }
                if (!solutionSet.Contains(solution3)) { solutionSet.Add(solution3); }
            }
        }
        /// <summary>
        /// 一元三次方程（一般式）
        /// </summary>
        public static void CubicEquationInOneUnknown(List<Complex> solutionSet, Complex a, Complex b, Complex c, Complex d, bool clear = false)
        {
            if (clear) { solutionSet.Clear(); }
            if (a == 0.0) { QuadraticEquationOfOneUnknown(solutionSet, b, c, d, clear); }
            else
            {
                Complex p = (3.0 * a * c - b * b) / (3.0 * a * a);
                Complex q = (27.0 * a * a * d - 9.0 * a * b * c + 2.0 * b * b * b) / (27.0 * a * a * a);
                Complex delta = p * p * p / 27.0 + 0.25 * q * q;

                Complex _1_3 = 1.0 / 3.0;
                Complex _b_3a = -_1_3 * b / a;
                Complex _q_2 = -0.5 * q;
                Complex sqrt2Delta = Complex.Sqrt(delta);
                Complex w2 = s_w * s_w;
                Complex sqrt1_3_q_2__sqrt2Delta = Complex.Pow(_q_2 + sqrt2Delta, _1_3);
                Complex sqrt1_3_q_2_sqrt2Delta = Complex.Pow(_q_2 - sqrt2Delta, _1_3);

                Complex solution1 = _b_3a + sqrt1_3_q_2__sqrt2Delta + sqrt1_3_q_2_sqrt2Delta;
                Complex solution2 = _b_3a + s_w * sqrt1_3_q_2__sqrt2Delta + w2 * sqrt1_3_q_2_sqrt2Delta;
                Complex solution3 = _b_3a + w2 * sqrt1_3_q_2__sqrt2Delta + s_w * sqrt1_3_q_2_sqrt2Delta;

                solutionSet.Add(solution1);
                if (!solutionSet.Contains(solution2)) { solutionSet.Add(solution2); }
                if (!solutionSet.Contains(solution3)) { solutionSet.Add(solution3); }
            }
        }

        /// <summary>
        /// 一元四次方程（一般式）
        /// </summary>
        public static void QuarticEquationOfOneUnknown(List<float> solutionSet, float a, float b, float c, float d, float e, bool clear = false)
        {
            if (clear) { solutionSet.Clear(); }
            s_list.Clear();
            QuarticEquationOfOneUnknown(s_list, a, b, c, d, e, clear);
            foreach (Complex complex in s_list)
            {
                if (Data.IsAbsGreaterMin(complex.Imaginary)) { continue; }
                float solution = (float)complex.Real;
                if (!solutionSet.Contains(solution)) { solutionSet.Add(solution); }
            }
            s_list.Clear();
        }
        /// <summary>
        /// 一元四次方程（一般式）
        /// </summary>
        public static void QuarticEquationOfOneUnknown(List<Complex> solutionSet, double a, double b, double c, double d, double e, bool clear = false)
        {
            if (clear) { solutionSet.Clear(); }
            if (a == 0.0) { CubicEquationInOneUnknown(solutionSet, b, c, d, e, clear); }
            else
            {
                double P = (c * c + 12.0 * a * e - 3.0 * b * d) / 9.0;
                double Q = (27.0 * a * d * d + 2.0 * c * c * c + 27.0 * b * b * e - 72.0 * a * c * e - 9.0 * b * c * d) / 54.0;
                Complex D = Complex.Sqrt(Q * Q - P * P * P);
                Complex Q__D = Q + D, Q_D = Q - D;
                Complex u = Complex.Pow(Q__D.Magnitude > Q_D.Magnitude ? Q__D : Q_D, 1.0 / 3.0);
                Complex v = u.Magnitude != 0.0 ? P / u : 0.0;
                double bb_8_3ac = b * b - 8.0 / 3.0 * a * c, _4a = 4.0 * a;
                Complex m1 = Complex.Sqrt(bb_8_3ac + _4a * (u + s_w * s_w * s_w * v));
                Complex m2 = Complex.Sqrt(bb_8_3ac + _4a * (s_w * u + s_w * s_w * v));
                Complex m3 = Complex.Sqrt(bb_8_3ac + _4a * (s_w * s_w * u + s_w * v));
                Complex m, _4awu__wv;
                {
                    double magnitude1 = m1.Magnitude;
                    double magnitude2 = m2.Magnitude;
                    double magnitude3 = m3.Magnitude;
                    if (magnitude1 >= magnitude2 && magnitude1 >= magnitude3)
                    {
                        m = m1;
                        _4awu__wv = _4a * (u + s_w * s_w * s_w * v);
                    }
                    else if (magnitude2 >= magnitude1 && magnitude2 >= magnitude3)
                    {
                        m = m2;
                        _4awu__wv = _4a * (s_w * u + s_w * s_w * v);
                    }
                    else if(magnitude3 >= magnitude1 && magnitude3 >= magnitude2)
                    {
                        m = m3;
                        _4awu__wv = _4a * (s_w * s_w * u + s_w * v);
                    }
                }
                Complex S, T;
                if (m.Magnitude != 0.0)
                {
                    S = 2.0 * b * b - 16.0 / 3.0 * a * c - _4awu__wv;
                    T = (8.0 * a * b * c - 16.0 * a * a * d - 2.0 * b * b * b) / m;
                }
                else
                {
                    S = bb_8_3ac;
                    T = 0.0;
                }
                double _4_a = 0.25 / a;

                Complex solution1 = (-b - m + Complex.Sqrt(S - T)) * _4_a;
                Complex solution2 = (-b - m - Complex.Sqrt(S - T)) * _4_a;
                Complex solution3 = (-b + m + Complex.Sqrt(S + T)) * _4_a;
                Complex solution4 = (-b + m - Complex.Sqrt(S + T)) * _4_a;
                solutionSet.Add(solution1);
                if (!solutionSet.Contains(solution2)) { solutionSet.Add(solution2); }
                if (!solutionSet.Contains(solution3)) { solutionSet.Add(solution3); }
                if (!solutionSet.Contains(solution4)) { solutionSet.Add(solution4); }
            }
        }
        /// <summary>
        /// 一元四次方程（一般式）
        /// </summary>
        public static void QuarticEquationOfOneUnknown(List<Complex> solutionSet, Complex a, Complex b, Complex c, Complex d, Complex e, bool clear = false)
        {
            if (clear) { solutionSet.Clear(); }
            if (a == 0.0) { CubicEquationInOneUnknown(solutionSet, b, c, d, e, clear); }
            else
            {
                Complex P = (c * c + 12.0 * a * e - 3.0 * b * d) / 9.0;
                Complex Q = (27.0 * a * d * d + 2.0 * c * c * c + 27.0 * b * b * e - 72.0 * a * c * e - 9.0 * b * c * d) / 54.0;
                Complex D = Complex.Sqrt(Q * Q - P * P * P);
                Complex Q__D = Q + D, Q_D = Q - D;
                Complex u = Complex.Pow(Q__D.Magnitude > Q_D.Magnitude ? Q__D : Q_D, 1.0 / 3.0);
                Complex v = u.Magnitude != 0.0 ? P / u : 0.0;
                Complex bb_8_3ac = b * b - 8.0 / 3.0 * a * c, _4a = 4.0 * a;
                Complex m1 = Complex.Sqrt(bb_8_3ac + _4a * (u + s_w * s_w * s_w * v));
                Complex m2 = Complex.Sqrt(bb_8_3ac + _4a * (s_w * u + s_w * s_w * v));
                Complex m3 = Complex.Sqrt(bb_8_3ac + _4a * (s_w * s_w * u + s_w * v));
                Complex m, _4awu__wv;
                {
                    double magnitude1 = m1.Magnitude;
                    double magnitude2 = m2.Magnitude;
                    double magnitude3 = m3.Magnitude;
                    if (magnitude1 >= magnitude2 && magnitude1 >= magnitude3)
                    {
                        m = m1;
                        _4awu__wv = _4a * (u + s_w * s_w * s_w * v);
                    }
                    else if (magnitude2 >= magnitude1 && magnitude2 >= magnitude3)
                    {
                        m = m2;
                        _4awu__wv = _4a * (s_w * u + s_w * s_w * v);
                    }
                    else if (magnitude3 >= magnitude1 && magnitude3 >= magnitude2)
                    {
                        m = m3;
                        _4awu__wv = _4a * (s_w * s_w * u + s_w * v);
                    }
                }
                Complex S, T;
                if (m.Magnitude != 0.0)
                {
                    S = 2.0 * b * b - 16.0 / 3.0 * a * c - _4awu__wv;
                    T = (8.0 * a * b * c - 16.0 * a * a * d - 2.0 * b * b * b) / m;
                }
                else
                {
                    S = bb_8_3ac;
                    T = 0.0;
                }
                Complex _4_a = 0.25 / a;

                Complex solution1 = (-b - m + Complex.Sqrt(S - T)) * _4_a;
                Complex solution2 = (-b - m - Complex.Sqrt(S - T)) * _4_a;
                Complex solution3 = (-b + m + Complex.Sqrt(S + T)) * _4_a;
                Complex solution4 = (-b + m - Complex.Sqrt(S + T)) * _4_a;
                solutionSet.Add(solution1);
                if (!solutionSet.Contains(solution2)) { solutionSet.Add(solution2); }
                if (!solutionSet.Contains(solution3)) { solutionSet.Add(solution3); }
                if (!solutionSet.Contains(solution4)) { solutionSet.Add(solution4); }
            }
        }
    }
}