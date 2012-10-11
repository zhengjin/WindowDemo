using System;

namespace ChartDemo
{
    public class TopCustomer
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TopCustomer()
        { }

        private string _customerName;
        private string _company;
        private int _cycle;
        private string _acctNum;
        private Int32 _usage;
        private DateTime _readdate;

        public DateTime Readdate
        {
            get { return _readdate; }
            set { _readdate = value; }
        }

        public string Name
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        public string compName
        {
            get { return _company; }
            set { _company = value; }
        }

        public string AcctNum
        {
            get { return _acctNum; }
            set { _acctNum = value; }
        }

        public int Cycle
        {
            get { return _cycle; }
            set { _cycle = value; }
        }

        public Int32 Usage
        {
            get { return _usage; }
            set { _usage = value; }
        }
    }
}