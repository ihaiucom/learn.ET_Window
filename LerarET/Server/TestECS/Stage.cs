using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETModel;

namespace TestECS
{
    [ObjectSystem]
    public class StageAwakeSystem : AwakeSystem<Stage, ulong, string>
    {
        public override void Awake(Stage self, ulong stageId, string stageName)
        {
            self.Awake(stageId, stageName);
        }
    }


    public class Stage : Entity
    {
        public ulong StageId { get; set; }
        public string StageName { get; set; }

        private readonly Dictionary<long, Unit> allUnits = new Dictionary<long, Unit>();

        private readonly Dictionary<Type, List<Component>> allUnitCompoents = new Dictionary<Type, List<Component>>();


        public void Awake(ulong stageId, string stageName)
        {
            this.StageId = stageId;
            this.StageName = stageName;
        }



        public void AddUnit(Unit unit)
        {
            if(unit.Parent != null && unit.Parent != this)
            {
                throw new Exception("该单位没有从之前关卡移除");
            }
            else if(unit.Parent == null)
            {
                unit.Parent = this;
            }

            Component[] components = unit.GetComponents();
            List<Component> list;
            foreach (Component component in components)
            {
                Type type = component.GetType();

                if(allUnitCompoents.ContainsKey(type))
                {
                    list = allUnitCompoents[type];
                }
                else
                {
                    list = allUnitCompoents[type] = new List<Component>();
                }

                list.Add(component);
            }

            allUnits.Add(unit.Id, unit);
        }

        public void RemoveUnit(Unit unit)
        {
            if(allUnits.ContainsKey(unit.Id))
            {
                Component[] components = unit.GetComponents();
                List<Component> list;
                foreach (Component component in components)
                {
                    Type type = component.GetType();

                    if (allUnitCompoents.ContainsKey(type))
                    {
                        list = allUnitCompoents[type];
                        list.Remove(component);
                    }

                }

                allUnits.Remove(unit.Id);
            }
        }

        public Unit GetUnit(long unitId)
        {
            return allUnits?[unitId];
        }


        public Component[] GetUnits()
        {
            return this.allUnits.Values.ToArray();
        }

        public List<Component> GetUnitCompoents<T>()
        {
            Type type = typeof(T);
            
            return allUnitCompoents.ContainsKey(type) ? allUnitCompoents[type] : null;
        }


        public List<Component> GetUnitCompoents(Type type)
        {
            return allUnitCompoents.ContainsKey(type) ? allUnitCompoents[type] : null;
        }


        public List<Component> SwapUnitCompoents(Type type, ref List<Component> queue)
        {
            List<Component> t = allUnitCompoents[type];
            allUnitCompoents[type] = queue;
            queue = t;
            return t;
        }


        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            foreach (Unit unit in this.GetUnits())
            {
                try
                {
                    unit.Dispose();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }

            allUnits.Clear();
            allUnitCompoents.Clear();
        }
    }
}
