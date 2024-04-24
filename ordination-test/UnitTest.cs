namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]

public class UnitTest
{

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