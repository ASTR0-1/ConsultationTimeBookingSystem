using AutoMapper;
using CTBS.Application.DataTransferObjects.Appointment;
using CTBS.Application.DataTransferObjects.Authentication;
using CTBS.Application.DataTransferObjects.QuestionsCategory;
using CTBS.Application.DataTransferObjects.User;
using CTBS.Domain.Models;

namespace CTBS.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<UserForRegistrationDto, User>();

		CreateMap<CreateAppointmentDto, Appointment>()
			.ForMember(a => a.Date,
				opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Date)));

		CreateMap<Appointment, GetAppointmentDto>()
			.ForMember(a => a.State,
				opt => opt.MapFrom(src => src.State.ToString()));

		CreateMap<QuestionsCategory, GetQuestionsCategoryDto>();

		CreateMap<CreateQuestionsCategoryDto, QuestionsCategory>();

		CreateMap<GetUserDto, User>().ReverseMap();
	}
}