using System.Linq.Expressions;
using UnityEngine.UIElements;

namespace Script.SuanFa
{
    /// <summary>
    /// 划分链表
    /// </summary>
    public class S_12
    {
        /*给定一个链表的头节点Head，和一个特定值x，
         * 对链表进行分割，使得所有小于x的节点都出现在大于或者等于x的节点之前，
         * 需要保留两个分区中每个节点的初始相对位置（保证安全有序）
         */

        public static LinkListNode SortList(LinkListNode list1, int inValue)
        {
            if (list1 == null || list1.Next == null)
            {
                return list1;
            }

            LinkListNode maxHead = null;
            LinkListNode maxLast = null;
            LinkListNode minHead = null;
            LinkListNode minLast = null;

            while (list1 != null)
            {
                var next = list1.Next;
                list1.Next = null;
                if (list1.Value > inValue)
                {
                    if (maxHead == null)
                    {
                        maxHead = list1;
                        maxLast = list1;
                    }
                    else
                    {
                        maxLast.Next = list1;
                        maxLast = list1;
                    }
                }
                else
                {
                    if (minHead == null)
                    {
                        minHead = list1;
                        minLast = list1;
                    }
                    else
                    {
                        minLast.Next = list1;
                        minLast = list1;
                    }
                }

                list1 = next;
            }

            if (minHead != null)
            {
                minLast.Next = maxHead;
                return minHead;
            }
            else
            {
                return maxHead;
            }
        }
    }
}