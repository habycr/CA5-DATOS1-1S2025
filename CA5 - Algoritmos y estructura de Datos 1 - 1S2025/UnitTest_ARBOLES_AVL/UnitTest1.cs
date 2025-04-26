using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using AVLTree;

namespace UnitTest_ARBOLES_AVL
{
    [TestClass]
    public class UnitTest1
    {
        private AVLTree.AVLTree tree;
        private StringBuilder consoleOutput;
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        [TestInitialize]
        public void Setup()
        {
            tree = new AVLTree.AVLTree();
            consoleOutput = new StringBuilder();
            stringWriter = new StringWriter(consoleOutput);
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }

        [TestMethod]
        public void InsertAndSearch_SingleValue_ReturnsTrue()
        {
            tree.Insert(50);
            consoleOutput.Clear();
            tree.InOrder();
            string output = consoleOutput.ToString().Trim();
            Assert.AreEqual("50", output);
        }

        [TestMethod]
        public void InsertAndSearch_MultipleValues_ReturnsExpectedResults()
        {
            int[] keys = { 10, 5, 15, 3, 8, 12, 20 };
            foreach (var key in keys)
                tree.Insert(key);

            consoleOutput.Clear();
            tree.InOrder();
            string output = consoleOutput.ToString().Trim();
            Assert.AreEqual("3 5 8 10 12 15 20", output);
        }

        [TestMethod]
        public void Delete_LeafNode_RemovesNode()
        {
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(15);

            tree.Delete(5);

            consoleOutput.Clear();
            tree.InOrder();
            string output = consoleOutput.ToString().Trim();
            Assert.AreEqual("10 15", output);
        }

        [TestMethod]
        public void Delete_NodeWithOneChild_RemovesCorrectly()
        {
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(3);

            tree.Delete(5);

            consoleOutput.Clear();
            tree.InOrder();
            string output = consoleOutput.ToString().Trim();
            Assert.AreEqual("3 10", output);
        }

        [TestMethod]
        public void Delete_NodeWithTwoChildren_RemovesCorrectly()
        {
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(15);
            tree.Insert(3);
            tree.Insert(7);

            tree.Delete(5);

            consoleOutput.Clear();
            tree.InOrder();
            string output = consoleOutput.ToString().Trim();
            Assert.AreEqual("3 7 10 15", output);
        }

        [TestMethod]
        public void Delete_RootNode_RemovesRootCorrectly()
        {
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(15);

            tree.Delete(10);

            consoleOutput.Clear();
            tree.InOrder();
            string output = consoleOutput.ToString().Trim();
            Assert.AreEqual("5 15", output);
        }

        [TestMethod]
        public void Delete_NonExistentNode_DoesNotModifyTree()
        {
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(15);

            tree.Delete(100);

            consoleOutput.Clear();
            tree.InOrder();
            string output = consoleOutput.ToString().Trim();
            Assert.AreEqual("5 10 15", output);
        }

        [TestMethod]
        public void InOrder_TraversalOrder_IsCorrect()
        {
            int[] keys = { 10, 5, 15, 3, 8, 12, 20 };
            foreach (var key in keys)
                tree.Insert(key);

            consoleOutput.Clear();
            tree.InOrder();
            string output = consoleOutput.ToString().Trim();
            Assert.AreEqual("3 5 8 10 12 15 20", output);
        }

        [TestMethod]
        public void PreOrder_TraversalOrder_IsCorrect()
        {
            int[] keys = { 10, 5, 15, 3, 8, 12, 20 };
            foreach (var key in keys)
                tree.Insert(key);

            consoleOutput.Clear();
            tree.PreOrder();
            string output = consoleOutput.ToString().Trim();

            // Preorder para este caso esperado: 10, 5, 3, 8, 15, 12, 20
            string[] expectedOrder = { "10", "5", "3", "8", "15", "12", "20" };
            string[] actualOrder = output.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            CollectionAssert.AreEqual(expectedOrder, actualOrder);
        }

        [TestMethod]
        public void Insert_DuplicateKey_DoesNotInsertTwice()
        {
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(10); // Insertar duplicado

            consoleOutput.Clear();
            tree.InOrder();
            string output = consoleOutput.ToString().Trim();
            int count = output.Split(' ').Length;
            Assert.AreEqual(2, count); // Solo dos nodos deben estar
        }

        [TestMethod]
        public void EmptyTree_InOrderTraversal_ReturnsNothing()
        {
            consoleOutput.Clear();
            tree.InOrder();
            string output = consoleOutput.ToString().Trim();
            Assert.AreEqual(string.Empty, output);
        }
    }
}
