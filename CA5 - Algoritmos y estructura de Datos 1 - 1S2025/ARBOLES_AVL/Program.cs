using System;

namespace AVLTree
{
    public class AVLNode
    {
        public int Key { get; set; }
        public int Height { get; set; }
        public AVLNode Left { get; set; }
        public AVLNode Right { get; set; }

        public AVLNode(int key)
        {
            Key = key;
            Height = 1;
            Left = Right = null;
        }
    }

    public class AVLTree
    {
        private AVLNode root;

        private int Height(AVLNode node) => node?.Height ?? 0;

        private int GetBalance(AVLNode node) => node == null ? 0 : Height(node.Right) - Height(node.Left);

        private AVLNode RotateRight(AVLNode y)
        {
            var x = y.Left;
            var T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        private AVLNode RotateLeft(AVLNode x)
        {
            var y = x.Right;
            var T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        public void Insert(int key) => root = Insert(root, key);

        private AVLNode Insert(AVLNode node, int key)
        {
            if (node == null) return new AVLNode(key);

            if (key < node.Key) node.Left = Insert(node.Left, key);
            else if (key > node.Key) node.Right = Insert(node.Right, key);
            else return node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = GetBalance(node);

            // Caso LL (izquierda-izquierda)
            if (balance < -1 && key < node.Left.Key) return RotateRight(node);
            // Caso RR (derecha-derecha)
            if (balance > 1 && key > node.Right.Key) return RotateLeft(node);
            // Caso LR (izquierda-derecha)
            if (balance < -1 && key > node.Left.Key)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            // Caso RL (derecha-izquierda)
            if (balance > 1 && key < node.Right.Key)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        // Método Delete (simplificado)
        public void Delete(int key) => root = Delete(root, key);

        private AVLNode Delete(AVLNode node, int key)
        {
            if (node == null) return node;

            if (key < node.Key) node.Left = Delete(node.Left, key);
            else if (key > node.Key) node.Right = Delete(node.Right, key);
            else
            {
                if (node.Left == null || node.Right == null)
                {
                    var temp = node.Left ?? node.Right;
                    node = temp;
                }
                else
                {
                    var temp = MinValueNode(node.Right);
                    node.Key = temp.Key;
                    node.Right = Delete(node.Right, temp.Key);
                }
            }

            if (node == null) return node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
            int balance = GetBalance(node);

            // Rebalanceo
            if (balance < -1 && GetBalance(node.Left) <= 0) return RotateRight(node);
            if (balance > 1 && GetBalance(node.Right) >= 0) return RotateLeft(node);
            if (balance < -1 && GetBalance(node.Left) > 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balance > 1 && GetBalance(node.Right) < 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        private AVLNode MinValueNode(AVLNode node) => node.Left == null ? node : MinValueNode(node.Left);

        // Recorridos
        public void InOrder() => InOrder(root);
        private void InOrder(AVLNode node)
        {
            if (node == null) return;
            InOrder(node.Left);
            Console.Write($"{node.Key} ");
            InOrder(node.Right);
        }

        public void PreOrder() => PreOrder(root);
        private void PreOrder(AVLNode node)
        {
            if (node == null) return;
            Console.Write($"{node.Key} ");
            PreOrder(node.Left);
            PreOrder(node.Right);
        }

        public void PrintTree() => PrintTree(root, "", true);
        private void PrintTree(AVLNode node, string indent, bool last)
        {
            if (node == null) return;
            Console.Write(indent);
            Console.Write(last ? "└── " : "├── ");
            Console.WriteLine(node.Key);
            PrintTree(node.Left, indent + (last ? "    " : "│   "), false);
            PrintTree(node.Right, indent + (last ? "    " : "│   "), true);
        }
    }

    class Program
    {
        static void Main()
        {
            var tree = new AVLTree();
            int[] keys = { 10, 5, 15, 3, 8, 12, 20, 2, 4, 17, 25 };

            foreach (var key in keys) tree.Insert(key);

            Console.WriteLine("InOrder:");
            tree.InOrder();

            Console.WriteLine("\n\nPreOrder:");
            tree.PreOrder();

            Console.WriteLine("\n\nÁrbol AVL:");
            tree.PrintTree();
        }
    }
}