using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Core
{
    public class ExceptionManeger : IExceptionManager
    {
        private string ReferenceConstraintConst = "constraint";

        public  void AddError(Exception ex)
        {
            throw new NotImplementedException();
        }

        public  void AddApplicationLog(Exception ex)
        {
            throw new NotImplementedException();
        }

        public  bool IsReferenceConstraint(Exception ex)
        {
            if (ex.Message.Contains(GetReferenceConstraintText()))
            {
                return true;
            }
            else if (ex.InnerException != null)
            {
                return IsReferenceConstraint(ex.InnerException);
            }
            else
            {
                return false;
            }
        }

        public  string ReferenceConstraintText(Exception ex)
        {
            if (ex.Message.Contains(GetReferenceConstraintText()))
            {
                return ex.Message;
            }
            else if (ex.InnerException != null)
            {
                return ReferenceConstraintText(ex.InnerException);
            }
            else
            {
                return "";
            }
        }

        public  string GetReferenceConstraintText()
        {
            return ReferenceConstraintConst;
        }
    }
}
