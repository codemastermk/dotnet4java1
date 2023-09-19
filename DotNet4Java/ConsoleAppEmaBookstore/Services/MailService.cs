using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEmaBookstore.Services
{
    internal class MailServvice
    {
        public void Notify(object? sender, BookEventArgs eventArgs)
        {
            Console.WriteLine($"Sending an email to confirm: {eventArgs.Message}");

        }
    }
}

