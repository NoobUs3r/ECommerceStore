using System.Reflection;

namespace UnitTests.Shared;

public static class FileReader
{
    public static string ReadEmbededJson(string resourcePath)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using Stream stream = assembly.GetManifestResourceStream(resourcePath);
        using StreamReader reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}