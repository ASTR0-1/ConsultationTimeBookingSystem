using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTBS.Entities.DataTransferObjects.QuestionsCategory;

public class GetQuestionsCategoryDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public float ImpactOnAmountOfTime { get; set; }
}
