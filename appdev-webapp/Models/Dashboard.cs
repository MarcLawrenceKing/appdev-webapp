using System.Collections.Generic;
using appdev_webapp.Models;

public class Dashboard
{
    public int ExpenseCount { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal AverageExpense { get; set; }
    public Expense MostExpensive { get; set; }
}
