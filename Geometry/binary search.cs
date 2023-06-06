class Node
{
    private int Value { get; set; }
    private Node Left { get; set; }
    private Node Right { get; set; }

    public Node(int value)
    {
        Value = value;
    }

    public void Add(int value)
    {
        if (value < Value)
        {
            if (Left == null)
            {
                Left = new Node(value);
            }
            else
            {
                Left.Add(value);
            }
        }
        else
        {
            if (Right == null)
            {
                Right = new Node(value);
            }
            else
            {
                Right.Add(value);
            }
        }
    }

    public Node Remove(int value)
    {
        if (value < Value)
        {
            if (Left != null)
            {
                Left = Left.Remove(value);
            }
        }
        else if (value > Value)
        {
            if (Right != null)
            {
                Right = Right.Remove(value);
            }
        }
        else
        {
            if (Left == null && Right == null)
            {
                return null;
            }

            if (Left == null)
            {
                return Right;
            }

            if (Right == null)
            {
                return Left;

            }
            
            Value = Right.MinValue();
            Right = Right.Remove(Value);
        }
        return this;
    }

    private int MinValue()
    {
        if (Left == null)
            return Value;
        return Left.MinValue();
    }

    public void Preorder()
    {
        Console.Write(Value + " ");
        if (Left != null)
            Left.Preorder();
        if (Right != null)
            Right.Preorder();
    }
}

static class Program
{
    static void Main()
    {
        Node BST = new Node(5);
        BST.Add(3);
        BST.Add(7);
        BST.Add(2);
        BST.Add(14);
        BST.Add(6);
        BST.Add(8);

        BST.Preorder();
        Console.WriteLine();

        BST.Remove(7);
        BST.Remove(5);
        
        BST.Preorder();
        Console.WriteLine();
    }
}