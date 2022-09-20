using Moq;
using WheresMyMoney;

namespace WheresMyMoneyTest;

public class ConsoleApplicationTests
{
    private Mock<ICommandProcessor> _commandProcessor = null!;
    private ConsoleApplication _consoleApplication = null!;

    [SetUp]
    public void Setup()
    {
        _commandProcessor = new Mock<ICommandProcessor>();
        _consoleApplication = new ConsoleApplication(_commandProcessor.Object);
    }

    [TestCase("load", "Missing command parameters")]
    [TestCase("load account", "Missing account parameter")]
    [TestCase("load account test-account", "Missing file parameter")]
    public async Task Load_command_fails_if_parameters_are_missing(string args, string scenario) =>
        Assert.That(await _consoleApplication.Run(args.Split(' ')), Is.EqualTo(1), $"Incorrect exit code for scenario '{scenario}'");

    [TestCase("load account test-account file1")]
    [TestCase("load account test-account file1 file2")]
    public async Task Load_command_parses_and_processes_parameters(string args)
    {
        var commands = new List<LoadCommand>();
        _commandProcessor.Setup(processor => processor.ProcessCommand(Capture.In(commands)));

        Assert.That(await _consoleApplication.Run(args.Split(' ')), Is.EqualTo(0));

        _commandProcessor.Verify(processor => processor.ProcessCommand(It.IsAny<LoadCommand>()), Times.Once);
        Assert.Multiple(() =>
        {
            Assert.That(commands[0].Account, Is.EqualTo("test-account"), "Account is incorrect");

            var expectedFiles = args.EndsWith("file2") ? new[] {"file1", "file2"} : new[] {"file1"};
            Assert.That(commands[0].Files, Is.EqualTo(expectedFiles), "Files are incorrect");
        });
    }
}