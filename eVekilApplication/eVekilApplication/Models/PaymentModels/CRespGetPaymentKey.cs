using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models.PaymentModels
{
    public class CRespGetPaymentKey
    {
        private CStatus _status;

        public CStatus status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _paymentKey;

        public string paymentKey
        {
            get { return _paymentKey; }
            set { _paymentKey = value; }
        }
    }
}
