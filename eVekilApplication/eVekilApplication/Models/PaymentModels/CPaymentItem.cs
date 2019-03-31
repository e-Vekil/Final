using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models.PaymentModels
{
    public class CPaymentItem
    {
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
        private String _lang;

        public String lang
        {
            get { return _lang; }
            set { _lang = value; }
        }
        private String _cardType;

        public String cardType
        {
            get { return _cardType; }
            set { _cardType = value; }
        }
        private String _description;

        public String description
        {
            get { return _description; }
            set { _description = value; }
        }
        private String _hashCode;
        public String hashCode
        {
            get { return _hashCode; }
            set { _hashCode = value; }
        }
    }
}
