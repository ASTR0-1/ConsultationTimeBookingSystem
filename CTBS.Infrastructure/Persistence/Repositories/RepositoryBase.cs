using System.Linq.Expressions;
using CTBS.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Infrastructure.Persistence.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
	protected ApplicationContext ApplicationContext;

	public RepositoryBase(ApplicationContext applicationContext)
	{
		ApplicationContext = applicationContext;
	}

	public IQueryable<T> FindAll(bool trackChanges)
	{
		return !trackChanges
			? ApplicationContext.Set<T>()
				.AsNoTracking()
			: ApplicationContext.Set<T>();
	}

	public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
	{
		return !trackChanges
			? ApplicationContext.Set<T>()
				.Where(expression)
				.AsNoTracking()
			: ApplicationContext.Set<T>()
				.Where(expression);
	}

	public void Create(T entity)
	{
		ApplicationContext.Set<T>().Add(entity);
	}

	public void Update(T entity)
	{
		ApplicationContext.Set<T>().Update(entity);
	}

	public void Delete(T entity)
	{
		ApplicationContext.Set<T>().Remove(entity);
	}
}