using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.SQL_Commands
{
    public static class Select_Commands
    {
        public static string GetSQLforGetDocForm()
        {
            const string SQL = @"

               select 
                 df.*, 
                 ce.ORGCENTER as OC, 
                 ce.DEPARTMENT as DPRTMNT, 
                 cs.CLASS as CLASSID, 
                 (
                   Case When (
                     df.QUALITY_A + df.QUALITY_B + IND_BONUS_1 + IND_BONUS_2 + IND_BONUS_3 + IND_BONUS_4 + PERFORMANCE_REWARD + MANIPULATION_REWARD
                   ) < 0 Then 0 else (
                     df.QUALITY_A + df.QUALITY_B + IND_BONUS_1 + IND_BONUS_2 + IND_BONUS_3 + IND_BONUS_4 + PERFORMANCE_REWARD + MANIPULATION_REWARD
                   ) End
                 ) as SUM 
               from 
                 DOC_FORMS df 
                 left join CL_EMPLOYEE ce on df.PERSONAL_NR = ce.PERSONAL_NR 
                 left join CL_SALARYCLASS cs on cs.CLASSID = ce.CLASSID 
               where 
                 VISIBLE = 1 
                 and (
                   LOCKED is null 
                   or LOCKED = 0
                 ) 
               order by 
                 CREATED_DATE desc

               ";

            return SQL;
        }

        public static string GetSQLforGetOperationID()
        {
            const string SQL = @"

               select 
                  ID 
                from 
                  DOC_OPERATIONS 
                where 
                  BARCODE = @BARCODE

               ";

            return SQL;
        }
    }
    }
