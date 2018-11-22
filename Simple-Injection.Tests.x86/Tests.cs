﻿using System;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace Simple_Injection.Tests.x86
{
    public class Tests : IDisposable
    {
        // Create an instance of an Injector
        
        private readonly Injector _injector = new Injector();
        
        // Path to test dll

        private readonly string _dllPath = Path.GetFullPath(@"..\..\") + "Test-Dll-x86.dll";
        
        // Test process

        private readonly Process _process;
        

        public Tests()
        {
            // Create a new test process
            
            _process = new Process { StartInfo = { CreateNoWindow = true, FileName = "cmd.exe"} };

            _process.Start();
        }
        
        public void Dispose()
        {
            // Terminate the test process
            
            _process.Kill();     
        }
        
        [Fact]
        public void TestCreateRemoteThread()
        {
            Assert.True(_injector.CreateRemoteThread(_dllPath, _process.ProcessName));
        }
        
        [Fact]
        public void TestQueueUserAPC()
        {
            Assert.True(_injector.QueueUserAPC(_dllPath, _process.ProcessName));
        }
        
        [Fact]
        public void TestRtlCreateUserThread()
        {
            Assert.True(_injector.RtlCreateUserThread(_dllPath, _process.ProcessName));
        }
        
        [Fact]
        public void TestSetThreadContext()
        {
            Assert.True(_injector.SetThreadContext(_dllPath, _process.ProcessName));
        }

        [Fact]
        public void TestEraseHeaders()
        {
            _injector.RtlCreateUserThread(_dllPath, _process.ProcessName);
            
            Assert.True(_injector.EraseHeaders(_dllPath, _process.ProcessName));
        }
        
        [Fact]
        public void TestRandomiseHeaders()
        {
            _injector.RtlCreateUserThread(_dllPath, _process.ProcessName);
            
            Assert.True(_injector.RandomiseHeaders(_dllPath, _process.ProcessName));
        }
    }
}