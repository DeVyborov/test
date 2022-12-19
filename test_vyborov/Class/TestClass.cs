using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_vyborov.Class
{
    public class TestClass
    {
        Random random = new Random();
        string msg_1 = "Оценка за экзамен: ";
        int[] balls = {4,5,100};
        int[] vs = new int[100];

        public string GetBalls()
        {
            string result = msg_1 + balls[random.Next(0, balls.Length)];
            return result;
        }
    }
}
