//namespace Infrastructure.Persistence.Extenstions
//{
//	public static class EnumExtension
//	{
//		public static IEnumerable<SelectListItem> GetEnumSelectList<T>()
//		{
//			return Enum.GetValues(typeof(T)).Cast<int>()
//				.Select(e => new SelectListItem()
//				{
//					Text = Enum.GetName(typeof(T), e),
//					Value = e.ToString()
//				}).ToList();
//		}
//	}
//}
