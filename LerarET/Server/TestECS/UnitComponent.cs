using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestECS
{


    public class UnitComponent : Component
    {
        public Unit Unit
        {
            get
            {
                return GetParent<Unit>();
            }
        }

        public Stage Stage
        {
            get
            {
                return Unit?.Stage;
            }
        }
    }
}
