namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]

public class UnitTest
{
    // Test af DagligFast.doegnDosis
    [TestMethod]
    public void DagligFast_doegnDosis_TC1()
    {
        // Initialisere DagligFast objekt
        var dagligFast = new DagligFast(new DateTime(2023, 1, 1), new DateTime(2023, 1, 1), null, 10, 10, 10, 10);

        // Beregner daglis dosis, med metoden doegnDosis    
        double result = dagligFast.doegnDosis();

        // Kontroller output stemmer overens med forventede værdi
        Assert.AreEqual(40, result);
    }

    [TestMethod]
    public void DagligFast_doegnDosis_TC2()
    {
        // Initialisere DagligFast objekt
        var dagligFast = new DagligFast(new DateTime(2023, 1, 1), new DateTime(2023, 1, 1), null, 5, 8, 5, 0);

        // Beregner daglis dosis, med metoden doegnDosis    
        double result = dagligFast.doegnDosis();

        // Kontroller output stemmer overens med forventede værdi
        Assert.AreEqual(18, result);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DagligFast_doegnDosis_TC3()
    {
        // Initialisere DagligFast objekt
        var dagligFast = new DagligFast(new DateTime(2023, 1, 1), new DateTime(2023, 1, 1), null, 0, 0, 0, 0);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DagligFast_doegnDosis_TC4()
    {
        // Initialisere DagligFast objekt
        var dagligFast = new DagligFast(new DateTime(2023, 1, 1), new DateTime(2023, 1, 1), null, -5, 0, 5, 5);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DagligFast_doegnDosis_TC5()
    {
        // Initialisere DagligFast objekt
        var dagligFast = new DagligFast(new DateTime(2023, 1, 1), new DateTime(2023, 1, 1), null, -5, -5, -5, 5);
    }

    // Test af DagligSkæv.doegnDosis
    // TODO

    // Test af PN.doegnDosis
    [TestMethod]
    public void PN_doegnDosis_TC1()
    {
        // Arrange
        PN pn = new PN { dates = new List<Dato>(), antalEnheder = 1 };

        // Act
        double result = pn.doegnDosis();

        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void PN_doegnDosis_TC2()
    {
        // Arrange
        PN pn = new PN
        {
            dates = new List<Dato> { new Dato { dato = DateTime.Today }, new Dato { dato = DateTime.Today } },
            antalEnheder = 1
        };

        // Act
        double result = pn.doegnDosis();

        // Assert
        Assert.AreEqual(2, result);  // da der er to doser på en dag, og antal enheder er 1 pr. dosis
    }

    [TestMethod]
    public void PN_doegnDosis_TC3()
    {
        // Arrange
        PN pn = new PN
        {
            dates = new List<Dato>
            {
                new Dato { dato = DateTime.Today },
                new Dato { dato = DateTime.Today.AddDays(1) }
            },
            antalEnheder = 1
        };

        // Act
        double result = pn.doegnDosis();

        // Assert
        Assert.AreEqual(1, result);  // 2 doser over 2 dage giver 1 enhed per dag
    }
}
