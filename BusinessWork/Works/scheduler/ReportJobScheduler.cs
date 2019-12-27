using Quartz;
using Quartz.Impl;

namespace BusinessWork.Works
{
    public class ReportJobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<ReportJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("triggerName", "groupName")
              //.WithSimpleSchedule(t =>
              //  t.WithIntervalInSeconds(100)
              //   .RepeatForever())
              .WithCronSchedule("0 0 9,16 * * ? ")
                 .Build();

            scheduler.ScheduleJob(job, trigger);
        }
        /// <summary>
        /// 时间间隔执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="seconds">时间间隔(单位：毫秒)</param>
        public static void ExecuteInterval<T>(int seconds) where T : IJob
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler();
            IJobDetail job = JobBuilder.Create<T>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(seconds).RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);

            scheduler.Start();
        }

        /// <summary>
        /// 指定时间执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="cronExpression">cron表达式，即指定时间点的表达式</param>
        public static void ExecuteByCron<T>(string cronExpression) where T : IJob
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler();

            IJobDetail job = JobBuilder.Create<T>().Build();

            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                .WithCronSchedule(cronExpression)
                .Build();

            scheduler.ScheduleJob(job, trigger);

            scheduler.Start();
        }
    }
}