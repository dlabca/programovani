using Lesy;

namespace LesyTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test()
        {
            using var input = new StreamReader("TestFiles/tests.txt");
            using var output = new StringWriter();
            using var expected = new StreamReader("TestFiles/expected.txt");

            Console.SetIn(input);
            Console.SetOut(output);
            Program.Main([]);

            Assert.AreEqual(expected.ReadToEnd(), output.ToString(), true);
        }

        void TestI(int i)
        {
            using var input = new StreamReader("TestFiles/tests.txt");
            using var output = new StringWriter();
            using var expected = new StreamReader("TestFiles/expected.txt");

            Console.SetIn(input);
            Console.SetOut(output);

            int testCount = int.Parse(Console.ReadLine());
            for (int _ = 0; _ < i - 1; _++)
            {
                string[] firstLine = Console.ReadLine().Split(' ');
                int m = int.Parse(firstLine[0]);
                int n = int.Parse(firstLine[1]);
                int l = int.Parse(firstLine[2]);
                for (int __ = 0; __ < l; __++)
                {
                    Console.ReadLine();
                }
                expected.ReadLine();
            }

            Program.Solve();

            Assert.AreEqual(expected.ReadLine() + "\r\n", output.ToString(), true);
        }

        [TestMethod] public void Test1() => TestI(1);
        [TestMethod] public void Test2() => TestI(2);
        [TestMethod] public void Test3() => TestI(3);
        [TestMethod] public void Test4() => TestI(4);
        [TestMethod] public void Test5() => TestI(5);
        [TestMethod] public void Test6() => TestI(6);
        [TestMethod] public void Test7() => TestI(7);
        [TestMethod] public void Test8() => TestI(8);
        [TestMethod] public void Test9() => TestI(9);
        [TestMethod] public void Test10() => TestI(10);
    }
}