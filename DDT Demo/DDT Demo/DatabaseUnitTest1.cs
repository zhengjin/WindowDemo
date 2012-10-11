using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.Schema.UnitTesting;
using Microsoft.Data.Schema.UnitTesting.Conditions;

namespace DDT_Demo
{
    [TestClass()]
    public class DatabaseUnitTest1 : DatabaseTestClass
    {

        public DatabaseUnitTest1()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        [TestMethod()]
        public void DatabaseTest1()
        {
            DatabaseTestActions testActions = this.DatabaseTest1Data;
            // 执行预先测试脚本
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "正在执行预先测试脚本...");
            ExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            // 执行测试脚本
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "正在执行测试脚本...");
            ExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            // 执行后期测试脚本
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "正在执行后期测试脚本...");
            ExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        }

        #region 设计器支持代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction DatabaseTest1_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseUnitTest1));
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition1;
            this.DatabaseTest1Data = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            DatabaseTest1_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition1 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            // 
            // DatabaseTest1Data
            // 
            this.DatabaseTest1Data.PosttestAction = null;
            this.DatabaseTest1Data.PretestAction = null;
            this.DatabaseTest1Data.TestAction = DatabaseTest1_TestAction;
            // 
            // DatabaseTest1_TestAction
            // 
            DatabaseTest1_TestAction.Conditions.Add(inconclusiveCondition1);
            resources.ApplyResources(DatabaseTest1_TestAction, "DatabaseTest1_TestAction");
            // 
            // inconclusiveCondition1
            // 
            inconclusiveCondition1.Enabled = true;
            inconclusiveCondition1.Name = "inconclusiveCondition1";
        }

        #endregion


        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        private DatabaseTestActions DatabaseTest1Data;
    }
}
