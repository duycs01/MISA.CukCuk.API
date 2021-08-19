using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.MISAAttribute
{
    public class MISAAttribute
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class MISARequired:Attribute
        {
            public string FieldName = string.Empty;

            public MISARequired(string fieldName)
            {
                FieldName = fieldName;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class MISADateTime : Attribute
        {
            public string FieldName = string.Empty;

            public MISADateTime(string fieldName)
            {
                FieldName = fieldName;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class MISANotMap : Attribute
        {

        }
    }
}
