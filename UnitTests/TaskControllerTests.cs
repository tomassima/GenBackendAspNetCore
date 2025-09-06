using Database;
using DemoServer.Controllers;
using Models;
using Validations;

namespace UnitTests;

[TestClass]
public class TaskControllerTests
{
    [TestMethod]
    public void TestGET()
    {
        var db = new MockDatabase<TaskEntity>();
        var validation = new TaskEntityValidation();
        var mockLogger = new MockLogger<TaskController>();
        var controller = new TaskController(db, validation, mockLogger);

        var t = new TaskEntity
        {
            Key = Guid.NewGuid(),
            Name = "Test",
            Priority = 1,
            Status = Status.InProgress
        };

        db.AddOrUpdate(t);

        var getResult = controller.Get();
        Assert.IsNotNull(getResult);
        Assert.IsNotNull(getResult.Value);
        Assert.AreEqual(1, getResult.Value.Length);
        var tresult = getResult.Value[0];
        Assert.IsNotNull(tresult);
        Assert.AreEqual(t.Key, tresult.Key);
        Assert.AreEqual(t.Name, tresult.Name);
        Assert.AreEqual(t.Priority, tresult.Priority);
        Assert.AreEqual(t.Status, tresult.Status);
    }

    [TestMethod]
    public void TestPOSTAdd()
    {
        var db = new MockDatabase<TaskEntity>();
        var validation = new TaskEntityValidation();
        var mockLogger = new MockLogger<TaskController>();
        var controller = new TaskController(db, validation, mockLogger);

        var t = new TaskEntity
        {
            Key = Guid.NewGuid(),
            Name = "Test",
            Priority = 1,
            Status = Status.InProgress
        };

        controller.Post(t);

        var tresult = db.GetValues().Single();

        Assert.IsNotNull(tresult);
        Assert.AreEqual(t.Key, tresult.Key);
        Assert.AreEqual(t.Name, tresult.Name);
        Assert.AreEqual(t.Priority, tresult.Priority);
        Assert.AreEqual(t.Status, tresult.Status);
    }

    [TestMethod]
    public void TestPOSTUpdate()
    {
        var db = new MockDatabase<TaskEntity>();
        var validation = new TaskEntityValidation();
        var mockLogger = new MockLogger<TaskController>();
        var controller = new TaskController(db, validation, mockLogger);

        var t0 = new TaskEntity
        {
            Key = Guid.NewGuid(),
            Name = "Test",
            Priority = 1,
            Status = Status.InProgress
        };

        db.AddOrUpdate(t0);

        var t = new TaskEntity
        {
            Key = t0.Key,
            Name = "Test2",
            Priority = 2,
            Status = Status.Completed
        };

        controller.Post(t);

        var tresult = db.GetValues().Single();

        Assert.IsNotNull(tresult);
        Assert.AreEqual(t.Key, tresult.Key);
        Assert.AreEqual(t.Name, tresult.Name);
        Assert.AreEqual(t.Priority, tresult.Priority);
        Assert.AreEqual(t.Status, tresult.Status);
    }

    [TestMethod]
    public void TestDELETE()
    {
        var db = new MockDatabase<TaskEntity>();
        var validation = new TaskEntityValidation();
        var mockLogger = new MockLogger<TaskController>();
        var controller = new TaskController(db, validation, mockLogger);

        var t = new TaskEntity
        {
            Key = Guid.NewGuid(),
            Name = "Test",
            Priority = 1,
            Status = Status.InProgress
        };

        db.AddOrUpdate(t);

        controller.Delete(t.Key);

        Assert.AreEqual(0, db.GetValues().Count());
        Assert.AreEqual(1, mockLogger.LogMessages.Count);
        Assert.IsTrue(mockLogger.LogMessages[0].Contains($"Task {t.Key} deleted successfully"));
    }
}