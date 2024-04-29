using System.Data.Common;

namespace shared.Model;

public class PN : Ordination
{
    public double antalEnheder { get; set; }
    public List<Dato> dates { get; set; } = new List<Dato>();

    public PN(DateTime startDen, DateTime slutDen, double antalEnheder, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen)
    {
        this.antalEnheder = antalEnheder;
    }

    public PN() : base(null!, new DateTime(), new DateTime())
    {
    }

    /// <summary>
    /// Registrerer at der er givet en dosis på dagen givesDen
    /// Returnerer true hvis givesDen er inden for ordinationens gyldighedsperiode og datoen huskes
    /// Returner false ellers og datoen givesDen ignoreres
    /// </summary>
    public bool givDosis(Dato givesDen)
    {
        // TODO: Implement!

        if (givesDen.dato >= startDen && givesDen.dato <= slutDen)
        {
            dates.Add(givesDen);

            return true;
        }

        return false;
    }

    /**
    (antal gange ordinationen er anvendt * antal enheder) / (antal dage mellem første og sidste givning)
    */

    public override double doegnDosis()
    {
        // Tjekker om der ikke er nogen givninger
        if (dates.Count == 0)
        {
            return 0;
        }

        // Finder minimums- og maksimumsdatoen blandt givningerne
        DateTime min = dates[0].dato;
        DateTime max = dates[0].dato;

        foreach (Dato dato in dates)
        {
            if (dato.dato < min)
            {
                min = dato.dato;
            }
            if (dato.dato > max)
            {
                max = dato.dato;
            }
        }

        // Beregner det totale antal hele dage fra den første til den sidste dosis
        // Tilføjer 1 for at inkludere både start og slut datoer, hvis forskellige
        int totalDage = (max - min).Days + 1;

        // Antager at 'antalEnheder' repræsenterer antallet af enheder givet ved hver administration
        double samletGangeGivet = dates.Count;
        double samletDosis = samletGangeGivet * antalEnheder;

        // Beregner og returnerer den gennemsnitlige daglige dosis
        return samletDosis / totalDage;
    }


    public override double samletDosis()
    {
        return dates.Count() * antalEnheder;
    }

    public int getAntalGangeGivet()
    {
        return dates.Count();
    }

    public override String getType()
    {
        return "PN";
    }
}
