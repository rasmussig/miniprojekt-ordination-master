namespace shared.Model;

using System.Xml.Schema;
using static shared.Util;

public class DagligFast : Ordination
{

	public Dosis MorgenDosis { get; set; } = new Dosis();
	public Dosis MiddagDosis { get; set; } = new Dosis();
	public Dosis AftenDosis { get; set; } = new Dosis();
	public Dosis NatDosis { get; set; } = new Dosis();

	public DagligFast(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel, double morgenAntal, double middagAntal, double aftenAntal, double natAntal) : base(laegemiddel, startDen, slutDen)
	{
		// Tjekker om dosis værdier er negative
		if (morgenAntal < 0 || middagAntal < 0 || aftenAntal < 0 || natAntal < 0)
			throw new ArgumentException("Dosis værdier må ikke være negative.");
		// Tjekker om mindst en dosis er større end 0
		if (morgenAntal == 0 && middagAntal == 0 && aftenAntal == 0 && natAntal == 0)
			throw new ArgumentException("Mindst en dosis skal være større end 0.");

		MorgenDosis = new Dosis(CreateTimeOnly(6, 0, 0), morgenAntal);
		MiddagDosis = new Dosis(CreateTimeOnly(12, 0, 0), middagAntal);
		AftenDosis = new Dosis(CreateTimeOnly(18, 0, 0), aftenAntal);
		NatDosis = new Dosis(CreateTimeOnly(23, 59, 0), natAntal);
	}

	public DagligFast() : base(null!, new DateTime(), new DateTime())
	{
	}

	public override double samletDosis()
	{

		return base.antalDage() * doegnDosis();
	}

	public override double doegnDosis()
	{
		// TODO: Implement!
		return MorgenDosis.antal + MiddagDosis.antal + AftenDosis.antal + NatDosis.antal;
	}

	public Dosis[] getDoser()
	{
		Dosis[] doser = { MorgenDosis, MiddagDosis, AftenDosis, NatDosis };
		return doser;
	}

	public override String getType()
	{
		return "DagligFast";
	}
}
