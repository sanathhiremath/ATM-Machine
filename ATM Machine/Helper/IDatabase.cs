using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Machine.Helper
{
    public interface IDatabase
    {        
            void ClearSqlObjects(ref SqlConnection connection, ref SqlCommand command);
            void ClearSqlObjects(ref SqlConnection connection, ref SqlCommand command, ref SqlDataAdapter adaptor);
            void CreateConnection(ref SqlConnection connection);
            void InitializeSqlCommandComponent(ref SqlConnection connection, ref SqlCommand command, string storedProcedureName);
        
    }
}
