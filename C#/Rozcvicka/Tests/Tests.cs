using Rozcvicka;

namespace TestsNS
{
    [TestClass]
    public class Tests
    {
        void Base(string expected, string input) {
            StringReader reader = new StringReader(input);
            StringWriter writer = new StringWriter();
            Program.PrectiVstup(reader, writer);

            string actual = writer.ToString().ReplaceLineEndings();
            expected = expected.ReplaceLineEndings();

            Assert.AreEqual(expected, actual);
        }

        void BaseFile(string expectedFilename, string inputFilename) {
            string dir = "TestFiles/";

            using StreamReader expected = new StreamReader(dir + expectedFilename);
            using StreamReader input = new StreamReader(dir + inputFilename);

            StringWriter actual = new StringWriter();
            Program.PrectiVstup(input, actual);

            Assert.AreEqual(expected.ReadToEnd().ReplaceLineEndings(), actual.ToString().ReplaceLineEndings());
        }

        [TestMethod] public void Lehky1() => Base(
            "1409\n" +
            "469\n" +
            "567\n" +
            "662\n" +
            "360\n" +
            "759\n" +
            "993\n" +
            "1306\n" +
            "247\n" +
            "1246\n",

            "10\n" +
            "916\n" +
            "953\n" +
            "398\n" +
            "58\n" +
            "277\n" +
            "289\n" +
            "155\n" +
            "219\n" +
            "503\n" +
            "130\n" +
            "262\n" +
            "175\n" +
            "895\n" +
            "133\n" +
            "672\n" +
            "589\n" +
            "311\n" +
            "170\n" +
            "29\n" +
            "161\n" +
            "776\n" +
            "527\n" +
            "743\n" +
            "577\n" +
            "135\n" +
            "869\n" +
            "100\n" +
            "24\n" +
            "750\n" +
            "907\n" +
            "254\n" +
            "145\n" +
            "136\n" +
            "126\n" +
            "52\n" +
            "69\n" +
            "711\n" +
            "786\n" +
            "267\n" +
            "193\n"
        );

        [TestMethod]
        public void Lehky2() => Base(
            "858\n" +
            "1023\n" +
            "836\n" +
            "1591\n" +
            "1011\n" +
            "1353\n" +
            "960\n" +
            "129\n" +
            "915\n" +
            "811\n",

            "10\n" +
            "861\n" +
            "700\n" +
            "47\n" +
            "111\n" +
            "471\n" +
            "732\n" +
            "137\n" +
            "154\n" +
            "778\n" +
            "384\n" +
            "16\n" +
            "436\n" +
            "990\n" +
            "996\n" +
            "484\n" +
            "111\n" +
            "963\n" +
            "851\n" +
            "45\n" +
            "115\n" +
            "963\n" +
            "693\n" +
            "404\n" +
            "256\n" +
            "184\n" +
            "789\n" +
            "16\n" +
            "155\n" +
            "57\n" +
            "95\n" +
            "28\n" +
            "6\n" +
            "809\n" +
            "341\n" +
            "559\n" +
            "485\n" +
            "826\n" +
            "441\n" +
            "699\n" +
            "583\n"
        );

        [TestMethod]
        public void Lehky3() => Base(
            "1019\n" +
            "1248\n" +
            "596\n" +
            "1223\n" +
            "1292\n" +
            "1547\n" +
            "1030\n" +
            "1545\n" +
            "791\n" +
            "625\n",

            "10\n" +
            "695\n" +
            "400\n" +
            "223\n" +
            "548\n" +
            "558\n" +
            "790\n" +
            "164\n" +
            "294\n" +
            "738\n" +
            "68\n" +
            "536\n" +
            "412\n" +
            "727\n" +
            "687\n" +
            "79\n" +
            "457\n" +
            "505\n" +
            "933\n" +
            "160\n" +
            "199\n" +
            "945\n" +
            "984\n" +
            "863\n" +
            "464\n" +
            "760\n" +
            "484\n" +
            "623\n" +
            "351\n" +
            "954\n" +
            "776\n" +
            "616\n" +
            "523\n" +
            "922\n" +
            "39\n" +
            "754\n" +
            "338\n" +
            "531\n" +
            "197\n" +
            "176\n" +
            "458\n"
        );

        [TestMethod] public void Tezky1() => BaseFile("A-tezky1-out.txt", "A-tezky1.txt");
        [TestMethod] public void Tezky2() => BaseFile("A-tezky2-out.txt", "A-tezky2.txt");
        [TestMethod] public void Tezky3() => BaseFile("A-tezky3-out.txt", "A-tezky3.txt");
    }
}