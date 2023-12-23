using AutoMapper;
using CTBS.Entities.DataTransferObjects.Appointment;
using CTBS.Entities.DataTransferObjects.Authentication;
using CTBS.Entities.DataTransferObjects.QuestionsCategory;
using CTBS.Entities.DataTransferObjects.User;
using CTBS.Entities.Models;

namespace CTBS.Entities.Mappings;

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
