using System;
using System.Collections.Generic;
using System.Text;

namespace TestECS
{
    public static class War
    {
        private static StageManager stageManager;

        public static StageManager StageManager
        {
            get
            {
                return stageManager ?? (stageManager = new StageManager());
            }
        }
    }
}
