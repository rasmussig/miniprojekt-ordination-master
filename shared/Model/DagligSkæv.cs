namespace shared.Model;

public class DagligSkæv : Ordination
{
	public List<Dosis> doser { get; set; } = new List<Dosis>();

	public DagligSkæv(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen)
	{
	}

	public DagligSkæv(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel, Dosis[] doser) : base(laegemiddel, startDen, slutDen)
	{
		// Validering af input
		if (doser.Length == 0)
			throw new ArgumentException("DagligSkæv skal have mindst en dosis.");

		this.doser = doser.ToList();

	}

	public DagligSkæv() : base(null!, new DateTime(), new DateTime())
	{
	}

	public void opretDosis(DateTime tid, double antal)
	{
		// Validering af input
		if (antal < 0)
			throw new ArgumentException("Antal doser må ikke være negativt.");

		doser.Add(new Dosis(tid, antal));
	}

	public override double samletDosis()
	{
		return base.antalDage() * doegnDosis();
	}

	public override double doegnDosis()
	{
		// TODO: Implement!
		// Kan laves med en LINQ-forespørgsel: return doser.Sum(dosis => dosis.antal);
		double sum = 0;
		foreach (Dosis dosis in doser)
		{
			sum += dosis.antal;
		}
		return sum;
	}

	public override String getType()
	{
		return "DagligSkæv";
	}
}
