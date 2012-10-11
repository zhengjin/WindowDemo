using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace ConsoleApplication1
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void C()
        {
            logger.Info("Info CCC");
        }
        static void B()
        {
            logger.Trace("Trace BBB");
            logger.Debug("Debug BBB");
            logger.Info("Info BBB");
            C();
            logger.Warn("Warn BBB");
            logger.Error("Error BBB");
            logger.Fatal("Fatal BBB");
        }
        static void A()
        {
            logger.Trace("Trace AAA");
            logger.Debug("Debug AAA");
            logger.Info("Info AAA");
            B();
            logger.Warn("Warn AAA");
            logger.Error("Error AAA");
            logger.Fatal("Fatal AAA");
        } 

        static void Main(string[] args)
        {
            Console.WriteLine("日志输出开始！");
            logger.Debug("Hello World!");
            logger.Trace("This is a Trace message");
            logger.Debug("This is a Debug message");
            logger.Info("This is an Info message");
            A();
            logger.Warn("This is a Warn message");
            logger.Error("This is an Error message");
            logger.Fatal("This is a Fatal error message"); 
            Console.WriteLine("日志输出结束！");
            Console.ReadLine();

        } 
    }
}
