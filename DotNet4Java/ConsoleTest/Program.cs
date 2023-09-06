namespace ConsoleTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var t1 = GetNumber(4, 5000);
            Console.WriteLine("Going forward");
            var t2 = GetNumber(1, 10000);
            var t3 = GetNumber(7, 1000);

            var taskList = new List<Task<int>>
            {
                t1,
                t2,
                t3
            };

            while(taskList.Count > 0)
            {
                try
                {
                    var result = await Task.WhenAny(taskList);
                    taskList.Remove(result);
                    Console.WriteLine(await result);
                }
                catch (Exception exp)
                {

                }
            }

            var result1 = await t1;
            var result2 = await t1;
            var result3 = await t3;

            Console.WriteLine("Hello, World2!");

            await t1;
            Console.ReadLine();
        }

        public static async Task<T> GetNumber<T>(T number, int delay) where T : struct 
        {
            Console.WriteLine(number);
            await Task.Delay(delay);
            Console.WriteLine(number);
            if(int.Parse(number.ToString()) > 4)
            {
                throw new ArgumentNullException($"Number {number} has exception!");
            }
            
            return number;
        }
    }
}