using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models.PaymentModels
{
    public class CStatus
    {

        private int _code;

        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _message;

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
