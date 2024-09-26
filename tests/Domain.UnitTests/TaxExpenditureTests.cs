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
}
