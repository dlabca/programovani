using Nakup;

namespace NakupTests
{
    [TestClass]
    public class Tests
    {
        void Base(int cena, int[] mince, string expected)
        {
            string output = Program.VyresProblem(cena, mince);
            Assert.AreEqual(expected, output);
        }

        [TestMethod] public void Test1() => Base(1029, [18, 3, 4, 1, 13, 18], "ANO");
        [TestMethod] public void Test2() => Base(416, [1, 7, 3, 4, 12, 15], "ANO");
        [TestMethod] public void Test3() => Base(54, [17, 12, 2, 0, 16, 20], "ANO");
        [TestMethod] public void Test4() => Base(528, [19, 9, 2, 0, 16, 1], "NE");
        [TestMethod] public void Test5() => Base(1121, [5, 6, 1, 4, 4, 0], "NE");
        [TestMethod] public void Test6() => Base(343, [14, 17, 2, 3, 13, 1], "ANO");
        [TestMethod] public void Test7() => Base(578, [3, 2, 0, 1, 9, 5], "NE");
        [TestMethod] public void Test8() => Base(251, [8, 1, 1, 2, 17, 17], "ANO");
        [TestMethod] public void Test9() => Base(713, [0, 16, 10, 16, 11, 5], "NE");
        [TestMethod] public void Test10() => Base(610, [6, 17, 3, 4, 7, 19], "ANO");
        [TestMethod] public void Test11() => Base(1132, [13, 6, 2, 4, 16, 4], "NE");
        [TestMethod] public void Test12() => Base(655, [0, 0, 0, 0, 0, 0], "NE");
        [TestMethod] public void Test13() => Base(425, [2, 4, 1, 1, 9, 11], "ANO");
        [TestMethod] public void Test14() => Base(1023, [18, 2, 3, 1, 16, 1], "NE");
        [TestMethod] public void Test15() => Base(1619, [7, 2, 2, 0, 6, 15], "NE");
        [TestMethod] public void Test16() => Base(1327, [17, 0, 2, 2, 14, 8], "NE");
        [TestMethod] public void Test17() => Base(546, [3, 19, 17, 13, 12, 1], "ANO");
        [TestMethod] public void Test18() => Base(1129, [18, 2, 1, 0, 19, 10], "NE");
        [TestMethod] public void Test19() => Base(1509, [3, 9, 3, 1, 11, 11], "NE");
        [TestMethod] public void Test20() => Base(1542, [12, 15, 2, 2, 14, 17], "NE");
    }
}
