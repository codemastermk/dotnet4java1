using System.Threading.Channels;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            IProducer<Fruit> producerFruit = new ProducerClass<Fruit>();
            IProducer<Apple> producerApple = new ProducerClass<Apple>();


            IConsumer<Fruit> consumerFruit = new ConsumerClass<Fruit>();
            IConsumer<Apple> consumerApple = new ConsumerClass<Apple>();


            IProducer<Fruit> q = producerFruit;
            IProducer<Fruit> e = producerApple;
            IProducer<Apple> r = producerApple;

            q.Produce().PrintSomething();
            e.Produce().PrintSomething();
            r.Produce().PrintSomething();


            IConsumer<Fruit> p = consumerFruit;
            //IConsumer<Fruit> l = consumerApple;
            IConsumer<Apple> m = consumerFruit;
            IConsumer<Apple> n = consumerApple;

            m.Consume(new Apple());

            var helper = new HelperClass<Apple>();

            helper.PrintInstance(consumerFruit);

            Console.ReadLine();
        }
    }

    public class HelperClass<T> where T :Fruit, new()
    {
        private T fruit = new T();
        public void PrintInstance(IConsumer<T> consumer)
        {
            consumer.Consume(fruit);
        }
    }


    public interface IProducer<out T>
    {
        T Produce();
    }

    public interface IConsumer<in T>
    {
        void Consume(T fruit);
    }

    public class ProducerClass<T> : IProducer<T> where T : Fruit, new()
    {
        public T Produce()
        {
            return new T();
        }
    }

    public class ConsumerClass<T> : IConsumer<T> where T : Fruit
    {
        public void Consume(T fruit)
        {
            fruit.PrintSomething();
        }
    }


    public class Fruit
    {
        public virtual void PrintSomething()
        {
            Console.WriteLine("This is some fruit!");
        }
    }

    public class Apple : Fruit
    {
        public override void PrintSomething()
        {
            Console.WriteLine("This is some Apple!");
        }
    }
}