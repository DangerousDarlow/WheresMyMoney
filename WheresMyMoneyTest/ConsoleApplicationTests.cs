using WheresMyMoney;

namespace WheresMyMoneyTest;

public class ConsoleApplicationTests
{
    private MockImportCommandProcessor _importCommandProcessor = null!;
    private ConsoleApplication _consoleApplication = null!;

    [SetUp]
    public void Setup()
    {
        _importCommandProcessor = new MockImportCommandProcessor();
        _consoleApplication = new ConsoleApplication(new[] {_importCommandProcessor});
    }

    [TestCase("import", "Missing command parameters")]
    [TestCase("import account", "Missing account parameter")]
    [TestCase("import account test-account", "Missing file parameter")]
    public async Task Import_command_fails_if_parameters_are_missing(string args, string scenario)
    {
        Assert.That(await _consoleApplication.Run(args.Split(' ')), Is.EqualTo(1), $"Incorrect exit code for scenario '{scenario}'");
        Assert.That(_importCommandProcessor.Commands, Is.Empty);
    }

    [TestCase("import account test-account file1")]
    [TestCase("import account test-account file1 file2")]
    public async Task Import_command_parses_and_processes_parameters(string args)
    {
        Assert.That(await _consoleApplication.Run(args.Split(' ')), Is.EqualTo(0));

        Assert.That(_importCommandProcessor.Commands.Count, Is.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(_importCommandProcessor.Commands[0].Account, Is.EqualTo("test-account"), "Account is incorrect");

            var expectedFiles = args.EndsWith("file2") ? new[] {"file1", "file2"} : new[] {"file1"};
            Assert.That(_importCommandProcessor.Commands[0].Files, Is.EqualTo(expectedFiles), "Files are incorrect");
        });
    }
}