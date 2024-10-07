namespace Eldergrove.Engine.Core.Utils;

public static class CSharpJsConverterUtils
{
    public static string ConvertCSharpTypeToTypeScript(string csharpType)
    {
        return csharpType switch
        {
            "Int32"   => "number",
            "Int64"   => "number",
            "float"   => "number",
            "double"  => "number",
            "string"  => "string",
            "String"  => "string",
            "bool"    => "boolean",
            "Boolean" => "boolean",
            "void"    => "void",
            "object"  => "any",
            _         => "any" // Default per i tipi non mappati
        };
    }
}
