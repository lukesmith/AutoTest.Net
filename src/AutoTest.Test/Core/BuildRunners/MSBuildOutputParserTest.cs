﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoTest.Core.BuildRunners;
using System.IO;
using AutoTest.Messages;

namespace AutoTest.Test.Core.BuildRunners
{
    [TestFixture]
    public class MSBuildOutputParserTest
    {
        [Test]
        public void Should_parse_so_called_compatible_msbuild_output()
        {
			// Test will fail on other platforms than windows because of Path.GetDirectoryName
			if (!isWindows())
				return;
            var resultfile = string.Format("TestResources{0}MSBuild{0}msbuild_windows.txt", Path.DirectorySeparatorChar);
            var result = new BuildRunResults("");
            var parser = new MSBuildOutputParser(result, File.ReadAllLines(resultfile));
            parser.Parse();
            result.ErrorCount.ShouldEqual(3);
            result.Errors[0].File
                .Replace('/', Path.DirectorySeparatorChar)
                .ShouldEqual(@"C:\Users\ack\src\EventStore\EventStore\Program.cs");
            result.Errors[0].LineNumber.ShouldEqual(20);
            result.Errors[0].LinePosition.ShouldEqual(20);
            result.Errors[0].ErrorMessage.ShouldEqual("CS1002: ; expected");
        }
		
		[Test]
        public void Should_parse_so_called_compatible_msbuild_output_with_warnings()
        {
			// Test will fail on other platforms than windows because of Path.GetDirectoryName
			if (!isWindows())
				return;
            var resultfile = string.Format("TestResources{0}MSBuild{0}msbuild_windows_warnings.txt", Path.DirectorySeparatorChar);
            var result = new BuildRunResults("");
            var parser = new MSBuildOutputParser(result, File.ReadAllLines(resultfile));
            parser.Parse();
            result.Warnings.Length.ShouldEqual(1);
            result.Warnings[0].File
                .Replace('/', Path.DirectorySeparatorChar)
                .ShouldEqual(@"C:\Users\ack\src\EventStore\EventStore\Program.cs");
            result.Warnings[0].LineNumber.ShouldEqual(21);
            result.Warnings[0].LinePosition.ShouldEqual(8);
            result.Warnings[0].ErrorMessage.ShouldEqual("CS0219: The variable 'a' is assigned but its value is never used");
        }
		
		private bool isWindows()
		{
			return Environment.OSVersion.Platform == PlatformID.Win32NT ||
					Environment.OSVersion.Platform == PlatformID.Win32S ||
					Environment.OSVersion.Platform == PlatformID.Win32Windows ||
					Environment.OSVersion.Platform == PlatformID.WinCE ||
					Environment.OSVersion.Platform == PlatformID.Xbox;
		}

        [Test]
        public void Should_parse_errors()
        {
			var resultfile = string.Format("TestResources{0}MSBuild{0}msbuild_errors.txt", Path.DirectorySeparatorChar);
            var result = new BuildRunResults("");
            var parser = new MSBuildOutputParser(result, File.ReadAllLines(resultfile));
            parser.Parse();
            result.ErrorCount.ShouldEqual(1);
            result.Errors[0].File
                .Replace('/', Path.DirectorySeparatorChar)
                .ShouldEqual("/home/ack/src/AutoTest.Net/src/AutoTest.Core/Messaging/MessageConsumers/ProjectChangeConsumer.cs".Replace('/', Path.DirectorySeparatorChar));
            result.Errors[0].LineNumber.ShouldEqual(62);
            result.Errors[0].LinePosition.ShouldEqual(50);
            result.Errors[0].ErrorMessage.ShouldEqual("CS1003: ; expected");
        }

        [Test]
        public void Should_parse_warning()
        {
			var resultfile = string.Format("TestResources{0}MSBuild{0}msbuild_warnings.txt", Path.DirectorySeparatorChar);
            var result = new BuildRunResults("");
            var parser = new MSBuildOutputParser(result, File.ReadAllLines(resultfile));
            parser.Parse();
            result.WarningCount.ShouldEqual(2);
			
            result.Warnings[0].File
                .Replace('/', Path.DirectorySeparatorChar)
                .ShouldEqual("/home/ack/src/AutoTest.Net/src/AutoTest.Core/BuildRunners/MSBuildOutputParser.cs".Replace('/', Path.DirectorySeparatorChar));
            result.Warnings[0].LineNumber.ShouldEqual(21);
            result.Warnings[0].LinePosition.ShouldEqual(29);
            result.Warnings[0].ErrorMessage.ShouldEqual("CS1717: Assignment made to same variable; did you mean to assign something else?");
			
			result.Warnings[1].File
                .Replace('/', Path.DirectorySeparatorChar)
                .ShouldEqual("/home/ack/src/AutoTest.Net/src/AutoTest.Test/Core/BuildRunners/MSBuildOutputParserTest.cs".Replace('/', Path.DirectorySeparatorChar));
            result.Warnings[1].LineNumber.ShouldEqual(27);
            result.Warnings[1].LinePosition.ShouldEqual(29);
            result.Warnings[1].ErrorMessage.ShouldEqual("CS1717: Assignment made to same variable; did you mean to assign something else?");
        }
		
		[Test]
        public void Should_parse_succeeded()
        {
			var resultfile = string.Format("TestResources{0}MSBuild{0}msbuild_succeeded.txt", Path.DirectorySeparatorChar);
            var result = new BuildRunResults("");
            var parser = new MSBuildOutputParser(result, File.ReadAllLines(resultfile));
            parser.Parse();
            result.WarningCount.ShouldEqual(0);
            result.ErrorCount.ShouldEqual(0);
        }
    }
}
