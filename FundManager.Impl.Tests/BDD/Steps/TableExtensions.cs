using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace FundManager.Impl.Tests.BDD.Steps
{
    public static class TableExtensions
    {
        public static IEnumerable<IDictionary<string, string>> ParseTable(this Table table)
        {
            var result = new List<Dictionary<string, string>>();
            foreach (var row in table.Rows)
            {
                result.Add(row.ToDictionary(p => p.Key, p => p.Value));
            }

            return result;
        }
    }
}