﻿namespace CTBS.Entities.Models;

public class Subject
{
	public int Id { get; set; }
	public string Name { get; set; }

	public ICollection<Lecturer> Lecturers { get; set; }
}
