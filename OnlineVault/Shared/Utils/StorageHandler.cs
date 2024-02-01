namespace OnlineVault.Shared.Utils
{
    public class StorageHandler
    {
        public static string GetFreeName()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string name = Guid.NewGuid().ToString();
            while (File.Exists(Path.Combine(path, name)))
            {
                name = Guid.NewGuid().ToString();
            }

            return name;
        }

        public static async Task SaveBytes(byte[] bytes, string filename )
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            await File.WriteAllBytesAsync(Path.Combine(path, filename), bytes);
        }

        public static async Task<string> ReadFileAsync(string filename)
        {
            return await File.ReadAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", filename));
        }
        public static string ReadFile(string filename)
        {
            return File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", filename));
        }

        public static async Task<byte[]> ReadBytesAsync(string filename)
        {
            return await File.ReadAllBytesAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", filename));
        }
        public static byte[] ReadBytes(string filename)
        {
            return File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", filename));
        }
    }
}
