﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.DataAccess.SQLite
{
    internal static class SQLiteFollowerStringBuilder
    {
        internal static string SelectFollowerByIdQuery(string idFollower)
        {
            return $"SELECT json FROM Follower WHERE idFollower = '{idFollower}'";
        }
    }
}
