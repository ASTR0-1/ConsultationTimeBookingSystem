using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTBS.Entities.DataTransferObjects.Appointment;

public class GetAppointmentDto
{
	public int Id { get; set; }
	public float Priority { get; set; }
	public float RequestedMinutes { get; set; }
	public string State { get; set; }
	public string LecturerId { get; set; }
	public string StudentId { get; set; }
	public string QuestionsCategoryId { get; set; }
}
