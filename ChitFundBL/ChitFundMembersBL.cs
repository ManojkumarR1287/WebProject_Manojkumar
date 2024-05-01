using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChitFundDL;
using ChitFundInfo;
namespace ChitFundBL
{
    public class ChitFundMembersBL
    {
        public long SaveChitFundMember(ChitFundMembersInfo cfInfo)
        {
            try
            {
                var cf = new ChitFundDL.ChitFundMembersDL();
                return cf.SaveChitFundMember(cfInfo);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public long SaveChitFundMemberByChitFundId(long chitFundId,long memberId)
        {
            try
            {
                var cf = new ChitFundDL.ChitFundMembersDL();
                return cf.SaveChitFundMemberByChitFundId(chitFundId, memberId);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public DataSet GetChitFundMemberDetails(long chitFundId)
        {
            try
            {
                var cf = new ChitFundDL.ChitFundMembersDL();
                return cf.GetChitFundMemberByChitFundId(chitFundId);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public DataSet GetChitFundMembers()
        {
            try
            {
                var cf = new ChitFundDL.ChitFundMembersDL();
                return cf.GetChitFundMembers();
            }
            catch (Exception)
            {
                return default;
            }
        }
        public DataTable GetChitFundMembetbyId(long id)
        {
            try
            {
                var cf = new ChitFundDL.ChitFundMembersDL();
                return cf.GetChitFundMemberById(id);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public DataSet GetChitFundMemberNotAssignedDetails(long chitFundId)
        {
            try
            {
                var cf = new ChitFundDL.ChitFundMembersDL();
                return cf.GetChitFundMemberNotAssignedDetails(chitFundId);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public bool Validation(long chitFundId, long memberId)
        {
            try
            {
                var mem = new ChitFundDL.ChitFundMembersDL();
                return mem.ValidationMember(chitFundId, memberId);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public bool ValidationCount(long chitFundId, long memberId)
        {
            try
            {
                var mem = new ChitFundDL.ChitFundMembersDL();
                return mem.ValidationCount(chitFundId, memberId);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
