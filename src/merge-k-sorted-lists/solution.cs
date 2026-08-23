class ListNode
{
    public int val;
    public ListNode next;

    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}

ListNode MergeKLists(ListNode[] lists)
{
    if (lists.Length == 0)
    {
        return null;
    }

    return MergeKHelper(lists, 0, lists.Length - 1);
}

ListNode MergeKHelper(ListNode[] lists, int start, int end)
{
    if (start == end)
    {
        return lists[start];
    }

    if (start == end - 1)
    {
        return Merge2Lists(lists[start], lists[end]);
    }

    int mid = start + (end - start) / 2;
    ListNode left = MergeKHelper(lists, start, mid);
    ListNode right = MergeKHelper(lists, mid + 1, end);
    return Merge2Lists(left, right);
}

ListNode Merge2Lists(ListNode l1, ListNode l2)
{
    var output = new ListNode();
    var temp = output;
    while (l1 != null && l2 != null)
    {
        if (l1.val < l2.val)
        {
            temp.next = new ListNode(l1.val);
            l1 = l1.next;
        }
        else
        {
            temp.next = new ListNode(l2.val);
            l2 = l2.next;
        }

        temp = temp.next;
    }

    temp.next = (l1 != null) ? l1 : l2;

    return output.next;
}

void Check(ListNode[] lists, ListNode expected)
{
    ListNode actual = MergeKLists(lists);

    ListNode a = actual;
    ListNode e = expected;

    while (a != null && e != null)
    {
        if (a.val != e.val)
            throw new Exception($"Check failed: expected {e.val}, got {a.val}");

        a = a.next;
        e = e.next;
    }

    if (a != null || e != null)
        throw new Exception("Check failed: output lengths do not match");
}
// Example 1:
//
// Input: lists = [[1,4,5],[1,3,4],[2,6]]
// Output: [1,1,2,3,4,4,5,6]
// Explanation: The linked-lists are:
// [
// 1->4->5,
// 1->3->4,
// 2->6
//     ]
// merging them into one sorted linked list:
// 1->1->2->3->4->4->5->6
//
// Example 2:
//
// Input: lists = []
// Output: []
//
// Example 3:
//
// Input: lists = [[]]
// Output: []
Check(
    new[]
    {
        new ListNode(1, new ListNode(4, new ListNode(5))),
        new ListNode(1, new ListNode(3, new ListNode(4))),
        new ListNode(2, new ListNode(6))
    },
    new ListNode(1,
        new ListNode(1,
            new ListNode(2,
                new ListNode(3,
                    new ListNode(4,
                        new ListNode(4,
                            new ListNode(5,
                                new ListNode(6))))))))
);

Check(
    Array.Empty<ListNode>(),
    null
);

Check(
    new[] { (ListNode)null },
    null
);
Console.WriteLine("All tests passed.");
