using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models.PaymentModels
{
    public class CRespGetPaymentResult
    {

        private CStatus _status;

        public CStatus status
        {
            get { return _status; }
            set { _status = value; }
        }
        private String _paymentKey;

        public String paymentKey
        {
            get { return _paymentKey; }
            set { _paymentKey = value; }
        }
        private String _merchantName;

        public String merchantName
        {
            get { return _merchantName; }
            set { _merchantName = value; }
        }
        private int _amount;

        public int amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private int _checkCount;

        public int checkCount
        {
            get { return _checkCount; }
            set { _checkCount = value; }
        }
        private String _paymentDate;

        public String paymentDate
        {
            get { return _paymentDate; }
            set { _paymentDate = value; }
        }
        private String _cardNumber;

        public String cardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }
        private String _language;

        public String language
        {
            get { return _language; }
            set { _language = value; }
        }
        private String _description;

        public String description
        {
            get { return _description; }
            set { _description = value; }
        }
        private String _rrn;

        public String rrn
        {
            get { return _rrn; }
            set { _rrn = value; }
        }

        public override string ToString()
        {
            return "paymentKey=" + _paymentKey + "<br>"
                   + "statuscode=" + status.code + "<br>"
                   + "statusemessage=" + status.message + "<br>"
                   + "amount=" + amount + "<br>"
                   + "merchantname=" + merchantName + "<br>"
                   + "paymentdate=" + paymentDate + "<br>"
                   + "cardnumber=" + cardNumber + "<br>"
                   + "language=" + language + "<br>"
                   + "description=" + description + "<br>"
                   + "rrn=" + rrn;
        }
    }
}
