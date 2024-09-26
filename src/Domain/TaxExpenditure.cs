using System;
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

    public IEnumerable<BudgetCategory> Calculate(
        decimal taxPaid,
        IEnumerable<BudgetCategory> budgetCategories
    )
    {
        ArgumentNullException.ThrowIfNull(budgetCategories);

        List<BudgetCategory> result = [];
        foreach (BudgetCategory category in budgetCategories)
        {
            var resultCategory = new BudgetCategory()
            {
                Category = taxPaid / category.Category,
                Name = category.Name,
            };

            result.Add(resultCategory);
        }
        return result;
    }
}
