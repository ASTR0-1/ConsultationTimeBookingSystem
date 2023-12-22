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

		CreateMap<CreateAppointmentDto, Appointment>();

		CreateMap<CreateQuestionsCategoryDto, QuestionsCategory>();

		CreateMap<GetUserDto, User>().ReverseMap();
	}
}
