using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitFundInfo
{
    public class UserInfo
    {
        private long id;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }
        
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string chitOwnerMail;

        public string ChitOwnerMail
        {
            get { return chitOwnerMail; }
            set { chitOwnerMail = value; }
        }
        private long mobile;

        public long Mobile
        {
            get { return mobile; }
            set { mobile = value; }
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
