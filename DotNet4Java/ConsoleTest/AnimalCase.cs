using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    internal class Dog : AnimalCase
    {
        public Owner? Owner { get; set; }
        public string? Description => $"This is a dog from {Name} race!";
        public int Id { get; init; } = 0;

        public int? Years { get; init; } = 0;
        public string Name { get; set; } = string.Empty;


        public void DoThis(Owner? owner)
        {
            if (!Validate(owner))
            {
                return;
            }

            var name = owner!.Name;

            var animal = new AnimalCase();
            DoSomething();
        }
        private bool Validate(Owner? owner)
        {
            if (owner == null)
            {
                return false;
            }

            return true;
        }
    }
    public class AnimalCase
    {
        private protected void DoSomething()
        {

        }
    }

    public class Owner
    {
        public string Name { get; set; } = string.Empty;
    }
}
