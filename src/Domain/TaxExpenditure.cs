using System.Collections.Generic;
using System.Linq;

namespace WithMyTaxMoney.Domain;

public sealed class TaxExpenditure
{
    public IEnumerable<decimal> Calculate(decimal taxPaid, IEnumerable<decimal> budgetCategories)
    {
        List<decimal> result = [];
        result.AddRange(budgetCategories.Select(category => taxPaid / category));
        return result;
    }
}
