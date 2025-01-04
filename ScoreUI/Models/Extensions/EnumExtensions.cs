using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ScoreUI.Models.Extensions;

public static class EnumExtensions
{
	public static string DisplayName(this Enum item)
	{
		var type = item.GetType();

		if (type.GetMember(item.ToString()).FirstOrDefault() is { } member)
		{
			if (member.GetCustomAttributes<DisplayAttribute>().FirstOrDefault() is { } attr)
			{
				return attr.GetName() ?? item.ToString();
			}
		}

		return item.ToString();
	}
}