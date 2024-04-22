namespace shared.Model;

public class PN : Ordination {
	public double antalEnheder { get; set; }
    public List<Dato> dates { get; set; } = new List<Dato>();

    public PN (DateTime startDen, DateTime slutDen, double antalEnheder, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen) {
		this.antalEnheder = antalEnheder;
	}

    public PN() : base(null!, new DateTime(), new DateTime()) {
    }

    /// <summary>
    /// Registrerer at der er givet en dosis på dagen givesDen
    /// Returnerer true hvis givesDen er inden for ordinationens gyldighedsperiode og datoen huskes
    /// Returner false ellers og datoen givesDen ignoreres
    /// </summary>
    public bool givDosis(Dato givesDen) {
        // TODO: Implement!

        if (givesDen.dato >= startDen && givesDen.dato <= slutDen) {
            dates.Add(givesDen);
            return true;
        }

        return false;
    }

    /**
    (antal gange ordinationen er anvendt * antal enheder) / (antal dage mellem første og sidste givning)
    */

    public override double doegnDosis() {
    	// TODO: Implement!

        // find min og max i dates i et loop
        if (dates.Count() > 0) {
            DateTime min = dates[0].dato;
            DateTime max = dates[0].dato;

            foreach (Dato dato in dates) {
                if (dato.dato < min) {
                    min = dato.dato;
                }

                if (dato.dato > max) {
                    max = dato.dato;
                }
            }

            TimeSpan span = max - min;
            return (dates.Count() * antalEnheder) / span.TotalDays + 1;
        }

        // hvis der ikke er nogen givninger

        return 0;
    }

    public override double samletDosis() {
        return dates.Count() * antalEnheder;
    }

    public int getAntalGangeGivet() {
        return dates.Count();
    }

	public override String getType() {
		return "PN";
	}
}
