namespace Script.SuanFa
{
    public class S_10
    {
        /// <summary>
        /// 合并两个链表  假设是升序
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public LinkListNode MergeLinkList(LinkListNode list1, LinkListNode list2)
        {
            LinkListNode head = list1.Value <= list2.Value ? list1 : list2;
            LinkListNode cur1 = head.Next;
            LinkListNode cur2 = head == list1 ? list2 : list1;
            LinkListNode pre = head;
            while (cur1 != null && cur2 != null)
            {
                if (cur1.Value <= cur2.Value)
                {
                    pre.Next = cur1;
                    cur1 = cur1.Next;
                }
                else
                {
                    pre.Next = cur2;
                    cur2 = cur2.Next;
                }

                pre = pre.Next;
            }

            pre.Next = cur1 ?? cur2;
            return head;
        }
    }
}