using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTBS.Entities.Models;

public class QuestionsCategory
{
	public int Id { get; set; }
	public string Name { get; set; }
	public int ImpactOnAmountOfTime { get; set; }

	public ICollection<Appointment> Appointments { get; set; }
}
