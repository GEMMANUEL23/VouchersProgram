using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Interfaces
{
    public interface IExpense_AccountService
    {
        Task<IEnumerable<Expense_Account>> Expense_Acc_List();
        Task<int> Expense_Acc_Insert(int ExpenseAccountCode, string ExpenseAccountDescription);
        Task<Expense_Account> Expense_Acc_GetOne(int @ExpenseAccountID);
        Task<int> Expense_Acc_Update(int ExpenseAccountID, int ExpenseAccountCode, string ExpenseAccountDescription, bool ExpenseAccountIsArchived);
    }
}
