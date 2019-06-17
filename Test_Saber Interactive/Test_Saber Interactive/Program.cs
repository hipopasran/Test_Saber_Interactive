using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Test_Saber_Interactive
{
    class Program
    {

        static Random rand = new Random();

        //  Creating the next node
        static ListNode addNode(ListNode prev)
        {
            ListNode result = new ListNode();
            result.Prev = prev;
            result.Next = null;
            result.Data = rand.Next(0, 100).ToString();
            prev.Next = result;

            return result;
        }

        //  Creating a link to a random node
        static ListNode randomNode(ListNode _head, int _length)
        {
            int k = rand.Next(0, _length);
            int i = 0;
            ListNode result = _head;
            while (i < k)
            {
                result = result.Next;
                i++;
            }

            return result;
        }

        static void Main(string[] args)
        {
            // Nodes count for test
            int length = 5;

            // File name
            string FileName = "dat.dat";

            ListNode head = new ListNode();
            ListNode tail = new ListNode();
            ListNode temp = new ListNode();

            head.Data = rand.Next(0, 1000).ToString();

            tail = head;

            for (int i = 1; i < length; i++)
            {
                tail = addNode(tail);
            }

            temp = head;

            // Add ref to Random node
            for (int i = 0; i < length; i++)
            {
                temp.Rand = randomNode(head, length);
                temp = temp.Next;
            }

            // Declare first List
            ListRand first = new ListRand();
            first.Head = head;
            first.Tail = tail;
            first.Count = length;

            // Serialize
            try
            {
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }
                FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate);
                first.Serialize(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Enter to exit.");
                Console.Read();
                Environment.Exit(0);
            }

            // Deserialize
            ListRand second = new ListRand();
            try
            {
                FileStream fs = new FileStream(FileName, FileMode.Open);
                second.Deserialize(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Enter to exit.");
                Console.Read();
                Environment.Exit(0);
            }

            // Simple tests
            if (first.Head.Data == second.Head.Data)
            {
                Console.WriteLine("Success Head");
            }
            else
            {
                Console.WriteLine("Fail Head!");
            }



            if (second.Tail.Data == first.Tail.Data)
            {
                Console.WriteLine("Success Tail");
            }
            else
            {
                Console.WriteLine("Fail Tail!");
            }
            Console.WriteLine("Press Enter to exit.");
            Console.Read();
        }
    }
}
