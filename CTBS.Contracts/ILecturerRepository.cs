using CTBS.Entities.Models;
using CTBS.Entities.RequestFeatures;

namespace CTBS.Contracts;

public interface ILecturerRepository
{
	Task<PagedList<Lecturer>> GetAllLecturersAsync(RequestParameters requestParameters, bool trackChanges);
	Task<Lecturer?> GetLecturerAsync(int lecturerId, bool trackChanges);
	void CreateLecturer(Lecturer lecturer);
	void DeleteLecturer(Lecturer lecturer);
}