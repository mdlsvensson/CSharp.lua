using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLua.Tests {
  [TestClass]
  public class GlobalPackagesFolderTests {
    [TestMethod]
    public void TestNuGetPackagesVariableIsTheFolderPackagesAreReadFrom() {
      var previous = Environment.GetEnvironmentVariable("NUGET_PACKAGES");
      var folder = Path.Combine(Path.GetTempPath(), "csharplua-packages-" + Guid.NewGuid().ToString("N"));
      try {
        Environment.SetEnvironmentVariable("NUGET_PACKAGES", folder);
        Assert.AreEqual(folder, PackageHelper.GetGlobalPackagesPath().TrimEnd(Path.DirectorySeparatorChar));
      } finally {
        Environment.SetEnvironmentVariable("NUGET_PACKAGES", previous);
      }
    }

    [TestMethod]
    public void TestWithNoVariableItIsTheFolderNuGetRestoresTo() {
      var previous = Environment.GetEnvironmentVariable("NUGET_PACKAGES");
      try {
        Environment.SetEnvironmentVariable("NUGET_PACKAGES", null);
        var folder = PackageHelper.GetGlobalPackagesPath();
        Assert.IsTrue(Path.IsPathRooted(folder), folder);
      } finally {
        Environment.SetEnvironmentVariable("NUGET_PACKAGES", previous);
      }
    }
  }
}
