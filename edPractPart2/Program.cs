using System;
using System.Threading;

namespace edPractPart2
{
    class Program {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            int op = 0;
            int cOp = 1000000000;
            int proc = 0;
            string[] arStr = { "▉", "▉", "▉", "▉", "▉", "▉", "▉", "▉", "▉", "▉" };
            string build= "";
            for (int i = 0; i < cOp; i++)
            {
                if (q1() <= 2) op++;
                if (i % 100000 == 0)
                {
                    if (i == 0)
                    {
                        arStr[0] = "\x1b[32m▉\x1b[0m";
                        build = "";
                        foreach (string s in arStr) { build += s; }
                    }
                    proc++;
                    if (i + 1 == cOp) proc++;
                    Console.Clear();
                    if (proc % 1000000 == 0) {
                        arStr[proc / 10000000] = "\x1b[32m▉\x1b[0m";
                        build = "";
                        foreach(string s in arStr){build += s;}
                    }
                    Console.WriteLine("["+build+"]" + ((float)proc)/100 + "%\n(" + op + "/" + i+")"+((float)op)/i*100+"%");
                }
            }
        }

        public static int q1()
        {
            int x = 0;
            int y = 0;

            for(int i = 0; i <5; i++)
            {
                step(ref x, ref y);
            }
            //Console.WriteLine($"{x}, {y}");
            return Math.Abs(x) + Math.Abs(y);
        }

        public static void alcMindGame(ref bool[] napr)
        {
            Random rnd = new Random();
            int c = 0;
            napr[0] = rnd.Next(1, 3) % 2 == 0 ? true : false;
            napr[1] = rnd.Next(1, 11) % 10 == 0 ? true : false;
            napr[2] = rnd.Next(1, 5) % 4 == 0 ? true : false;
            napr[3] = rnd.Next(1, 5) % 4 == 0 ? true : false;
            foreach(bool b in napr)
            {
                if (b) c++;
            }
            if (c == 0) alcMindGame(ref napr);
        }

        public static int isOne(bool[] napr) {
            int c = 0;
            for(int b = 0; b <napr.Length;b++)
            {
                if (napr[b]) c++;
                if (c > 1) return -1;
            }

            for (int b = 0; b < napr.Length; b++)
            {
                if (napr[b]) return b;
            }
            return -2;

        }

        public static void goToo(int temp, ref int y, ref int x)
        {
            if (temp == 0) y--;
            else if (temp == 1) y++;
            else if (temp == 2) x--;
            else if (temp == 3) x++;
        }

        public static void step(ref int x, ref int y) {
            Random rnd = new Random();
            bool[] napr = new bool[4];
            alcMindGame(ref napr);

            int one = isOne(napr);

            if (one == -2) return;
            if (one == -1)
            {
                int temp;
                do
                {
                    temp = rnd.Next(0, 4);
                }
                while (!napr[temp]);
                one = temp;
            }
            goToo(one, ref y, ref x);
        }

        /**
         *
         *
         *
         *
         */
        public static void q2()
        {
            int x = 0;
            for (int i = 0; i < 10000000; i++)
            {
                int w = forWeek();
                Console.WriteLine($"на этой неделе выигрышь: {w}");
                x += w;
            }
            Console.WriteLine($"{x / 10000000}");

        }

        public static void forThreadQ2()
        {
            Thread t = Thread.CurrentThread;
            Random rnd = new Random();
            long winCount = 0;
            long count = 1000000000;
            for (int i = 0; i < count; i++)
            {
                if (onePartForCheckRate(rnd)) winCount++;
            }

            double s = winCount  / count ;
            Console.WriteLine($"Количество выгрышых дней {winCount}");
            Console.WriteLine($"Вероятность выигрыша: {s}%");
        }

        public static int forWeek()
        {
            Random rnd = new Random();
            int count = 5;
            int sum = 100;
            for (int i = 0; i < count; i++)
            {
                onePart(rnd, ref sum);
            }

            return sum;
        }

        public static bool onePartForCheckRate(Random rnd)
        {
            int orel = 0;
            int resh = 0;
            int tR = 10;
            while (tR != 0)
            {
                tR--;
                if (rnd.Next(1, 3) % 2 == 0) orel++;
                else resh++;

                if (Math.Abs(resh - orel) == 3)
                {
                    return true;
                }
            }
            return false;
        }

        public static void onePart(Random rnd, ref int sum)
        {
            int orel = 0;
            int resh = 0;
            int round = 0;
            while (round != 10)
            {
                round++;
                if (rnd.Next(1, 3) % 2 == 0) orel++;
                else resh++;

                if (Math.Abs(resh - orel) == 3)
                {
                    sum += -10 * round + 80;
                    return;
                }
            }
            sum -= 100;
        }
    }
}