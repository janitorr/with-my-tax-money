using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WithMyTaxMoney.Domain.UnitTests;

internal sealed class TaxExpenditureTests
{
    [Test]
    public async Task GivenDefaultInput_WhenCalledWithEmptyCategories_ReturnEmptyResult()
    {
        var sut = new TaxExpenditure();
        IEnumerable<decimal> result = sut.Calculate(0, []);
        await Assert.That(result).IsNotNull();
    }

    [Test]
    [Arguments(10, 2, 5)]
    [Arguments(10, 5, 2)]
    [Arguments(10, 10, 1)]
    public async Task GivenValidInput_WhenCalledWithOneCategory_CorrectBreakdown(
        decimal testTax, decimal budgetCategory, decimal expected)
    {
        var sut = new TaxExpenditure();
        IEnumerable<decimal> result = sut.Calculate(testTax, [budgetCategory]);

        await Assert.That(result).Contains(expected);
    }

    [Test]
    [MethodDataSource(typeof(MyTestDataSources), nameof(MyTestDataSources.SimpleTestData))]
    public async Task GivenValidInput_WhenCalledWithOneCategory_CorrectBreakdown(AdditionTestData foo)
    {
        var sut = new TaxExpenditure();
        IEnumerable<decimal> result = sut.Calculate(foo.TestTax, foo.BudgetCategories);

        await Assert.That(result).Contains(foo.Expected.First());
    }

    internal sealed record AdditionTestData(decimal TestTax, List<decimal> BudgetCategories, List<decimal> Expected);

    internal static class MyTestDataSources
    {
        public static AdditionTestData SimpleTestData()
        {

            return new AdditionTestData(10m, [4m, 8m], [2.5m, 1.25m]);
        }
    }
}
