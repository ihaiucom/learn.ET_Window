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

        private readonly Dictionary<Type, Queue<long>> allUnitCompoents = new Dictionary<Type, Queue<long>>();


        public void Awake(ulong stageId, string stageName)
        {
            this.StageId = stageId;
            this.StageName = stageName;
        }



        public void AddUnit(Unit unit)
        {
            if(allUnits.ContainsKey(unit.Id))
            {
                throw new Exception($"该场景已存在该ID的单位 {unit.Id}");
            }

            if(unit.Parent != null && unit.Parent != this)
            {
                throw new Exception("该单位没有从之前关卡移除");
            }
            else if(unit.Parent == null)
            {
                unit.Parent = this;
            }

            Component[] components = unit.GetComponents();
            Queue<long> list;
            foreach (Component component in components)
            {
                Type type = component.GetType();

                if(allUnitCompoents.ContainsKey(type))
                {
                    list = allUnitCompoents[type];
                }
                else
                {
                    list = allUnitCompoents[type] = new Queue<long>();
                }

                list.Enqueue(unit.Id);
            }

            allUnits.Add(unit.Id, unit);
        }

        public void RemoveUnit(Unit unit)
        {
            if(allUnits.ContainsKey(unit.Id))
            {
                allUnits.Remove(unit.Id);
            }
        }

        public Unit GetUnit(long unitId)
        {
            return allUnits?[unitId];
        }


        public Unit[] GetUnits()
        {
            return this.allUnits.Values.ToArray();
        }


        public T GetUnitCompoent<T>(long unitId) where T : Component
        {
            if(allUnits.ContainsKey(unitId))
            {
                return allUnits[unitId].GetComponent<T>();
            }
            return default(T);
        }


        public T GetUnitCompoent<T>(long unitId, Type type) where T : Component
        {
            if (allUnits.ContainsKey(unitId))
            {
                return (T) allUnits[unitId].GetComponent(type);
            }
            return default(T);
        }

        public Queue<long> GetUnitCompoents<T>()
        {
            Type type = typeof(T);
            
            return allUnitCompoents.ContainsKey(type) ? allUnitCompoents[type] : null;
        }


        public Queue<long> GetUnitCompoents(Type type)
        {
            return allUnitCompoents.ContainsKey(type) ? allUnitCompoents[type] : null;
        }


        public Queue<long> SwapUnitCompoents(Type type, ref Queue<long> queue)
        {
            Queue<long> t = allUnitCompoents[type];
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
