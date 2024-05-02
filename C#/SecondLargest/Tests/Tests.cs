using SecondLargest;

namespace TestsNS
{
    [TestClass]
    public class SecondLargestTests
    {
        void Base(int expected, int[] list)
        {
            int actual = Program.SecondLargest(list);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod] public void Increasing() => Base(5, [1, 2, 3, 4, 5, 6]);
        [TestMethod] public void Decreasing() => Base(6, [7, 6, 5, 4, 3, 2, 1]);
        [TestMethod] public void First() => Base(4, [4, 2, 1, 5, 3]);
        [TestMethod] public void Last() => Base(9, [8, 5, 7, 10, 6, 8, 9]);
        [TestMethod] public void Repeated() => Base(10, [5, 10, 2, 3, 10, 7, 8]);

        [TestMethod] public void Random1() => Base(90, [67, 78, 64, 66, 77, 83, 73, 18, 31, 24, 67, 21, 91, 70, 90, 85, 51, 21, 33, 79]);
        [TestMethod] public void Random2() => Base(94, [28, 67, 66, 67, 78, 96, 35, 19, 80, 76, 56, 79, 90, 52, 55, 34, 94]);
        [TestMethod] public void Random3() => Base(78, [1, 56, 77, 78, 71, 53, 60, 78, 79, 11, 25, 68]);
        [TestMethod] public void Random4() => Base(39, [39, 11, 25, 15, 69]);
        [TestMethod] public void Random5() => Base(83, [17, 83, 74, 59, 31, 3, 82, 14, 84, 47, 49, 82, 2]);
        [TestMethod] public void Random6() => Base(86, [36, 17, 6, 73, 24, 86, 24, 28, 95]);
        [TestMethod] public void Random7() => Base(90, [54, 88, 1, 97, 59, 14, 7, 5, 13, 11, 49, 6, 9, 90, 17, 64, 47, 84]);
        [TestMethod] public void Random8() => Base(93, [21, 5, 47, 11, 2, 98, 17, 31, 93, 27, 54, 69, 48, 46, 44, 48, 89]);
        [TestMethod] public void Random9() => Base(88, [37, 88, 53, 84, 96, 1]);
        [TestMethod] public void Random10() => Base(82, [88, 11, 66, 60, 82, 28]);
    }

    [TestClass]
    public class NthLargestTests
    {
        void Base(int[] list, int n)
        {
            int actual = Program.NthLargest(list, n);
            Array.Sort(list, (a, b) => b.CompareTo(a));
            int expected = list[n - 1];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod] public void Random1() => Base([80, 25, 30, 50, 22, 96, 43, 66, 71, 38, 21, 18, 6, 3, 8], 9);
        [TestMethod] public void Random2() => Base([78, 96, 27, 15, 41, 70, 58, 93, 70, 74, 1, 29, 49, 22, 8, 88], 5);
        [TestMethod] public void Random3() => Base([83, 14, 20, 85, 88, 18, 98, 98, 91, 96], 8);
        [TestMethod] public void Random4() => Base([22, 68, 97, 86, 85, 64, 85, 5, 20, 80, 35, 93, 16, 54, 44, 60, 76, 33, 1, 89], 17);
        [TestMethod] public void Random5() => Base([56, 54, 53, 75, 57, 36, 53, 1, 28, 58, 79, 33, 44, 87, 60, 42, 71, 16], 8);
        [TestMethod] public void Random6() => Base([14, 99, 33, 44, 81, 27, 34, 62, 11, 56, 15, 88], 8);
        [TestMethod] public void Random7() => Base([61, 21, 37, 20, 9, 44, 41, 19, 20, 73, 47, 21, 66, 53, 95, 81, 80, 74, 68], 14);
        [TestMethod] public void Random8() => Base([12, 94, 76, 80, 80, 45, 27, 27, 61], 1);
        [TestMethod] public void Random9() => Base([34, 13, 87, 26, 16, 50, 95, 87, 13, 73, 15, 79, 66, 30, 68, 85], 10);
        [TestMethod] public void Random10() => Base([99, 77, 23, 60, 56, 50, 81, 82, 6, 3], 7);

        [TestMethod] public void N1_1() => Base([33, 21, 65, 20, 98, 82, 71, 45, 97, 14, 25, 44, 44, 18], 1);
        [TestMethod] public void N1_2() => Base([68, 89, 99, 31, 56, 77, 7, 77, 27, 90, 88, 22, 48, 56], 1);
        [TestMethod] public void N1_3() => Base([42, 78, 9, 39, 36, 10, 19, 94, 18, 47, 28, 59, 93, 99, 19, 100, 55], 1);
        [TestMethod] public void N1_4() => Base([74, 28, 99, 52, 16, 54, 3, 13, 65, 84, 52, 13, 72], 1);
        [TestMethod] public void N1_5() => Base([64, 2, 18, 10, 15, 20, 82, 62, 14, 94, 24, 71, 66, 53, 31, 7, 77, 54, 36], 1);

        [TestMethod] public void NMax_1() => Base([84, 47, 62, 90, 52, 87, 67, 49], 8);
        [TestMethod] public void NMax_2() => Base([70, 44, 18, 15, 64, 18, 49, 68, 68, 5, 66, 65, 18, 24, 98, 24], 16);
        [TestMethod] public void NMax_3() => Base([84, 87, 86, 1, 7, 27, 11, 84, 25, 41, 76, 90, 55], 13);
        [TestMethod] public void NMax_4() => Base([84, 48, 83, 80, 62, 40], 6);
        [TestMethod] public void NMax_5() => Base([69, 99, 95, 94, 49, 13, 16, 43, 36, 88, 29, 45, 13], 13);
    }
}