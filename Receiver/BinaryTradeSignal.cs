using System;
using System.Collections.Generic;
using System.Text;

namespace Receiver
{
    class BinaryTradeSignalParameters
    {
        public decimal amount;
        public string basis;
        public string contract_type;
        public string currency;
        public long date_start;
        public long date_expiry;
        public int duration;
        public string duration_unit;
        public string symbol;
    }
    class BinaryTradeSignalPassthrough
    {
        public int CorrectionId;
    }
    class BinaryTradeSignal
    {
        public decimal price;
        public BinaryTradeSignalParameters parameters;
        public BinaryTradeSignalPassthrough passthrough;
        public int req_id;
    }
}
