using System.Collections.Generic;

using FluentAssertions;


namespace WithMyTaxMoney.Domain.UnitTests;

internal sealed class TaxExpenditureTests
{
    [Test]
    public void GivenDefaultInput_WhenCalledWithEmptyCategories_ReturnEmptyResult()
    {
        var sut = new TaxExpenditure();
        IEnumerable<decimal> result = sut.Calculate(0, []);
        result.Should().NotBeNull();

    }

    [Test]
    [Arguments(10, 2, 5)]
    [Arguments(10, 5, 2)]
    [Arguments(10, 10, 1)]
    public void GivenValidInput_WhenCalledWithOneCategory_CorrectBreakdown(
        decimal testTax, decimal budgetCategory, decimal expected)
    {
        var sut = new TaxExpenditure();
        IEnumerable<decimal> result = sut.Calculate(testTax, [budgetCategory]);
        result.Should().NotBeNull().And.Contain(expected);
    }

    [Test]
    [MethodDataSource(typeof(MyTestDataSources), nameof(MyTestDataSources.SimpleTestData))]
    public void GivenValidInput_WhenCalledWithOneCategory_CorrectBreakdown(AdditionTestData testInput)
    {
        var sut = new TaxExpenditure();
        IEnumerable<decimal> result = sut.Calculate(testInput.TestTax, testInput.BudgetCategories);

        result.Should().Contain(testInput.TestTax);
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
