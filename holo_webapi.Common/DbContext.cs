using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using holo_webapi.Model.Entitys;

namespace holo_webapi.Common
{
    public class DbContext
    {
        public static SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = "Server=localhost;Port=3306;Database=hololive;Uid=root;Pwd=123456;", //连接符字串
            DbType = DbType.MySql, //数据库类型
            IsAutoCloseConnection = true //不设成true要手动close
        }); // 静态属性

        public static void InitDataBase() // 静态方法，外部可以不实例化对象，直接使用
        {
            //初始化/创建数据库:如果不存在则创建数据库
            db.DbMaintenance.CreateDatabase(); //用于创建数据库的方法调用。

            //创建表
            string nspace = "holo_webapi.Model.Entitys"; //定义了要加载的实体类所在的命名空间。

            /*  使用 Assembly.LoadFrom() 方法加载指定路径下的程序集 "holo_webapi.Model.dll"。
                使用 GetTypes() 方法获取程序集中所有的类型。
                使用 LINQ 表达式 Where(p => p.Namespace == nspace) 过滤出命名空间为 "holo_webapi.Model.Entitys" 的类型。
                使用 ToArray() 方法将过滤后的类型转换为 Type 数组，并将结果赋值给 ass 变量。*/
            Type[] ass = Assembly.LoadFrom("bin/Debug/net6.0/holo_webapi.Model.dll").GetTypes().Where(p => p.Namespace == nspace).ToArray();
            /* 反射
                在C#中，Type是一个表示类型的类。它提供了关于类型的元数据信息，包括成员、属性、方法和事件等。
                以下是Type的一些关键属性和方法的解释：

                Type的属性：

                Name：获取类型的名称。
                FullName：获取类型的完全限定名称（包括命名空间）。
                Namespace：获取类型所在的命名空间。
                IsClass：指示类型是否为类。
                IsEnum：指示类型是否为枚举。
                IsInterface：指示类型是否为接口。
                IsArray：指示类型是否为数组。
                IsGenericType：指示类型是否为泛型类型。
                BaseType：获取类型的基类。
                GetProperties()：获取类型的公共属性。
                GetMethods()：获取类型的公共方法。
                GetConstructors()：获取类型的公共构造函数。
                GetInterfaces()：获取类型实现的接口。
                Type的方法：

                GetMethod(string name)：根据方法名称获取指定的方法。
                GetField(string name)：根据字段名称获取指定的字段。
                GetProperty(string name)：根据属性名称获取指定的属性。
                InvokeMember(string name, BindingFlags bindingFlags, Binder binder, object target, params object[] args)：
                通过名称调用指定成员（方法、属性、字段等）。
                
                Type类是通过反射来获取和操作类型信息的重要工具。*/

            //简单说就是Type存储了一个类的完整信息，Type[]存储了一组类的完整信息。

            //初始化 指定的一组实体类(ass)对应的 一组数据库表(其定义的表名就是根据类名而来的)。
            db.CodeFirst.SetStringDefaultLength(200).InitTables(ass); //并设置默认字符串长度为 200。 
            
            //模拟测试数据
            List<Flower> flowers = new List<Flower>() {
                #region 爱情鲜花
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/a1.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=65
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=1 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/a2.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=65
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=1 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/a3.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=65
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=1 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/a4.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=203
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=1 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/a5.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=204
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=1 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/a6.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=205
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=1 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/a7.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=206
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=1 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/a8.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=207
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=1 },
             #endregion
                #region 生日鲜花
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/s1.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=300
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=2 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/s2.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=301
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=2 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/s3.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=302
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=2 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/s4.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=303
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=2 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/s5.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=304
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=2 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/s6.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=305
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=2 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/s7.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=306
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=2 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/s8.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=307
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=2 },
             #endregion
                #region 友情鲜花
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/y1.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=100
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=3 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/y2.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=101
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=3 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/y3.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=102
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=3 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/y4.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=103
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=3 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/y5.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=104
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=3 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/y6.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=105
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=3 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/y7.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=106
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=3 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/y8.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=107
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=3 },
             #endregion
                #region 婚庆鲜花
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/a1.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=500
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=4 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/s1.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=501
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=4 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/y1.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=502
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=4 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/a2.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=503
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=4 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/s2.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=504
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=4 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/y2.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=505
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=4 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/a3.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=506
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=4 },
                new Flower(){ Title ="天宮（あまみや） こころ"
                ,Image="/images/content/s3.jpg"
                ,BigImage="/images/detail/202004091613483166.jpg"
                ,Description="一名从2019年8月13日在YouTube开始活动的VTuber，所属业界团体彩虹社的成员"
                ,Price=507
                ,Language="巫女、萝莉、呆毛、斜刘海、双马尾、披肩双马尾、发饰、单腿袜、鸳鸯袜、飘带"
                ,Material="阿嘛喵（あまみゃ）、阿喵喵（あみゃみゃ）、天宫喵、流泪猫猫头"
                ,Packing="蓝发"
                ,DeliveryRemarks="江浙沪包邮，偏远地区除外，小城市请提前一天预定"
                ,Type=4 },
             #endregion
            };

            //写入测试数据
            db.Insertable(flowers).ExecuteCommand();//创建了一个可插入操作的实例，并将 flowers 列表作为要插入的数据。
            //ExecuteCommand() 方法执行插入操作，并返回受影响的行数或执行结果。

            //Sugar ORM 使用了对象关系映射（ORM）的方式来将数据插入到数据库中。
            //根据约定，默认情况下，Sugar ORM 会将数据插入到与实体类（Flower 类）名称相同的数据库表中。
        }
    }
}
