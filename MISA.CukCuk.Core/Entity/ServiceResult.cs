using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entity
{
    public class ServiceResult
    {
        #region Property
        public bool IsValid { get; set; } = true;

        public object Data { get; set; }

        public string Messenger { get; set; }
        #endregion
    }
}
