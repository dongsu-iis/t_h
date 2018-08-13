using System;

class Main
{
    static void Main(string[] args)
    {
        Random rd = new Random();
        int r = rd.Next(10, 13);
        long sum = 0;
        long t = ConvertDateTime(DateTime.Now);

        for (int i = 0; i <= 40000 + r; i++)
        {
            for (int j = 0; j <= 40000; j++)
            {
                sum = sum + i * j;
            }
        }

        Console.WriteLine(r.ToString());
        Console.WriteLine(sum.ToString());
        double f = (double)(ConvertDateTime(DateTime.Now) - t) / 1000;
        Console.WriteLine("実行時間：" + f.ToString());

    }

    public static long ConvertDateTime(DateTime time)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime());
        long t = (time.Ticks - startTime.Ticks);
        return t;
    }
}

