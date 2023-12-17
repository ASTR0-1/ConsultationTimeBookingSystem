using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CTBS.Entities.Enums;

public enum UserType
{
	/// <summary>
	/// User is a Lecturer.
	/// </summary>
	Lecturer,

	/// <summary>
	/// User is a Student.
	/// </summary>
	Student
}