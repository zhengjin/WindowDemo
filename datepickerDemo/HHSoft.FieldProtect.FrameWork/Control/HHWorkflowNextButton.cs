using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.FrameWork
{
    public class HHWorkflowNextButton : HHButton
    {
        public HHWorkflowNextButton()
        {
            OnClientClick = "return confirm('流程进行确认');";
        }
    }
}
