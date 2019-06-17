using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Saber_Interactive;

namespace Tests
{

    [TestFixture]
    public class Class1
    {

        [Test]
        public void TestSerializationIFAllParamsIsNotEmptyOrNull()
        {
            var head = new ListNode()
            {
                Prev = null,
                Next = null,
                Rand = null,
                Data = "test\" data"
            };

            var tail = new ListNode()
            {
                Prev = head,
                Next = null,
                Rand = head,
                Data = "test data2"
            };
            head.Next = tail;

            var list = new ListRand()
            {
                Head = head,
                Tail = tail,
                Count = 2
            };

            var ser = list.Serialize();
            Assert.That(ser, Is.EqualTo("\"test\"\" data\"n\"test data2\"0"));
            //Assert.AreEqual(ser, "\"test data\"n\"test data2\"0");
        }

        [Test]
        public void TestSerializationIFEmptyRandValue()
        {
            var head = new ListNode()
            {
                Prev = null,
                Next = null,
                Rand = null,
                Data = null
            };

            var tail = new ListNode()
            {
                Prev = head,
                Next = null,
                Rand = null,
                Data = null
            };
            head.Next = tail;

            var list = new ListRand()
            {
                Head = head,
                Tail = tail,
                Count = 2
            };

            var ser = list.Serialize();
            Assert.That(ser, Is.EqualTo(@"nnnn"));
        }

        [Test]
        public void TestSerializationIFNullDataValue()
        {
            var head = new ListNode()
            {
                Prev = null,
                Next = null,
                Rand = null,
                Data = null
            };

            var tail = new ListNode()
            {
                Prev = head,
                Next = null,
                Rand = head,
                Data = null
            };
            head.Next = tail;

            var list = new ListRand()
            {
                Head = head,
                Tail = tail,
                Count = 2
            };

            var ser = list.Serialize();
            Assert.That(ser, Is.EqualTo(@"nnn0"));
        }


        [Test]
        public void TestSerializationIFEmptyDataValue()
        {
            var head = new ListNode()
            {
                Prev = null,
                Next = null,
                Rand = null,
                Data = ""
            };

            var tail = new ListNode()
            {
                Prev = head,
                Next = null,
                Rand = head,
                Data = ""
            };
            head.Next = tail;

            var list = new ListRand()
            {
                Head = head,
                Tail = tail,
                Count = 2
            };

            var ser = list.Serialize();
            Assert.That(ser, Is.EqualTo("\"\"n\"\"0"));
        }

        [Test]
        public void TestDeserializationIfNull()
        {
            string test = "nn\"\"\"n\"\"\"n";

            ListRand Test = new ListRand();

            Test.Deserialize(test);
            Assert.That(Test.Head.Data, Is.EqualTo(null));
            Assert.That(Test.Head.Rand, Is.EqualTo(null));
            Assert.That(Test.Tail.Data, Is.EqualTo("\"n\""));
            Assert.That(Test.Tail.Rand, Is.EqualTo(null));
        }

        [Test]
        public void TestDeserializationIfEmptyString()
        {
            string test = "\"\"n";

            ListRand Test = new ListRand();

            Test.Deserialize(test);
            Assert.That(Test.Head.Data, Is.EqualTo(String.Empty));
            Assert.That(Test.Head.Rand, Is.EqualTo(null));
        }

        [Test]
        public void TestDeserializationIfSymbols()
        {
            string test = "\"{[]\"n";

            ListRand Test = new ListRand();

            Test.Deserialize(test);
            Assert.That(Test.Head.Data, Is.EqualTo("{[]"));
            Assert.That(Test.Head.Rand, Is.EqualTo(null));
        }

        [Test]
        public void TestDeserializationIfNNNInString()
        {
            string test = "\"nnnn\"n";

            ListRand Test = new ListRand();

            Test.Deserialize(test);
            Assert.That(Test.Head.Data, Is.EqualTo("nnnn"));
            Assert.That(Test.Head.Rand, Is.EqualTo(null));
        }

        [Test]
        public void TestDeserializationIfMoreQuotes()
        {
            string test = "\"\"\"\"\"\"n";

            ListRand Test = new ListRand();

            Test.Deserialize(test);
            Assert.That(Test.Head.Data, Is.EqualTo("\"\""));
            Assert.That(Test.Head.Rand, Is.EqualTo(null));
        }

        [Test]
        public void TestDeserializationIfSpaceBeteenQuotes()
        {
            string test = "\" \"n";

            ListRand Test = new ListRand();

            Test.Deserialize(test);
            Assert.That(Test.Head.Data, Is.EqualTo(" "));
            Assert.That(Test.Head.Rand, Is.EqualTo(null));
        }

        [Test]
        public void TestDeserializationIfRandIsNotNull()
        {
            string test = "\"\"0";

            ListRand Test = new ListRand();

            Test.Deserialize(test);
            Assert.That(Test.Head.Data, Is.EqualTo(String.Empty));
            Assert.That(Test.Head.Rand, Is.EqualTo(Test.Head));
        }

        [Test]
        public void TestDeserializationTailTest()
        {
            string test = "\"\"\" \"\"\"0nn";

            ListRand Test = new ListRand();

            Test.Deserialize(test);
            Assert.That(Test.Head.Data, Is.EqualTo("\" \""));
            Assert.That(Test.Head.Rand, Is.EqualTo(Test.Head));
            Assert.That(Test.Tail.Data, Is.EqualTo(null));
            Assert.That(Test.Tail.Rand, Is.EqualTo(null));

        }

        [Test]
        public void TestDeserializationTailTestSecond()
        {
            string test = "\"\"\"n\"\"\"n\" \"0";

            ListRand Test = new ListRand();

            Test.Deserialize(test);
            Assert.That(Test.Head.Data, Is.EqualTo("\"n\""));
            Assert.That(Test.Head.Rand, Is.EqualTo(null));
            Assert.That(Test.Tail.Data, Is.EqualTo(" "));
            Assert.That(Test.Tail.Rand, Is.EqualTo(Test.Head));

        }

        [Test]
        public void TestSerializationDeserialization()
        {
            var head = new ListNode()
            {
                Prev = null,
                Next = null,
                Rand = null,
                Data = "\"\"\"\"\"\""
            };

            var tail = new ListNode()
            {
                Prev = head,
                Next = null,
                Rand = head,
                Data = "\"tail\" \" \"nnn ;:,.&?!"
            };
            head.Next = tail;

            var list = new ListRand()
            {
                Head = head,
                Tail = tail,
                Count = 2
            };

            list.Head.Next = list.Tail;

            var serString = list.Serialize();


            ListRand Test = new ListRand();

            Test.Deserialize(serString);
            Assert.That(Test.Head.Data, Is.EqualTo("\"\"\"\"\"\""));
            Assert.That(Test.Head.Rand, Is.EqualTo(null));
            Assert.That(Test.Tail.Data, Is.EqualTo("\"tail\" \" \"nnn ;:,.&?!"));
            Assert.That(Test.Tail.Rand, Is.EqualTo(Test.Head));

        }
    }
}
