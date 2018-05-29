using ETModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace TestECS
{
    class Program
    {
        static void Main(string[] args)
        {

            // 异步方法全部会回掉到主线程
            OneThreadSynchronizationContext contex = new OneThreadSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(contex);

            //AssemblyTypes(typeof(Game).Assembly);
            //AssemblyTypes(typeof(StageScene).Assembly);

            Game.EventSystem.Add(DLLType.Model, typeof(Stage).Assembly);


            Component willRemove = null;

            Stage stage1 = ComponentFactory.CreateWithId<Stage, ulong, string>(1, 1001, "关卡一");
            Stage stage2 = ComponentFactory.CreateWithId<Stage, ulong, string>(2, 1002, "关卡二");
            stage1.AddComponent<UnitMoveSystem>();
            stage2.AddComponent<UnitMoveSystem>();

            War.StageManager.Add(stage1);
            War.StageManager.Add(stage2);

            Unit unit = ComponentFactory.CreateWithParent<Unit, string>(stage1, "单位1");
            unit.AddComponent<UnitMoveComponent>();
            stage1.AddUnit(unit);

            unit = ComponentFactory.Create<Unit, string>("单位2");
            unit.AddComponent<UnitMoveComponent>();
            stage1.AddUnit(unit);

            willRemove = unit;


            unit = ComponentFactory.Create<Unit, string>("单位B1");
            unit.AddComponent<UnitMoveComponent>();
            stage2.AddUnit(unit);


            while (true)
            {
                try
                {
                    contex.Update();
                    Game.EventSystem.Update();

                    if (willRemove != null)
                    {
                        willRemove.Dispose();
                        willRemove = null;

                        unit = ComponentFactory.CreateWithParent<Unit, string>(stage1, "单位A11");
                        unit.AddComponent<UnitMoveComponent>();
                        stage1.AddUnit(unit);

                        unit = ComponentFactory.CreateWithParent<Unit, string>(stage1, "单位A12");
                        unit.AddComponent<UnitMoveComponent>();
                        stage1.AddUnit(unit);
                    }

                    Log.Info("--------------------");
                    Thread.Sleep(2000);

                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }

        }

        private static void AssemblyTypes(Assembly assembly)
        {

            Type[] types = assembly.GetTypes();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < types.Length; i++)
            {
                Type type = types[i];
                sb.AppendLine($"{i} {type.FullName}");
            }

            File.WriteAllText($"AssemblyTypes-{assembly.FullName}.txt", sb.ToString());
        }
    }
}
