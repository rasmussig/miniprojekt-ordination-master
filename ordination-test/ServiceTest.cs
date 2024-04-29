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
        optionsBuilder.UseInMemoryDatabase(databaseName: $"test-database-{DateTime.UtcNow.Ticks}");
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


    // Test af oprettelse af en ny PN
    [TestMethod]
    public void OpretPN_TC1()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(4, service.GetPNs().Count());

        service.OpretPN(patient.PatientId, lm.LaegemiddelId, 10, new DateTime(2024, 1, 1), new DateTime(2024, 1, 1));

        Assert.AreEqual(5, service.GetPNs().Count());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void OpretPN_TC2()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        service.OpretPN(patient.PatientId, lm.LaegemiddelId, 0, new DateTime(2024, 1, 1), new DateTime(2024, 1, 1));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void OpretPN_TC3()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        service.OpretPN(patient.PatientId, lm.LaegemiddelId, -10, new DateTime(2024, 1, 1), new DateTime(2024, 1, 1));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void OpretPN_TC4()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        service.OpretPN(patient.PatientId, lm.LaegemiddelId, 10, new DateTime(2024, 1, 3), new DateTime(2024, 1, 1));
    }

    // Test af metoden GetAnbefaletDosisPerDøgn
    [TestMethod]
    public void GetAnbefaletDosisPerDøgn_TC1()
    {
        Patient patient = service.GetPatienter().First(); // Jane Jensen
        Laegemiddel lm = service.GetLaegemidler().Last(); // Acetylsaicylsyre

            Assert.AreEqual("Jane Jensen", patient.navn, "Forkert patient hentet.");
            Assert.AreEqual("Acetylsalicylsyre", lm.navn, "Forkert lægemiddel hentet.");

        double result = service.GetAnbefaletDosisPerDøgn(patient.PatientId, lm.LaegemiddelId);

        Assert.AreEqual(63.4 * 0.15, result);
    }

    [TestMethod]
    public void GetAnbefaletDosisPerDøgn_TC2()
    {
        Patient patient = service.GetPatienter().First(); // Jane Jensen
        Laegemiddel lm = service.GetLaegemidler().Last(); // Acetylsalicylsyre
    
                Assert.AreEqual("Jane Jensen", patient.navn, "Forkert patient hentet.");
                Assert.AreEqual("Acetylsaicylsyre", lm.navn, "Forkert lægemiddel hentet.");

        // Sæt Jane Jensens vægt til 20 kg
        patient.vaegt = 20;

        double result = service.GetAnbefaletDosisPerDøgn(patient.PatientId, lm.LaegemiddelId);

        Assert.AreEqual(20 * 0.1, result);
    }

    [TestMethod]
    public void GetAnbefaletDosisPerDøgn_TC3()
    {
        Patient patient = service.GetPatienter().First(); // Jane Jensen
        Laegemiddel lm = service.GetLaegemidler().Last(); // Acetylsalicylsyre

            Assert.AreEqual("Jane Jensen", patient.navn, "Forkert patient hentet.");
            Assert.AreEqual("Acetylsalicylsyre", lm.navn, "Forkert lægemiddel hentet.");

        // Sæt Jane Jensens vægt til 130 kg
        patient.vaegt = 130;

        double result = service.GetAnbefaletDosisPerDøgn(patient.PatientId, lm.LaegemiddelId);

        Assert.AreEqual(130 * 0.16, result);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetAnbefaletDosisPerDøgn_TC4()
    {
        Patient patient = service.GetPatienter().First(); // Jane Jensen

        service.GetAnbefaletDosisPerDøgn(patient.PatientId, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetAnbefaletDosisPerDøgn_TC5()
    {
        Laegemiddel lm = service.GetLaegemidler().Last(); // Acetylsalicylsyre

        service.GetAnbefaletDosisPerDøgn(0, lm.LaegemiddelId);
    }


}