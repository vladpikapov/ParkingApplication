using System;
using System.Collections.Generic;
using System.Text;

namespace ServerPart.Data.Helper
{
    public static class QueryManager
    {
        public static string GetQueryForDelete(string tableName, int id)
        {
            return $"DELETE FROM {tableName} WHERE ID = {id}";
        }

        public static string GetQueryForSelect(string tableName, int id = 0)
        {
            if (id > 0)
            {
                return $"SELECT * FROM {tableName} WHERE ID = {id}";
            }
            return $"SELECT * FROM {tableName}";
        }

        //public static string GetQueryForUpdate(string tableName)
        //{
        //    return $"UPDATE {tableName} s"
        //}
    }
}
