using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitFundInfo
{
    public class ChitFundAuctionInfo
    {
        private long id;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        private long chitFundId;

        public long ChitFundId
        {
            get { return chitFundId; }
            set { chitFundId = value; }
        }

        private long chitNo;

        public long ChitNo
        {
            get { return chitNo; }
            set { chitNo = value; }
        }


        private DateTime chitDate;

        public DateTime ChitDate
        {
            get { return chitDate; }
            set { chitDate = value; }
        }


        private long dedution;

        public long Dedution
        {
            get { return dedution; }
            set { dedution = value; }
        }

        private long? memberBalanceCount;

        public long? MemberBalanceCount
        {
            get { return memberBalanceCount; }
            set { memberBalanceCount = value; }
        }

        private float interest;

        public float Interest
        {
            get { return interest; }
            set { interest = value; }
        }

        private float settlement;

        public float Settlement
        {
            get { return settlement; }
            set { settlement = value; }
        }

        private float roundUpAmount;

        public float RoundUpAmount
        {
            get { return roundUpAmount; }
            set { roundUpAmount = value; }
        }

        private long finalSettlementAmount;

        public long FinalSettlementAmount
        {
            get { return finalSettlementAmount; }
            set { finalSettlementAmount = value; }
        }

        private long chitTakenBy;

        public long ChitTakenBy
        {
            get { return chitTakenBy; }
            set { chitTakenBy = value; }
        }

        private DateTime? createdDate;

        public DateTime? CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        private long? createdBy;

        public long? CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        private DateTime? modifiedDate;
        public DateTime? ModifiedDate
        {
            get { return modifiedDate; }
            set { modifiedDate = value; }
        }

        private long? modifiedBy;
        public long? ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }
    }
}
