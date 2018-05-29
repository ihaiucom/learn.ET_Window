using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestECS
{

    [ObjectSystem]
    public class UnitMoveSystemDestroySystem : DestroySystem<UnitMoveSystem>
    {
        public override void Destroy(UnitMoveSystem self)
        {
            self.OnDestroy();
        }
    }

    [ObjectSystem]
    public class UnitMoveSystemUpdateSystem : UpdateSystem<UnitMoveSystem>
    {
        public override void Update(UnitMoveSystem self)
        {
            self.Update();
        }
    }

    

    public class UnitMoveSystem : UnitSystem
    {
        private Type unitCompoentType = typeof(UnitMoveComponent);


        private Queue<long> updates = new Queue<long>();
        public void Update()
        {
            Stage stage = this.Stage;
            Queue<long> queue = stage.GetUnitCompoents(unitCompoentType);

            if (queue == null) return;

            while(queue.Count > 0)
            {
                long unitId = queue.Dequeue();

                UnitMoveComponent instance = stage.GetUnitCompoent<UnitMoveComponent>(unitId, unitCompoentType);
                if (instance == null)
                    continue;

                updates.Enqueue(unitId);

                Log.Info($"UnitMoveSystem.Update: StageName={Stage.StageName} Unit.Name={instance.Unit.Name} ");
            }

            Stage.SwapUnitCompoents(unitCompoentType, ref updates);
        }

        internal void OnDestroy()
        {
            updates.Clear();
        }
    }
}
