using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;

namespace TesteQueriesRecipeintAndCharge
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(BenchmarkDotNet.Mathematics.NumeralSystem.Arabic)]
    public class Benchmarker
    {
        string[] ids = PgIds.IDS;

        Repository _repository;
        
        public Benchmarker()
        {
            _repository = new Repository();
        }

        [Benchmark(Baseline = true)]
        public void ExecuteWithADO() => _repository.QueryWithADO(ids, 1009, "2023-06-01");

        //[Benchmark]
        //public void ExecuteWithADOAndWithoutParameters() => _repository.QueryWithADOAndWithoutParameters(ids, 1009, "2023-06-01");

        //[Benchmark]
        //public void ExecuteWithDapper() => _repository.QueryWithDapper(ids, 1009, "2023-06-01");

        //[Benchmark]
        //public void ExecuteWithDapperAndWithoutParameters() => _repository.QueryWithDapperWithoutParameters(ids, 1009, "2023-06-01");

        //[Benchmark]
        //public void ExecuteWithADOAndOpenJson() => _repository.QueryWithADOOpenJson(ids, 1009, "2023-06-01");

        [Benchmark]
        public void ExecuteWithDapperAndOpenJson() => _repository.QueryWithDapperAndOpenJson(ids, 1009, "2023-06-01");
    }
}
