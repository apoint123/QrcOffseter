using System.Text;
using System.Text.RegularExpressions;

Console.Write("请输入一个 Offset 值: ");
if (!int.TryParse(Console.ReadLine(), out int addValue))
{
    Console.WriteLine("输入的数字无效");
    return;
}

Console.Write("请输入要修改的 QRC 文件路径: ");
string? inputFilePath = Console.ReadLine();
if (string.IsNullOrEmpty(inputFilePath) || !File.Exists(inputFilePath))
{
    Console.WriteLine("文件不存在");
    return;
}

Console.Write("请输入输出文件路径: ");
string? outputFilePath = Console.ReadLine();
if (string.IsNullOrEmpty(outputFilePath))
{
    Console.WriteLine("输出文件路径无效");
    return;
}

try
{
    StringBuilder outputContent = new();
    Regex regex = NumberRegex.GetNumberPattern();

    foreach (string line in File.ReadLines(inputFilePath))
    {
        string modifiedLine = regex.Replace(line, m =>
        {
            int originalNum = int.Parse(m.Groups[1].Value);
            return (originalNum + addValue).ToString();
        });
        outputContent.AppendLine(modifiedLine);
    }
    File.WriteAllText(outputFilePath, outputContent.ToString());
    Console.WriteLine("处理成功，结果已写入文件。");
}
catch (Exception ex)
{
    Console.WriteLine("写入文件时出错：" + ex.Message);
}

partial class NumberRegex
{
    [GeneratedRegex(@"(?<=\[|\()\s*(\d+)\s*(?=,)")]
    public static partial Regex GetNumberPattern();
}