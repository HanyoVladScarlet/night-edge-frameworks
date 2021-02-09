using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFrameworks.MathLib
{
    /// <summary>
    /// 整型分数，用于避免浮点数计算中出现的偶然误差
    /// </summary>
    public struct NefxFracInt
    {
        // 公有字段成员
        #region PublicField

        public int nf_numerator;
        public int nf_denominator;  // 需添加分母为零时的情况处理
        public float Value { get { return (float)this.nf_numerator / this.nf_denominator; } }
        public bool IsSimple { get { return GetIsSimple(); } }

        #endregion

        public NefxFracInt(int numerator = 1, int denominator = 1)
        {
            this.nf_numerator = numerator;
            this.nf_denominator = denominator;
        }

        public NefxFracInt(Tuple<int, int> tuple)
        {
            this.nf_numerator = tuple.Item1;
            this.nf_denominator = tuple.Item2;
        }

        // 运算符重载
        #region OperatorOverride

        /// <summary>
        /// 整型分数的加法会自动返回最简形式
        /// </summary>
        /// <param name="nf1">加数1</param>
        /// <param name="nf2">加数2</param>
        /// <returns></returns>
        public static NefxFracInt operator +(NefxFracInt nf1, NefxFracInt nf2)
        {
            int gcd = NumThoery.GetGreatCommonDivisor(nf1.nf_denominator, nf2.nf_denominator);
            nf1.NF_AmplifyFracint(gcd);
            nf2.NF_AmplifyFracint(gcd);
            return new NefxFracInt(nf1.nf_numerator + nf2.nf_numerator, nf1.nf_denominator + nf2.nf_denominator).NF_SimplifyFracint();
        }

        /// <summary>
        /// 整型分数的减法会自动返回最简形式
        /// </summary>
        /// <param name="nf1">被减数</param>
        /// <param name="nf2">减数</param>
        /// <returns></returns>
        public static NefxFracInt operator -(NefxFracInt nf1, NefxFracInt nf2)
        {
            nf2.nf_denominator = -nf2.nf_denominator;
            return nf1 + nf2;
        }

        /// <summary>
        /// 整型分数的数乘会自动返回最简形式
        /// </summary>
        /// <param name="factor">数乘因数</param>
        /// <param name="fracint">整分数</param>
        /// <returns>数乘结果是一个整分数</returns>
        public static NefxFracInt operator *(int factor, NefxFracInt fracint)
        {
            return new NefxFracInt(fracint.nf_numerator * factor, fracint.nf_denominator).NF_SimplifyFracint();
        }

        /// <summary>
        /// 整形分数的乘法会自动返回最简形式
        /// </summary>
        /// <param name="nf1">乘数1</param>
        /// <param name="nf2">乘数2</param>
        /// <returns></returns>
        public static NefxFracInt operator *(NefxFracInt nf1, NefxFracInt nf2)
        {
            return new NefxFracInt(nf1.nf_numerator * nf2.nf_numerator, nf1.nf_denominator * nf2.nf_denominator).NF_SimplifyFracint();
        }

        /// <summary>
        /// 整形分数的除法会自动返回最简形式
        /// </summary>
        /// <param name="dividee">被除数</param>
        /// <param name="divider">除数</param>
        /// <returns>商分数</returns>
        public static NefxFracInt operator /(NefxFracInt dividee, NefxFracInt divider)
        {
            return dividee * divider.NF_GetReciprocal();
        }
        #endregion

        // 对当前对象自身进行操做
        #region ModifySelf
        /// <summary>
        /// 将自身的分子分母同时扩大，倍数为factor的大小
        /// 此方法会对对象本身造成影响！
        /// </summary>
        /// <param name="factor">扩大的因数</param>
        /// <returns>返回值是本身</returns>
        public NefxFracInt NF_AmplifyFracint(int factor)
        {
            this.nf_denominator *= factor;
            this.nf_numerator *= factor;
            return this;
        }

        public NefxFracInt NF_SimplifyFracint()
        {
            int gcd = NumThoery.GetGreatCommonDivisor(this.nf_numerator, this.nf_denominator);
            this.nf_numerator /= gcd;
            this.nf_denominator /= gcd;
            return this;
        }

        #endregion

        // 根据当前对象创建一个新的对象
        // 不改变当前对象
        #region GetNewInstance

        public NefxFracInt NF_GetReciprocal()
        {
            int top = this.nf_denominator;
            int bot = this.nf_numerator;
            return new NefxFracInt(top, bot);
        }

        public NefxFracInt NF_GetAmplified(int factor)
        {
            return new NefxFracInt(this.nf_numerator * factor, this.nf_denominator * factor);
        }

        public NefxFracInt NF_GetSimplified()
        {
            int gcd = NumThoery.GetGreatCommonDivisor(this.nf_numerator, this.nf_denominator);
            return new NefxFracInt(this.nf_numerator / gcd, this.nf_denominator / gcd);
        }

        #endregion

        // 类型内部的方法组
        #region PrivateMethods

        private bool GetIsSimple()
        {
            return NumThoery.GetGreatCommonDivisor(this.nf_numerator, this.nf_denominator) == 1 ? true : false;
        }

        #endregion
    }

    public static class IntExtension
    {
        /// <summary>
        /// 将整数转化为整型分数，默认分母为1
        /// </summary>
        /// <param name="num">目标整型数</param>
        /// <returns>整数的等值整型分数</returns>
        public static NefxFracInt ToNefxFracInt(this int num)
        {
            return new NefxFracInt(num, 1);
        }


        /// <summary>
        /// 将整数转化为整型分数，第二个参数是分母的值
        /// </summary>
        /// <param name="num">目标整型数</param>
        /// <param name="factor">分母大小</param>
        /// <returns>整数的等值整型分数</returns>
        public static NefxFracInt ToNefxFracInt(this int num,int factor)
        {
            return new NefxFracInt(num * factor, factor);
        }
    }

    public static class FloatExtension
    {
        /// <summary>
        /// 将浮点数转化为整型分数，保留小数点后四位精度
        /// </summary>
        /// <param name="num">目标浮点数字</param>
        /// <returns>浮点数的等值整型分数</returns>
        public static NefxFracInt ToNefxFracInt(this float num)
        {
            int top = Convert.ToInt32(num * 1e4);
            return new NefxFracInt(top, 10000);
        }

        /// <summary>
        /// 将浮点数转化为整型分数，第二个参数为精度值
        /// </summary>
        /// <param name="num">目标浮点数</param>
        /// <param name="denominator">分母数值</param>
        /// <returns>浮点数的等值整型分数</returns>
        public static NefxFracInt ToNefxFracInt(this float num, int denominator)
        {
            int top = Convert.ToInt32(num * denominator);
            return new NefxFracInt(top, (int)denominator);
        }
    }
}
