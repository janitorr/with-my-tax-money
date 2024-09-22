using System.Threading.Tasks;

namespace WithMyTaxMoney.Domain.UnitTests;

internal sealed class TaxExpenditureTests
{
    [Test]
    public async Task MyTest()
    {
        var result = Add(1, 2);

        await Assert.That(result).IsEqualTo(3);
    }

    private int Add(int x, int y)
    {
        return x + y;
    }
}
