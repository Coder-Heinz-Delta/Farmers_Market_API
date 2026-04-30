using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmers_Market_API.Interfaces
{
    public interface INotifiable
    {
        void Send(string message, string recipient);
    }

    public class EmailNotifier : INotifiable
    {
        public void Send(string message, string recipient) => 
            Console.WriteLine($"[EMAIL SENT to {recipient}]: {message}");
    }

    public class SmsNotifier : INotifiable
    {
        public void Send(string message, string recipient) => 
            Console.WriteLine($"[SMS SENT to {recipient}]: {message}");
    }
}