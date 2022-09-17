using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AspNetCore6WebApiAuth.Auth.Data.Extensions
{
    public static class SqlDataExtensions
    {
		public static T Get<T>(this SqlDataReader reader, string columnName)
		{			
			if (reader.IsDBNull(reader.GetOrdinal(columnName)))
				return default(T);
			return reader.GetFieldValue<T>(reader.GetOrdinal(columnName));
		}
		
		public static object DBNullHandler(this object value)
		{
			if (value != null)
                return value;

			return DBNull.Value;
		}
	}
}
