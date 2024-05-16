using Kofola;

namespace KofolaTests
{
    [TestClass]
    public class Tests
    {
        void Base(int cenaVelkeho, int objemVelkeho, int cenaMaleho, int objemMaleho, string expected)
        {
            string output = Program.VyresProblem(cenaVelkeho, objemVelkeho, cenaMaleho, objemMaleho);
            Assert.AreEqual(expected, output);
        }

        [TestMethod] public void Test1() => Base(96, 24, 48, 8, "VETSI");
        [TestMethod] public void Test2() => Base(198, 22, 35, 7, "MENSI");
        [TestMethod] public void Test3() => Base(154, 22, 50, 10, "MENSI");
        [TestMethod] public void Test4() => Base(100, 20, 30, 6, "VETSI");
        [TestMethod] public void Test5() => Base(130, 26, 40, 10, "MENSI");
        [TestMethod] public void Test6() => Base(90, 30, 36, 6, "VETSI");
        [TestMethod] public void Test7() => Base(160, 20, 30, 10, "MENSI");
        [TestMethod] public void Test8() => Base(180, 20, 40, 8, "MENSI");
        [TestMethod] public void Test9() => Base(116, 29, 32, 8, "VETSI");
        [TestMethod] public void Test10() => Base(126, 21, 40, 10, "MENSI");
        [TestMethod] public void Test11() => Base(168, 21, 49, 7, "MENSI");
        [TestMethod] public void Test12() => Base(115, 23, 49, 7, "VETSI");
        [TestMethod] public void Test13() => Base(96, 24, 40, 8, "VETSI");
        [TestMethod] public void Test14() => Base(150, 25, 30, 6, "MENSI");
        [TestMethod] public void Test15() => Base(78, 26, 30, 6, "VETSI");
        [TestMethod] public void Test16() => Base(84, 28, 42, 7, "VETSI");
        [TestMethod] public void Test17() => Base(108, 27, 42, 7, "VETSI");
        [TestMethod] public void Test18() => Base(100, 25, 40, 8, "VETSI");
        [TestMethod] public void Test19() => Base(50, 25, 42, 6, "VETSI");
        [TestMethod] public void Test20() => Base(72, 24, 35, 7, "VETSI");
        [TestMethod] public void Test21() => Base(63, 21, 42, 6, "VETSI");
        [TestMethod] public void Test22() => Base(60, 20, 49, 7, "VETSI");
        [TestMethod] public void Test23() => Base(168, 28, 48, 8, "VETSI");
        [TestMethod] public void Test24() => Base(162, 27, 42, 6, "VETSI");
        [TestMethod] public void Test25() => Base(174, 29, 35, 7, "MENSI");
        [TestMethod] public void Test26() => Base(90, 30, 40, 5, "VETSI");
        [TestMethod] public void Test27() => Base(52, 26, 50, 5, "VETSI");
        [TestMethod] public void Test28() => Base(140, 28, 32, 8, "MENSI");
        [TestMethod] public void Test29() => Base(176, 22, 25, 5, "MENSI");
        [TestMethod] public void Test30() => Base(154, 22, 40, 5, "VETSI");
        [TestMethod] public void Test31() => Base(52, 26, 45, 9, "VETSI");
        [TestMethod] public void Test32() => Base(84, 21, 40, 8, "VETSI");
        [TestMethod] public void Test33() => Base(174, 29, 50, 10, "MENSI");
        [TestMethod] public void Test34() => Base(60, 30, 30, 5, "VETSI");
        [TestMethod] public void Test35() => Base(108, 27, 32, 8, "VETSI");
        [TestMethod] public void Test36() => Base(132, 22, 48, 8, "VETSI");
        [TestMethod] public void Test37() => Base(140, 28, 50, 10, "VETSI");
        [TestMethod] public void Test38() => Base(160, 20, 40, 8, "MENSI");
        [TestMethod] public void Test39() => Base(88, 22, 42, 6, "VETSI");
        [TestMethod] public void Test40() => Base(84, 28, 45, 9, "VETSI");
        [TestMethod] public void Test41() => Base(130, 26, 42, 6, "VETSI");
        [TestMethod] public void Test42() => Base(69, 23, 28, 7, "VETSI");
        [TestMethod] public void Test43() => Base(88, 22, 32, 8, "VETSI");
        [TestMethod] public void Test44() => Base(160, 20, 27, 9, "MENSI");
        [TestMethod] public void Test45() => Base(87, 29, 32, 8, "VETSI");
        [TestMethod] public void Test46() => Base(115, 23, 36, 9, "MENSI");
        [TestMethod] public void Test47() => Base(60, 30, 45, 9, "VETSI");
        [TestMethod] public void Test48() => Base(120, 24, 48, 6, "VETSI");
        [TestMethod] public void Test49() => Base(100, 20, 42, 7, "VETSI");
        [TestMethod] public void Test50() => Base(100, 20, 40, 10, "MENSI");
        [TestMethod] public void Test51() => Base(75, 25, 42, 7, "VETSI");
        [TestMethod] public void Test52() => Base(54, 27, 42, 7, "VETSI");
        [TestMethod] public void Test53() => Base(108, 27, 42, 7, "VETSI");
        [TestMethod] public void Test54() => Base(150, 30, 36, 9, "MENSI");
        [TestMethod] public void Test55() => Base(52, 26, 48, 6, "VETSI");
        [TestMethod] public void Test56() => Base(196, 28, 40, 10, "MENSI");
        [TestMethod] public void Test57() => Base(140, 28, 36, 9, "MENSI");
        [TestMethod] public void Test58() => Base(161, 23, 40, 10, "MENSI");
        [TestMethod] public void Test59() => Base(150, 30, 42, 7, "VETSI");
        [TestMethod] public void Test60() => Base(92, 23, 27, 9, "MENSI");
        [TestMethod] public void Test61() => Base(156, 26, 50, 10, "MENSI");
        [TestMethod] public void Test62() => Base(144, 24, 32, 8, "MENSI");
        [TestMethod] public void Test63() => Base(147, 21, 25, 5, "MENSI");
        [TestMethod] public void Test64() => Base(72, 24, 42, 7, "VETSI");
        [TestMethod] public void Test65() => Base(54, 27, 50, 10, "VETSI");
        [TestMethod] public void Test66() => Base(196, 28, 40, 5, "VETSI");
        [TestMethod] public void Test67() => Base(105, 21, 30, 5, "VETSI");
        [TestMethod] public void Test68() => Base(116, 29, 35, 5, "VETSI");
        [TestMethod] public void Test69() => Base(84, 28, 42, 7, "VETSI");
        [TestMethod] public void Test70() => Base(54, 27, 35, 5, "VETSI");
        [TestMethod] public void Test71() => Base(147, 21, 36, 9, "MENSI");
        [TestMethod] public void Test72() => Base(60, 30, 30, 6, "VETSI");
        [TestMethod] public void Test73() => Base(138, 23, 40, 8, "MENSI");
        [TestMethod] public void Test74() => Base(175, 25, 40, 8, "MENSI");
        [TestMethod] public void Test75() => Base(126, 21, 36, 9, "MENSI");
        [TestMethod] public void Test76() => Base(161, 23, 45, 5, "VETSI");
        [TestMethod] public void Test77() => Base(87, 29, 32, 8, "VETSI");
        [TestMethod] public void Test78() => Base(60, 30, 45, 5, "VETSI");
        [TestMethod] public void Test79() => Base(189, 27, 25, 5, "MENSI");
        [TestMethod] public void Test80() => Base(105, 21, 48, 8, "VETSI");
        [TestMethod] public void Test81() => Base(54, 27, 42, 7, "VETSI");
        [TestMethod] public void Test82() => Base(50, 25, 50, 10, "VETSI");
        [TestMethod] public void Test83() => Base(60, 30, 48, 6, "VETSI");
        [TestMethod] public void Test84() => Base(168, 21, 32, 8, "MENSI");
        [TestMethod] public void Test85() => Base(138, 23, 48, 6, "VETSI");
        [TestMethod] public void Test86() => Base(80, 20, 27, 9, "MENSI");
        [TestMethod] public void Test87() => Base(69, 23, 45, 5, "VETSI");
        [TestMethod] public void Test88() => Base(168, 24, 25, 5, "MENSI");
        [TestMethod] public void Test89() => Base(75, 25, 36, 9, "VETSI");
        [TestMethod] public void Test90() => Base(161, 23, 45, 5, "VETSI");
        [TestMethod] public void Test91() => Base(60, 20, 35, 5, "VETSI");
        [TestMethod] public void Test92() => Base(63, 21, 40, 8, "VETSI");
        [TestMethod] public void Test93() => Base(115, 23, 28, 7, "MENSI");
        [TestMethod] public void Test94() => Base(150, 30, 49, 7, "VETSI");
        [TestMethod] public void Test95() => Base(135, 27, 45, 9, "VETSI");
        [TestMethod] public void Test96() => Base(126, 21, 40, 5, "VETSI");
        [TestMethod] public void Test97() => Base(135, 27, 50, 10, "VETSI");
        [TestMethod] public void Test98() => Base(69, 23, 36, 6, "VETSI");
        [TestMethod] public void Test99() => Base(180, 30, 42, 7, "VETSI");
        [TestMethod] public void Test100() => Base(100, 20, 36, 6, "VETSI");
    }
}
