using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS
{
    public static class Helper
    {
        public static string GetSqlCellString(DbDataReader reader, string cell, string lower="none")
        {
            if (lower == "lower")
            {
                return reader.IsDBNull(reader.GetOrdinal(cell)) ? string.Empty : reader.GetString(reader.GetOrdinal(cell)).ToLower();
            } 
            return reader.IsDBNull(reader.GetOrdinal(cell)) ? string.Empty : reader.GetString(reader.GetOrdinal(cell));
        }

        public static int GetSqlCellInt32(DbDataReader reader, string cell)
        {
            return reader.IsDBNull(reader.GetOrdinal(cell)) ? -1 : reader.GetInt32(reader.GetOrdinal(cell));
        }

        public static DateTime GetSqlCellDate(DbDataReader reader, string cell)
        {
            return reader.GetDateTime(reader.GetOrdinal(cell));
            //return (DateTime)(reader.IsDBNull(reader.GetOrdinal(cell)) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal(cell)));
        }
    }
}
