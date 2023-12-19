using AutoMapper;
using CTBS.Entities.DataTransferObjects.Appointment;
using CTBS.Entities.DataTransferObjects.Authentication;
using CTBS.Entities.DataTransferObjects.QuestionsCategory;
using CTBS.Entities.DataTransferObjects.Subject;
using CTBS.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace CTBS.Entities.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<UserForRegistrationDto, User>();

		CreateMap<CreateAppointmentDto, Appointment>();

		CreateMap<CreateQuestionsCategoryDto, QuestionsCategory>();

		CreateMap<CreateSubjectDto, Subject>();
	}
}
