﻿namespace CTBS.Entities.Models;

public class Subject
{
	public Guid Id { get; set; }
	public string Name { get; set; }

	public ICollection<Lecturer> Lecturers { get; set; }
}
