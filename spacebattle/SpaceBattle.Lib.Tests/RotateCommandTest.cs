namespace SpaceBattle.Lib.Tests;

public class RotateCommandTest
{
    [Fact]
    public void TheGameObjectCanRotateAroundItsOwnAxis() 
    {
        var rotatable = new Mock<IRotatable>();

        rotatable.SetupGet(t => t.Position).Returns(new Rotate_Vector(360, 45)).Verifiable();
        rotatable.SetupGet(t => t.Velocity).Returns(new Rotate_Vector(360, 90)).Verifiable();

        ICommand rotateCommand = new RotateCommand(rotatable.Object);

        rotateCommand.Execute();

        rotatable.Verify();
        rotatable.VerifySet(t => t.Position = new Rotate_Vector(135), Times.Once);

        rotatable.VerifyAll();
    }

    [Fact]
    public void TheAngleOfGameObjectCanNotBeDefined()
    {
        var rotatable = new Mock<IRotatable>();

        rotatable.SetupGet(t => t.Position).Throws(() => new Exception()).Verifiable();
        rotatable.SetupGet(t => t.Velocity).Returns(new Rotate_Vector(360, 100)).Verifiable();

        ICommand rotateCommand = new RotateCommand(rotatable.Object);

        Assert.Throws<Exception>(() => rotateCommand.Execute());
    }

    [Fact]
    public void TheVelocityOfGameObjectCanNotBeDefined()
    {
        var rotatable = new Mock<IRotatable>();

        rotatable.SetupGet(t => t.Position).Returns(new Rotate_Vector(360, 45)).Verifiable();
        rotatable.SetupGet(t => t.Velocity).Throws(() => new Exception()).Verifiable();

        ICommand rotateCommand = new RotateCommand(rotatable.Object);

        Assert.Throws<Exception>(() => rotateCommand.Execute());
    }

    [Fact]
    public void TheGameObjectCanNotRotateAroundItsOwnAxis()
    {
        var rotatable = new Mock<IRotatable>();

        rotatable.SetupGet(t => t.Position).Returns(new Rotate_Vector(360, 120)).Verifiable();
        rotatable.SetupGet(t => t.Velocity).Returns(new Rotate_Vector(360, 90)).Verifiable();
        rotatable.SetupSet(t => t.Position = It.IsAny<Rotate_Vector>()).Throws(() => new Exception()).Verifiable();

        ICommand rotateCommand = new RotateCommand(rotatable.Object);

        Assert.Throws<Exception>(() => rotateCommand.Execute());
    }
}
