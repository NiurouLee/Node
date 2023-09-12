using System.Data.SqlTypes;
using JetBrains.Annotations;
using NFrameWork.Collection;

namespace Script.SuanFa
{
    /// <summary>
    /// 单链表 双链表 反转
    /// </summary>
    public class S_09
    {
        // 先分清楚值类型和引用类型

        //整体思路： 先记住下一个，然后改当前指向的引用，然后修改修改上一个指向当前，当前指向下一个
        /// <summary>
        /// 反转链表
        /// </summary>
        /// <param name="inHead"></param>
        /// <returns></returns>
        public LinkListNode RevertListNode(LinkListNode inHead)
        {
            ///这个得存上一个节点，不然就得用栈去存
            ///上一个节点
            LinkListNode pre = null;
            LinkListNode next = null;
            while (inHead != null)
            {
                //下一个
                next = inHead.Next;
                //上一个
                inHead.Next = pre;
                //当前
                pre = inHead;
                //下一个
                inHead = next;
            }

            return pre;
        }


        public DoubleLinkListNode RevertListNode(DoubleLinkListNode inHead)
        {
            DoubleLinkListNode pre = null;
            DoubleLinkListNode next = null;
            while (inHead != null)
            {
                next = inHead.Next;
                inHead.Next = pre;
                inHead.Last = next;
                pre = inHead;
                inHead = next;
            }

            return pre;
        }
    }

    public class LinkListNode
    {
        public LinkListNode Next;
        public int Value;
    }

    public class DoubleLinkListNode
    {
        public DoubleLinkListNode Last;
        public DoubleLinkListNode Next;
        public int Value;
    }
}