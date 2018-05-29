using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestECS
{
    [ObjectSystem]
    public class UnitAwakeSystem : AwakeSystem<Unit, string>
    {
        public override void Awake(Unit self, string name)
        {
            self.Awake(name);
        }
    }

    public class Unit : Entity
    {
        public Stage Stage
        {
            get
            {
                return GetParent<Stage>();
            }
        }

        public string Name { get; set; }

        public void Awake(string name)
        {
            this.Name = name;
            if(this.Id == 0)
            {
                this.Id = this.InstanceId;
            }
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }


            Stage?.RemoveUnit(this);
            this.Parent = null;

            base.Dispose();
            this.Id = 0;

        }

    }
}
