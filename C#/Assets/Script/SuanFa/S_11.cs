using Unity.VisualScripting;

namespace Script.SuanFa
{
    /// <summary>
    /// 两个链表相加
    /// </summary>
    public class S_11
    {
        /// <summary>
        ///  两个链表相加，先给个位，再给十位，以此类推
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="listNode2"></param>
        public void Add2(LinkListNode list1, LinkListNode list2)
        {
            LinkListNode result = null;
            LinkListNode current = null;
            int carry = 0;
            for (int sum, val;
                 list1 != null || list2 != null;
                 list1 = list1?.Next, list2 = list2?.Next)
            {
                sum = (list1?.Value ?? 0) + (list2?.Value ?? 0) + carry;
        ///
        ///
        ///
        ///
        /// 
                val = sum % 10;
                carry = sum / 10;
                if (result == null)
                {
                    result = new LinkListNode();
                    result.Value = val;
                    current = result;
                }
                else
                {
                    current.Next = new LinkListNode();
                    current.Next.Value = val;
                    current = current.Next;
                }
            }

            if (carry == 1)
            {
                current.Next = new LinkListNode();
                current.Next.Value = carry;
            }
        }


        public LinkListNode Add(LinkListNode list1, LinkListNode list2)
        {
            LinkListNode result = new LinkListNode();
            LinkListNode current = result;
            while (list1 != null || list2 != null)
            {
                if (current == null)
                {
                    current = new LinkListNode();
                }

                if (list1 != null)
                {
                    current.Value += list1.Value;
                }

                if (list2 != null)
                {
                    current.Value += list2.Value;
                }

                list1 = list1.Next;
                list2 = list2.Next;
                if (current.Value > 10)
                {
                    current.Value = current.Value % 10;
                    current.Next = new LinkListNode();
                    current.Next.Value = 1;
                    current = current.Next;
                }
            }

            if (current.Value > 10)
            {
                current.Value = current.Value % 10;
                current.Next = new LinkListNode();
                current.Next.Value = 1;
            }

            return result;
        }
    }
}