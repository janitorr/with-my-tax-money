using System.Collections.Generic;
using TUnit.Core;

using FluentAssertions;


namespace WithMyTaxMoney.Domain.UnitTests;

internal sealed class TaxExpenditureTests
{
    [Test]
    public void GivenDefaultInput_WhenCalledWithEmptyCategories_ReturnEmptyResult()
    {

        var sut = new TaxExpenditure();
        IEnumerable<decimal> result = sut.Calculate(0, new List<decimal>());

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

        result.Should().Contain(testInput.Expected);
    }

    [Test]
    [MethodDataSource(typeof(MyTestDataSources), nameof(MyTestDataSources.SimpleTestData))]
    public void GivenInputCategoriesWithNames_WhenCalledWithOneCategory_GetNamesOnBreakdown(AdditionTestData testInput)
    {
        var sut = new TaxExpenditure();

        IEnumerable<decimal> result = sut.Calculate(testInput.TestTax, testInput.BudgetCategories);

        result.Should().Contain(testInput.Expected);
    }

    [Test]
    [MethodDataSource(typeof(MyTestDataSources), nameof(MyTestDataSources.ComplexTestData))]
    public void GivenInputCategoriesWithNames_WhenCalledWithOneCategory_GetNamesOnBreakdown(AdditionTestData2 testInput)
    {
        var sut = new TaxExpenditure();
        IEnumerable<BudgetCategory> result = sut.Calculate(testInput.TestTax, testInput.BudgetCategories);

        result.Should().Contain(testInput.Expected);
    }

    internal sealed record AdditionTestData(decimal TestTax, List<decimal> BudgetCategories, List<decimal> Expected);
    internal sealed record AdditionTestData2(decimal TestTax, List<BudgetCategory> BudgetCategories, List<BudgetCategory> Expected);

    internal static class MyTestDataSources
    {
        public static AdditionTestData SimpleTestData()
        {

            return new AdditionTestData(10m, [4m, 8m], [2.5m, 1.25m]);
        }

        public static AdditionTestData2 ComplexTestData()
        {
            List<BudgetCategory> input = [
                new BudgetCategory{
                    Name = "Mushrooms",
                    Category = 4m
                },
                new BudgetCategory{
                    Name = "Hyvää puuta",
                    Category = 8m
                }
                ];
            List<BudgetCategory> expected = [
                new BudgetCategory{
                    Name = "Mushrooms",
                    Category = 2.5m
                },
                new BudgetCategory{
                    Name = "Hyvää puuta",
                    Category = 1.25m
                }
                ];
            return new AdditionTestData2(10m, input, expected);
        }
    }
}
