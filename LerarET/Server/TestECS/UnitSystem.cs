using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestECS
{
    public class UnitSystem : Component
    {
        public Stage Stage
        {
            get
            {
                return GetParent<Stage>();
            }
        }

    }
}
