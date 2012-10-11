using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.FrameWork
{
    public class HHWorkflowBackButton : HHButton
    {
        public HHWorkflowBackButton()
        {
            OnClientClick = "return confirm('流程退回确认');";
        }
    }
}
