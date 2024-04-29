namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]

    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }

    // Test af oprettelse af en ny DagligFast
    [TestMethod]
    public void OpretDagligFast_TC1()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            10, 10, 10, 10, new DateTime(2024, 1, 1), new DateTime(2024, 1, 1));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }

    [TestMethod]
    public void OpretDagligFast_TC2()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            0, 8, 0, 0, new DateTime(2024, 1, 1), new DateTime(2024, 1, 1));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void OpretDagligFast_TC3()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            0, 0, 0, 0, new DateTime(2024, 1, 1), new DateTime(2024, 1, 1));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void OpretDagligFast_TC4()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 0, -6, 3, new DateTime(2024, 1, 1), new DateTime(2024, 1, 1));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void OpretDagligFast_TC5()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            10, 10, 10, 10, new DateTime(2024, 1, 3), new DateTime(2024, 1, 1));
    }

    // Test af oprettelse af en ny DagligSkæv
    [TestMethod]
    public void OpretDagligSkæv_TC1()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligSkæve().Count());

        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            new Dosis[] { new Dosis(new DateTime(2024,1,1,6,0,0), 4),
                          new Dosis(new DateTime(2024,1,1,12,0,0), 8),
                          },
            new DateTime(2024, 1, 1), new DateTime(2024, 1, 1));

        Assert.AreEqual(2, service.GetDagligSkæve().Count());
    }

    [TestMethod]
    public void OpretDagligSkæv_TC2()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligSkæve().Count());

        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            new Dosis[] { new Dosis(new DateTime(2024, 1, 1, 12, 0, 0), 10) },
            new DateTime(2024, 1, 1), new DateTime(2024, 1, 1));

        Assert.AreEqual(2, service.GetDagligSkæve().Count());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void OpretDagligSkæv_TC3()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            new Dosis[] { },
            new DateTime(2024, 1, 1), new DateTime(2024, 1, 1));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void OpretDagligSkæv_TC4()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            new Dosis[] {
                new Dosis(new DateTime(2024,1,1,8,0,0), 3),
                new Dosis(new DateTime(2024,1,1,12,0,0), -4),
                new Dosis(new DateTime(2024,1,1,18,0,0), 2),
                },
            new DateTime(2024, 1, 3), new DateTime(2024, 1, 1));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void OpretDagligSkæv_TC5()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            new Dosis[] {
                new Dosis(new DateTime(2024,1,1,8,0,0), 3),
                },
            new DateTime(2024, 1, 3), new DateTime(2024, 1, 1));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAtKodenSmiderEnException()
    {
        // Herunder skal man så kalde noget kode,
        // der smider en exception.

        // Hvis koden _ikke_ smider en exception,
        // så fejler testen.

        Console.WriteLine("Her kommer der ikke en exception. Testen fejler.");
    }
}