using System;
using System.Collections.Generic;

namespace SalesManagement.Domain
{
    public static class DomainEventDispatcher
    {
        private static readonly List<string> Logs = new();

        public static void Dispatch<T>(T domainEvent) where T : class
        {
            Logs.Add($"Evento disparado: {domainEvent.GetType().Name} em {DateTime.UtcNow}");
            Console.WriteLine($"Evento disparado: {domainEvent.GetType().Name} em {DateTime.UtcNow}");
        }

        public static IEnumerable<string> GetLogs()
        {
            return Logs;
        }
    }
}

