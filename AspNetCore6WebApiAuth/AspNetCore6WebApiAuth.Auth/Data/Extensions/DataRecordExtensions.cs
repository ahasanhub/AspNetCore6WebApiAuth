﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AspNetCore6WebApiAuth.Auth.Data.Extensions
{
    public static class DataRecordExtensions
    {
        public static bool HasColumn(this IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
