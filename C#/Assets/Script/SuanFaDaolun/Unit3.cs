using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  算法导论第三章 函数的增长
/// </summary>
public class Unit3
{
    public Unit3()
    {
        float aa = 11f;
        Math.Floor(aa);
        Math.Ceiling(aa);
        
    }

    /*渐进符号
     * 插入排序的最坏情况为0(n*n),插入排序的最快情况运行时间为a(n*n)+bn+c,其中a,b,c都是常量。
     * 
     * 0表示法：
     * 当给定义一个函数g(n)时，用0(g(n))表示，0(g(n))表示的是一个集合，
     * 0(g(n))={f(n):存在常量c1,c2和n0,使得对所有n>=n0,有0<=c1g(n)<=f(n)<c2g(n)};
     * f(n)是g(n)的一个成员，f(n)的所有值都<=c2g(n) >=c1g(n);
     * 称g(n)是f(n)的一个渐近紧确值。
     *
     * O标记法
     * 0标记法只是给出了一个上界和下界，但只有一个渐进上界时，使用O标记，对于给定函数g(n),用O(g(n)).
     * O(g(n))={f(n):存在正常量c和n0,使得对所有n>=n0,有0<=f(n)<=cg(n)}
     * 我们使用O记号给出函数的一个在常量因子内的上界。
     *
     * Ω记号
     * Ω记号提供了渐进下界，也就是最好的情况，
     * 例如插入排序的运行时间时介于O(n)和Ω(n)的，当默认是有序的时候，时间复杂度是n,当为倒序时，时间为n*n;
     * 
     */

    /*标准记号与常用函数
     * 单调性：
     * 若m<=n,蕴含f(m)<=f(n),则函数f(n)是单调递增的，类似的若m<=n,蕴含f(m)>=f(n),则f(n)是单调递减的。
     * 若m<n,蕴含f(m)<f(n),则f(n)是严格递减的，若m<n,蕴含f(m)>f(n),则f(n)是严格递减的。
     * 
     * 向下取证和向上取整
     * 对于任意实数x，|x|表示<= x的最小整数(读做x向下取整数)，同理[x]表示x向上取整。
     * 在c#中 调用Math.Floor(x), Math.Ceiling(x)
     * 
     * 
     * 模运算
     * 对于任意整数a和任意正整数n，a mod x 的值就是a/n的余数。
     * 在C#中 调用(a%b)
     *
     * 多项式
     * n的d次多项式
     *
     * 指数
     * 对于所有实数a>0,m和n,我们有以下恒等式
     * a0=1,
     * a1=a,
     * a-1=1/a;
     * (am)n=amn
     * (am)n=(an)m
     * aman=a(m+n)
     *
     * 对数
     * lgn=log2n
     * lnn=logen
     * l
     *
     * 
     * 
     */
}