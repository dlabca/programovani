using WordCounterNS;

namespace WordCounterTests
{
    [TestClass]
    public class Tests
    {
        void Base(string text, int expected)
        {
            WordCounter counter = new WordCounter();
            Assert.AreEqual(expected, counter.CountWords(text));
        }

        [TestMethod]
        public void SimpleTest1()
        {
            Base("This is a simple text prompt", 6);
        }

        [TestMethod]
        public void SimpleTest2()
        {
            Base("This is some other simple text prompt. Or maybe it's not. Maybe it's the same one. Who knows?", 18);
        }

        [TestMethod]
        public void MultipleSpaces() {
            Base("This    is   a simple   text prompt", 6);
        }

        [TestMethod]
        public void SingleWord() {
            Base("This", 1);
        }

        [TestMethod]
        public void MultipleLines() {
            Base("This\nis\na\nsimple\ntext\nprompt", 6);
        }

        [TestMethod]
        public void LeadingWhitespace() {
            Base("      This is a simple text prompt", 6);
        }

        [TestMethod]
        public void TrailingWhitespace() {
            Base("This is a simple text prompt      ", 6);
        }

        [TestMethod]
        public void CombiningWhitespace() {
            Base("This is \n a simple text \n  prompt", 6);
        }

        [TestMethod]
        public void EmptyString() {
            Base("", 0);
        }

        [TestMethod]
        public void SingleComma() {
            Base("This is a , simple text prompt", 6);
        }

        [TestMethod]
        public void SinglePeriod() {
            Base("This . is a simple text prompt", 6);
        }

        [TestMethod]
        public void CommaInsteadOfSpace() {
            Base("Text,prompt", 2);
        }

        [TestMethod]
        public void PeriodInsteadOfSpace() {
            Base("Text.Prompt", 2);
        }
    }
}