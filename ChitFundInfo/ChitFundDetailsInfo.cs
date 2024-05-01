using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChitFundInfo
{
    public class ChitFundDetailsInfo 
    {
        private long id;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        private string chitName;

        public string ChitName
        {
            get { return chitName; }
            set { chitName = value; }
        }

        private string chitAddress;

        public string ChitAddress
        {
            get { return chitAddress; }
            set { chitAddress = value; }
        }


        private long chitAmount;

        public long ChitAmount
        {
            get { return chitAmount; }
            set { chitAmount = value; }
        }


        private long? totalAmount;

        public long? TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        private long totalMember;

        public long TotalMember
        {
            get { return totalMember; }
            set { totalMember = value; }
        }

        private int interval;

        public int Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
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
        private byte[] chitImage;

        public byte[] ChitImage
        {
            get { return chitImage; }
            set { chitImage = value; }
        }
        private long? chitOwnerId;

        public long? ChitOwnerId
        {
            get { return chitOwnerId; }
            set { chitOwnerId = value; }
        }


        
    }
}
