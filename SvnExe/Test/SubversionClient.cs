﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using NUnit.Framework;
using SoftwareNinjas.BranchAndReviewTools.Core;

using Parent = SoftwareNinjas.BranchAndReviewTools.SvnExe;

namespace SoftwareNinjas.BranchAndReviewTools.SvnExe.Test
{
    /// <summary>
    /// A class to test <see cref="Parent.SubversionClient"/>.
    /// </summary>
    [TestFixture]
    public class SubversionClient
    {

        private const string infoXml = @"<?xml version=""1.0""?>
<info>
<entry
   kind=""dir""
   path="".""
   revision=""19"">
<url>https://branchreview.googlecode.com/svn/trunk</url>
<repository>
<root>https://branchreview.googlecode.com/svn</root>
<uuid>4154e2d8-f3f9-11dd-87c9-111d11f6bf44</uuid>
</repository>
<wc-info>
<schedule>normal</schedule>
<depth>infinity</depth>
</wc-info>
<commit
   revision=""19"">
<author>olivier.dagenais</author>
<date>2009-06-06T19:56:28.640811Z</date>
</commit>
</entry>
</info>";
        private const string errorMessage = "Bad stuff";

        /// <summary>
        /// Tests <see cref="Parent.SubversionClient.Create(XmlNode)"/> with some XML taken from the output of executing
        /// <c>svn info --xml .</c> on a working copy of this project.
        /// </summary>
        [Test]
        public void Create_FromXmlNode()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(infoXml);
            SvnInfo actualInfo = Parent.SubversionClient.Create(doc);
            CheckSvnInfo(actualInfo);
        }

        private static void CheckSvnInfo(SvnInfo actualInfo)
        {
            Assert.AreEqual(new Uri("https://branchreview.googlecode.com/svn/trunk"), actualInfo.Url);
            Assert.AreEqual(new Uri("https://branchreview.googlecode.com/svn"), actualInfo.RepositoryRoot);
            Assert.AreEqual(new Guid("4154e2d8-f3f9-11dd-87c9-111d11f6bf44"), actualInfo.RepositoryUuid);
            Assert.AreEqual(19, actualInfo.Revision);
        }

        /// <summary>
        /// Tests the <see cref="ISubversionClient.Info(string)"/> implementation of
        /// <see cref="Parent.SubversionClient"/> by simulating success.
        /// </summary>
        [Test]
        public void Info_Simulated_Success()
        {
            SimulatedCapturedProcess instance = new SimulatedCapturedProcess(infoXml, null, 0);
            SimulatedCapturedProcessFactory factory = new SimulatedCapturedProcessFactory(instance);
            ISubversionClient client = new Parent.SubversionClient(factory);
            SvnInfo actualInfo = client.Info(".");
            CheckSvnInfo(actualInfo);
        }

        /// <summary>
        /// Tests the <see cref="ISubversionClient.Info(string)"/> implementation of
        /// <see cref="Parent.SubversionClient"/> by simulating failure.
        /// </summary>
        [Test, ExpectedException(typeof(ApplicationException), errorMessage)]
        public void Info_Simulated_Failure()
        {
            SimulatedCapturedProcess instance = new SimulatedCapturedProcess(null, errorMessage, 1);
            SimulatedCapturedProcessFactory factory = new SimulatedCapturedProcessFactory(instance);
            ISubversionClient client = new Parent.SubversionClient(factory);
            SvnInfo actualInfo = client.Info(".");
        }
    }
}