using FieldFactory.DataAccess.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.Framework.Validator
{
    internal class LocationValidator
    {
        LocationProvider provider = new LocationProvider();
        internal bool Exist(int id)
        {
            return true;
        }
    }
}
