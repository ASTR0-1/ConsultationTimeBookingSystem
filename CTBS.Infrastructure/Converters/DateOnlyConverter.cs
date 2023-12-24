﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CTBS.Infrastructure.Converters;

public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
	public DateOnlyConverter() : base(
		dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
		dateTime => DateOnly.FromDateTime(dateTime))
	{
	}
}