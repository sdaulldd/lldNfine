using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utf8Json;
using Utf8Json.Formatters;
using Utf8Json.Resolvers;

namespace ConsoleAppNewTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // 假设我们有一个 JSON 字符串，其中包含一个 ISO 8601 格式的日期时间
                string jsonString = "{\"dateTime\":\"2025-01-01T01:01:01\"}";
                // 将 JSON 字符串转换为字节数组
                byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonString);
                // 创建一个 JsonReader 对象
                var reader = new JsonReader(jsonBytes);
                // 跳过对象的起始符 '{'
                reader.ReadNext();
                // 读取属性名 "dateTime"
                reader.ReadPropertyName();
                // 创建一个 IJsonFormatterResolver 实例，这里使用 StandardResolver 作为示例
                IJsonFormatterResolver resolver = StandardResolver.AllowPrivateExcludeNullSnakeCase;
                // 使用 ISO8601DateTimeFormatter 对日期时间进行反序列化
                ISO8601DateTimeFormatter formatter = new ISO8601DateTimeFormatter();
                DateTime dateTime = formatter.Deserialize(ref reader, resolver);
                //DateTime dateTime = ISO8601DateTimeFormatter.Deserialize(ref reader, resolver);
                // 输出反序列化后的日期时间
                Console.WriteLine(dateTime);
            }
            catch (Exception ex)
            {

            }
            //MakeEffectiveOfDutyTransfer(); 
            //Console.WriteLine("主任务执行完毕");
            //Console.ReadLine();
        }
        public static Task MakeEffectiveOfDutyTransfer()
        {
            TaskFactory taskFactory = new TaskFactory();
            Task[] dutyTransferTasks = new Task[]
            {
                //组织角色
                taskFactory.StartNew(() =>
                {
                   Thread.Sleep(1000);
                   Console.WriteLine("第一个Task任务执行完毕");
                }),
                //下属员工&其他人员角色
                taskFactory.StartNew(() =>
                {
                    Thread.Sleep(5000);
                     Console.WriteLine("第二个Task任务执行完毕");
                }),
                //入离职待办事项规则
                taskFactory.StartNew(() =>
                {
                   Thread.Sleep(10000);
                     Console.WriteLine("第三个Task任务执行完毕");
                })
            };
            Thread.Sleep(2000);
            Console.WriteLine("我看看我再那个位置");
            Task result = taskFactory.ContinueWhenAll(dutyTransferTasks, (tasks) =>
            {
                Console.WriteLine("并行Task任务执行完毕");
            });

            return result;
        }
    }
}
