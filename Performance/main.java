import java.util.Random;

import sun.util.logging.resources.logging;

public class Main {
    public static void main(String args[]) {
        Random rd = new Random();
        int r = rd.nextInt(3) + 10;
        System.out.println(r);
        long t1 = System.currentTimeMillis();
        long sum = 0;

        for (int i = 0; i <= 40000 + r; i++) {
            for (int j = 0; j <= 40000; j++) {
                sum = sum + i * j;
            }
        }

        System.out.println(sum);
        float a = (float) (System.currentTimeMillis() - t1) / 1000;
        System.out.println("実行時間" + a);
    }
}