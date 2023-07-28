using BenchmarkDotNet.Running;
using System;

namespace TesteQueriesRecipeintAndCharge
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //var accountId = 1009;

            //var createdAt = "2023-06-01";
            //var teste = new[] { "re_clj49aafp09vr019tst8gwmdo", "re_clj49aafp09vr019tst8gwmda" };

            //var repository = new Repository();
            //repository.QueryWithDapperAndOpenJson(PgIds.IDS, accountId, createdAt);
            //repository.QueryWithDapperAndOpenJson2(teste, accountId);
            //repository.QueryWithADO(pgid, accountId, createdAt);
            //repository.QueryWithADOAndWithoutParameters(pgid, accountId, createdAt);
            //repository.QueryWithDapper(pgid, accountId, createdAt);
            //repository.QueryWithDapperWithoutParameters(pgid, accountId, createdAt);
            //repository.QueryWithADOOpenJson(pgid, accountId, createdAt);
            //repository.QueryWithADO(pgid, accountId, createdAt);
            //repository.QueryWithDapper(pgid, accountId, createdAt);
            Console.WriteLine("Microbenchmark elapsed time and memory allocation with BenchmarkDotNet! with 301 ids");

            var summary = BenchmarkRunner.Run<Benchmarker>();
            Console.ReadLine();
        }
    }
}
