using System;

namespace ChitFundInfo
{
    public class ChitFundMembersInfo
    {
        private long id;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }
        
        private string chitMemberName;

        public string ChitMemberName
        {
            get { return chitMemberName; }
            set { chitMemberName = value; }
        }

        private string chitMemberAddress;

        public string ChitMemberAddress
        {
            get { return chitMemberAddress; }
            set { chitMemberAddress = value; }
        }


        private long mobile1;

        public long Mobile1
        {
            get { return mobile1; }
            set { mobile1 = value; }
        }


        private long? mobile2;

        public long? Mobile2
        {
            get { return mobile2; }
            set { mobile2 = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
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
       
        private long? chitFundId;

        public long? ChitFundId
        {
            get { return chitFundId; }
            set { chitFundId = value; }
        }


        
    }
}
