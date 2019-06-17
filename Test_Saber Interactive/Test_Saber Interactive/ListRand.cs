using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Test_Saber_Interactive
{
    public class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s)
        {
            List<ListNode> arr = new List<ListNode>();
            ListNode temp = new ListNode();
            temp = Head;
            var serString = this.Serialize();

            // Try write into file
            try
            {
                using (StreamWriter w = new StreamWriter(s))
                {
                    w.Write(serString);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось записать файл, подробности:");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Enter to exit.");
                Console.Read();
                Environment.Exit(0);
            }
        }

        public void Deserialize(FileStream s)
        {
            string line = String.Empty;

            // Try read file
            try
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    line = sr.ReadToEnd();
                    if (line[0].ToString() == "\"" || line[0].ToString() == "n")
                    {
                        this.Deserialize(line);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось обработать файл данных, возможно, он поврежден, подробности:");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Enter to exit.");
                Console.Read();
                Environment.Exit(0);
            }
        }

        // Creating a serialized string
        public string Serialize()
        {
            List<ListNode> arr = new List<ListNode>();
            ListNode temp = new ListNode();
            temp = Head;

            while(temp != null)
            {
                arr.Add(temp);
                temp = temp.Next;
            }


            var result = new StringBuilder();
            string stringValueData;
            string stringValueRand;
            
            foreach (ListNode n in arr)
            {

                // Check Rand for null
                if (n.Rand == null)
                {
                    stringValueRand = "n";
                }
                else
                {
                    stringValueRand = arr.IndexOf(n.Rand).ToString();
                }

                // Check Data for null
                if (n.Data == null)
                {
                    stringValueData = "n";
                }
                else
                {
                    stringValueData = "\"" + n.Data.Replace("\"", "\"\"") + "\"";
                }
                result.Append(stringValueData + stringValueRand);
            }
            return result.ToString();
        }

        // Create a list
        public void Deserialize(string line)
        {
            List<ListNode> arr = new List<ListNode>();
            List<string> ListForRand = new List<string>();
            StringBuilder newDataValue = new StringBuilder();
            StringBuilder newRandValue = new StringBuilder();
            

            bool InDataZone = true;
            Count = 0;
            Head = null;
            Tail = null;


            for (int i = 0; i < line.Length; i++)
            {
                if (i == 0)
                {
                    if (line[i].ToString() == "\"")
                    {
                        InDataZone = true;
                        continue;
                    }
                    else if (line[i].ToString() == "n")
                    {
                        newDataValue = null;
                        InDataZone = false;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (InDataZone)
                    {
                        if (line[i].ToString() == "\"")
                        {
                            if (line[i + 1].ToString() == "\"")
                            {
                                newDataValue.Append(line[i].ToString());
                                i++;
                                InDataZone = true;
                            }
                            else
                            {
                                InDataZone = false;
                            }

                            continue;
                        }
                        else
                        {
                            newDataValue.Append(line[i]);
                            continue;
                        }
                    }
                    else
                    {
                        if (line[i].ToString() == "n")
                        {
                            if (newRandValue != null)
                            {
                                InDataZone = false;
                                ListForRand.Add(newRandValue.ToString());
                                saveData(newDataValue, arr, Count);
                                newDataValue = null;
                                newRandValue = new StringBuilder();

                                continue;
                            }
                            else
                            {
                                InDataZone = true;
                                ListForRand.Add(null);
                                saveData(newDataValue, arr, Count);
                                newDataValue = new StringBuilder();
                                newRandValue = new StringBuilder();
                                continue;
                            }

                        }
                        else if (line[i].ToString() == "\"")
                        {
                            InDataZone = true;
                            ListForRand.Add(newRandValue.ToString());
                            saveData(newDataValue, arr, Count);
                            newDataValue = new StringBuilder();
                            newRandValue = new StringBuilder();
                            continue;
                        }
                        else
                        {
                            newRandValue.Append(line[i].ToString());

                            if (i == line.Length - 1)
                            {
                                if (newRandValue != null)
                                {
                                    InDataZone = true;
                                    ListForRand.Add(newRandValue.ToString());
                                    saveData(newDataValue, arr, Count);
                                    newDataValue = new StringBuilder();
                                    newRandValue = new StringBuilder();
                                    continue;
                                }
                                else
                                {
                                    InDataZone = true;
                                    ListForRand.Add(null);
                                    saveData(newDataValue, arr, Count);
                                    newDataValue = new StringBuilder();
                                    newRandValue = new StringBuilder();
                                    continue;
                                }
                            }
                            continue;
                        }


                    }
                }
            }




            // Return refs to Random nodes and restore Data
            foreach (ListNode n in arr)
            {

                if (arr.IndexOf(n) == 0)
                {
                    n.Prev = null;
                    Head = n;
                }
                else
                {
                    n.Prev = arr[arr.IndexOf(n) - 1];
                }

                if (arr.IndexOf(n) < Count-1)
                {
                    n.Next = arr[arr.IndexOf(n) + 1];
                }
                else
                {
                    n.Next = null;
                    Tail = n;
                }
                if (!string.IsNullOrEmpty(ListForRand[arr.IndexOf(n)]))
                {
                    n.Rand = arr[Convert.ToInt32(ListForRand[arr.IndexOf(n)])];
                }
                else
                {
                    n.Rand = null;
                }
            }
            
        }

        // Private method for add new ListNode to arr
        private void saveData(StringBuilder data, List<ListNode> arr, int count)
        {
            count++;

            if (data != null)
            {
                arr.Add(new ListNode()
                {
                    Prev = null,
                    Next = null,
                    Rand = null,
                    Data = data.ToString()
                });
            }
            else
            {
                arr.Add(new ListNode()
                {
                    Prev = null,
                    Next = null,
                    Rand = null,
                    Data = null
                });
            }
        }
    }
}
