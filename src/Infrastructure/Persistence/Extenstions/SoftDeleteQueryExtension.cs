using Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;

using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Persistence.Extenstions
{
	public static class SoftDeleteQueryExtension
	{
		public static void AddSoftDeleteQueryFilter(this IMutableEntityType entityData)
		{
			var methodToCall = typeof(SoftDeleteQueryExtension)
				.GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
				.MakeGenericMethod(entityData.ClrType);

			var filter = methodToCall.Invoke(null, new object[] { });

			entityData.SetQueryFilter((LambdaExpression)filter);
			entityData.AddIndex(entityData.FindProperty(nameof(ISoftDeletedEntity.DeletedAt)));
		}

		private static LambdaExpression GetSoftDeleteFilter<TEntity>() where TEntity : class, ISoftDeletedEntity
		{
			Expression<Func<TEntity, bool>> filter = x => !x.DeletedAt.HasValue;
			return filter;
		}
	}
}
