using Dapper;
using VouchersProgram.Data.Interfaces;
using VouchersProgram.Data.Models;
using System.Data;
using System.Diagnostics;

namespace VouchersProgram.Data.Services
{
    public class Expense_AccountService : IExpense_AccountService
    {
        ISqlConnectionConfiguration _configuration;
        public Expense_AccountService(ISqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<IEnumerable<Expense_Account>> Expense_Acc_List()
        {
            IEnumerable<Expense_Account> expenseaccs;
            using var conn = _configuration.GetConnection();
            expenseaccs = await conn.QueryAsync<Expense_Account>("spExpense_Acc_List", commandType: CommandType.StoredProcedure);
            return expenseaccs;
        
        }

        public async Task<int> Expense_Acc_Insert(int ExpenseAccountCode, string ExpenseAccountDescription)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("ExpenseAccountCode", ExpenseAccountCode, DbType.Int32);
            parameters.Add("ExpenseAccountDescription", ExpenseAccountDescription, DbType.String);
            parameters.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spExpense_Acc_Insert", parameters, commandType: CommandType.StoredProcedure);
            Success = parameters.Get<int>("@ReturnValue");
            return Success;
        }

        public async Task<Expense_Account> Expense_Acc_GetOne(int @ExpenseAccountID)
        { 
            Expense_Account expenseaccs = new Expense_Account();
            var parameters = new DynamicParameters();
            parameters.Add("ExpenseAccountID", ExpenseAccountID, DbType.Int32);
            using var conn = _configuration.GetConnection();
            expenseaccs = await conn.QueryFirstOrDefaultAsync<Expense_Account>("spExpense_Acc_GetOne", parameters, commandType: CommandType.StoredProcedure);
            return expenseaccs;
        
        }

        public async Task<int> Expense_Acc_Update(int ExpenseAccountID, int ExpenseAccountCode, string ExpenseAccountDescription, bool ExpenseAccountIsArchived)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("ExpenseAccountID", ExpenseAccountID, DbType.Int32);
            parameters.Add("ExpenseAccountCode", ExpenseAccountCode, DbType.Int32);
            parameters.Add("ExpenseAccountDescription", ExpenseAccountDescription, DbType.String);
            parameters.Add("ExpenseAccountIsArchived", ExpenseAccountIsArchived, DbType.Boolean);
            parameters.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spExpense_Acc_Update", parameters, commandType: CommandType.StoredProcedure);
            Success = parameters.Get<int>("@ReturnValue");
            return Success;
          
        }
    }
}
