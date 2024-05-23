using CestaDoPrirody;

namespace CestaDoPrirodyTests
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
            Assert.AreEqual(expected.ReadToEnd(), output.ToString());
        }
    }
}