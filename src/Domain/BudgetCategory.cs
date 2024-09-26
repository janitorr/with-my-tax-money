namespace WithMyTaxMoney.Domain;

public record BudgetCategory
{
    public required string Name { get; init; }
    public required decimal Category { get; init; }
}
