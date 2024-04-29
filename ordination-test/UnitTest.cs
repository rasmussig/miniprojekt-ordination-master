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

    // Test af DagligFast.samletDosis
    [TestMethod]
    public void DagligFast_samletDosis_TC1()
    {
        // Initialisere DagligFast objekt
        var dagligFast = new DagligFast(new DateTime(2023, 1, 1), new DateTime(2023, 1, 1), null, 10, 10, 10, 10);

        // Beregner samlet dosis, med metoden samletDosis    
        double result = dagligFast.samletDosis();

        // Kontroller output stemmer overens med forventede værdi
        Assert.AreEqual(40, result);
    }

    [TestMethod]
    public void DagligFast_samletDosis_TC2()
    {
        // Initialisere DagligFast objekt
        var dagligFast = new DagligFast(new DateTime(2023, 1, 1), new DateTime(2023, 1, 3), null, 5, 5, 5, 5);

        // Beregner samlet dosis, med metoden samletDosis    
        double result = dagligFast.samletDosis();

        // Kontroller output stemmer overens med forventede værdi
        Assert.AreEqual(60, result);
    }
    
    // Test af DagligSkæv.doegnDosis
    [TestMethod]
    public void DagligSkæv_doegnDosis_TC1()
    {
        // Iniatialiserer parametrene for en DagligSkæv ordination.
        DateTime startDen = new DateTime(2024, 1, 1);
        DateTime slutDen = new DateTime(2024, 1, 1);
        Laegemiddel laegemiddel = null;

        // Opretter et dosis-array 
        var doser = new Dosis[] { new Dosis(new DateTime(2024, 1, 1, 8, 0, 0), 2) };

        // Instantierer DagligSkæv klassen med de definerede parametre.
        var dagligSkæv = new DagligSkæv(startDen, slutDen, laegemiddel, doser);

        // Beregner den daglige dosis ved hjælp af doegnDosis metoden.
        double actualDosis = dagligSkæv.doegnDosis();

        // Bekræfter, at den beregnede daglige dosis er korrekt
        Assert.AreEqual(2, actualDosis);
    }

    [TestMethod]
    public void DagligSkæv_doegnDosis_TC2()
    {
        // Iniatialiserer parametrene for en DagligSkæv ordination.
        DateTime startDen = new DateTime(2024, 1, 1);
        DateTime slutDen = new DateTime(2024, 1, 4);
        Laegemiddel laegemiddel = null;

        // Opretter et dosis-array
        var doser = new Dosis[] { new Dosis(new DateTime(2024, 1, 1, 8, 0, 0), 2) };

        // Instantierer DagligSkæv klassen med de definerede parametre.
        var dagligSkæv = new DagligSkæv(startDen, slutDen, laegemiddel, doser);

        // Beregner den daglige dosis ved hjælp af doegnDosis metoden.
        double actualDosis = dagligSkæv.doegnDosis();

        // Bekræfter, at den beregnede daglige dosis er korrekt
        Assert.AreEqual(2, actualDosis);
    }

    [TestMethod]
    public void DagligSkæv_doegnDosis_TC3()
    {
        // Iniatialiserer parametrene for en DagligSkæv ordination.
        DateTime startDen = new DateTime(2024, 1, 1);
        DateTime slutDen = new DateTime(2024, 1, 1);
        Laegemiddel laegemiddel = null;

        // Opretter et dosis-array med flere doser
        var doser = new Dosis[] {
        new Dosis(new DateTime(2024, 1, 1, 8, 0, 0), 3),
        new Dosis(new DateTime(2024, 1, 1, 12, 0, 0), 4),
        new Dosis(new DateTime(2024, 1, 1, 18, 0, 0), 2)};

        // Instantierer DagligSkæv klassen med de definerede parametre.
        var dagligSkæv = new DagligSkæv(startDen, slutDen, laegemiddel, doser);

        // Beregner den daglige dosis ved hjælp af doegnDosis metoden.
        double actualDosis = dagligSkæv.doegnDosis();

        // Bekræfter, at den beregnede daglige dosis er korrekt
        Assert.AreEqual(9, actualDosis);
    }

    [TestMethod]
    public void DagligSkæv_doegnDosis_TC4()
    {
        // Iniatialiserer parametrene for en DagligSkæv ordination.
        DateTime startDen = new DateTime(2024, 1, 1);
        DateTime slutDen = new DateTime(2024, 1, 5);
        Laegemiddel laegemiddel = null;

        // Opretter et dosis-array med flere doser
        var doser = new Dosis[] {
        new Dosis(new DateTime(2024, 1, 1, 8, 0, 0), 3),
        new Dosis(new DateTime(2024, 1, 1, 12, 0, 0), 4),
        new Dosis(new DateTime(2024, 1, 1, 18, 0, 0), 2)};

        // Instantierer DagligSkæv klassen med de definerede parametre.
        var dagligSkæv = new DagligSkæv(startDen, slutDen, laegemiddel, doser);

        // Beregner den daglige dosis ved hjælp af doegnDosis metoden.
        double actualDosis = dagligSkæv.doegnDosis();

        // Bekræfter, at den beregnede daglige dosis er korrekt
        Assert.AreEqual(9, actualDosis);
    }

    // Test af DagligSkæv.samletDosis
    [TestMethod]
    public void DagligSkæv_samletDosis_TC1()
    {
        // Iniatialiserer parametrene for en DagligSkæv ordination.
        DateTime startDen = new DateTime(2024, 1, 1);
        DateTime slutDen = new DateTime(2024, 1, 1);
        Laegemiddel laegemiddel = null;

        // Opretter et dosis-array
        var doser = new Dosis[] { new Dosis(new DateTime(2024, 1, 1, 8, 0, 0), 2) };

        // Instantierer DagligSkæv klassen med de definerede parametre.
        var dagligSkæv = new DagligSkæv(startDen, slutDen, laegemiddel, doser);

        // Beregner den samlede dosis ved hjælp af samletDosis metoden.
        double actualDosis = dagligSkæv.samletDosis();

        // Bekræfter, at den beregnede samlede dosis er korrekt
        Assert.AreEqual(2, actualDosis);
    }

    [TestMethod]
    public void DagligSkæv_samletDosis_TC2()
    {
        // Iniatialiserer parametrene for en DagligSkæv ordination.
        DateTime startDen = new DateTime(2024, 1, 1);
        DateTime slutDen = new DateTime(2024, 1, 4);
        Laegemiddel laegemiddel = null;

        // Opretter et dosis-array
        var doser = new Dosis[] { new Dosis(new DateTime(2024, 1, 1, 8, 0, 0), 2) };

        // Instantierer DagligSkæv klassen med de definerede parametre.
        var dagligSkæv = new DagligSkæv(startDen, slutDen, laegemiddel, doser);

        // Beregner den samlede dosis ved hjælp af samletDosis metoden.
        double actualDosis = dagligSkæv.samletDosis();

        // Bekræfter, at den beregnede samlede dosis er korrekt
        Assert.AreEqual(8, actualDosis);
    }

    [TestMethod]
    public void DagligSkæv_samletDosis_TC3()
    {
        // Iniatialiserer parametrene for en DagligSkæv ordination.
        DateTime startDen = new DateTime(2024, 1, 1);
        DateTime slutDen = new DateTime(2024, 1, 1);
        Laegemiddel laegemiddel = null;

        // Opretter et dosis-array med flere doser
        var doser = new Dosis[] {
        new Dosis(new DateTime(2024, 1, 1, 8, 0, 0), 3),
        new Dosis(new DateTime(2024, 1, 1, 12, 0, 0), 4),
        new Dosis(new DateTime(2024, 1, 1, 18, 0, 0), 2)};

        // Instantierer DagligSkæv klassen med de definerede parametre.
        var dagligSkæv = new DagligSkæv(startDen, slutDen, laegemiddel, doser);

        // Beregner den samlede dosis ved hjælp af samletDosis metoden.
        double actualDosis = dagligSkæv.samletDosis();

        // Bekræfter, at den beregnede samlede dosis er korrekt
        Assert.AreEqual(9, actualDosis);
    }

    [TestMethod]
    public void DagligSkæv_samletDosis_TC4()
    {
        // Iniatialiserer parametrene for en DagligSkæv ordination.
        DateTime startDen = new DateTime(2024, 1, 1);
        DateTime slutDen = new DateTime(2024, 1, 5);
        Laegemiddel laegemiddel = null;

        // Opretter et dosis-array med flere doser
        var doser = new Dosis[] {
        new Dosis(new DateTime(2024, 1, 1, 8, 0, 0), 3),
        new Dosis(new DateTime(2024, 1, 1, 12, 0, 0), 4),
        new Dosis(new DateTime(2024, 1, 1, 18, 0, 0), 2)};

        // Instantierer DagligSkæv klassen med de definerede parametre.
        var dagligSkæv = new DagligSkæv(startDen, slutDen, laegemiddel, doser);

        // Beregner den samlede dosis ved hjælp af samletDosis metoden.
        double actualDosis = dagligSkæv.samletDosis();

        // Bekræfter, at den beregnede samlede dosis er korrekt
        Assert.AreEqual(45, actualDosis);
    }

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
