using System.Reflection;
using System.Runtime.Loader;
using PublicApiGenerator;


if (args.Length < 2)
{
    Console.WriteLine("Usage: PublicApiGen <assemblyPath> <outputFilePath>");
    return 2;
}

var assemblyPath = Path.GetFullPath(args[0]);
var outputPath = Path.GetFullPath(args[1]);

Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);

var resolver = new AssemblyDependencyResolver(assemblyPath);
var alc = new AssemblyLoadContext("PublicApiDump", isCollectible: true);

alc.Resolving += (_, name) =>
{
    var depPath = resolver.ResolveAssemblyToPath(name);
    return depPath is null ? null : alc.LoadFromAssemblyPath(depPath);
};

var asm = alc.LoadFromAssemblyPath(assemblyPath);

var api = asm.GeneratePublicApi();

await File.WriteAllTextAsync(outputPath, api);

alc.Unload();
return 0;
