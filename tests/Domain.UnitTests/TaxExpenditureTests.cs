using System.Collections.Generic;
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
}
