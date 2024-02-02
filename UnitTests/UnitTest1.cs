using System.Diagnostics;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestJSEncryption()
        {
            var str = "1234567890123456789";

            string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            Debug.WriteLine(solutiondir);
            string path = Path.Combine(solutiondir,"OnlineVault","Client", "wwwroot", "js");
            Debug.WriteLine(path);

            var engine = new Jurassic.ScriptEngine();
            engine.CompatibilityMode = Jurassic.CompatibilityMode.ECMAScript3;
            engine.ExecuteFile(Path.Combine(path, "jquery-3.7.1.min.js"));
            engine.ExecuteFile(Path.Combine(path, "main.js"));
            bool isValid = engine.CallGlobalFunction<bool>("CheckLength", str);
            Assert.True(isValid);
        }
    }
}