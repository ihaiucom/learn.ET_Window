using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestECS
{
    public class StageManager : Entity
    {
        private Dictionary<long, Stage> stages = new Dictionary<long, Stage>();

        public void Add(Stage stage)
        {
            stages.Add(stage.Id, stage);
        }


        public bool Remove(long stageId)
        {
            if(stages.ContainsKey(stageId))
            {
                stages.Remove(stageId);
                return true;
            }
            return false;
        }


        public bool Remove(Stage stage)
        {
            return Remove(stage.Id);
        }
    }
}
