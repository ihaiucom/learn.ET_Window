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


        private List<Component> updates = new List<Component>();
        public void Update()
        {
            List<Component> queue = Stage.GetUnitCompoents(unitCompoentType);

            if (queue == null) return;

            while(queue.Count > 0)
            {
                UnitMoveComponent instance = (UnitMoveComponent)queue[0];
                queue.RemoveAt(0);

                Log.Info($"UnitMoveSystem.Update: StageName={Stage.StageName} Unit.Name={instance.Unit.Name} ");
                updates.Add(instance);
            }

            Stage.SwapUnitCompoents(unitCompoentType, ref updates);
        }

        internal void OnDestroy()
        {
            updates.Clear();
        }
    }
}
