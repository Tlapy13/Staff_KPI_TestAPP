using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.SQL_Commands
{
    public static class Update_Commands
    {
        public static string GetSQLforUpdateEmployee()
        {
            const string SQL = @"

                        UPDATE
                          CL_EMPLOYEE
                        set
                          LEAVE_DATE = @LEAVE_DATE,
                          ENTRY_DATE = @ENTRY_DATE,
                          PERSONAL_NR = @PERSONAL_NR,
                          NAME = @NAME,
                          DEPARTMENT = @DEPARTMENT,
                          ORGCENTER = @ORGCENTER,
                          NOTE = @NOTE,
                          CLASSID = @CLASSID,
                          SHIFTSID = @SHIFTSID
                        where
                          PERSONAL_NR = @PERSONAL_NR

                        ";

            return SQL;
        }
    }
}
