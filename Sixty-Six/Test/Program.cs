using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<string>();
            while (true)
            {
                var action = Console.ReadLine();
                var num = Console.ReadLine();
                if (action == "a")
                {
                    list.Add(num);
                }
                if (action == "r")
                {
                    list.Remove(num);
                }

                Console.WriteLine();
                foreach (var item in list)
                {
                    Console.Write(item + " ");
                    Console.WriteLine(list.IndexOf(item));
                }
            }
        }
    }
}
