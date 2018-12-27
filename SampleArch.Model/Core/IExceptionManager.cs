using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Core
{
    public interface IExceptionManager
    {

          void AddError(Exception ex);
          void AddApplicationLog(Exception ex);
          Boolean IsReferenceConstraint(Exception ex);
          string ReferenceConstraintText(Exception ex);
          string GetReferenceConstraintText();
    }
}
