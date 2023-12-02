using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTBS.Entities.RequestFeatures;

public class RequestParameters
{
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; }
	public string OrderBy { get; set; }
}
