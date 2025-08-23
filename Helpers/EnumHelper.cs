using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Helpers
{
    public static class EnumHelper
    {
        public static List<SelectListItem> GetEnumSelectList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new SelectListItem
                {
                    Value = Convert.ToInt32(e).ToString(),
                    Text = e.GetType()
                             .GetMember(e.ToString())
                             .First()
                             .GetCustomAttribute<DisplayAttribute>()?.Name ?? e.ToString()
                })
                .ToList();
        }
        public static string GetDisplayName<TEnum>(TEnum enumValue) where TEnum : Enum
        {
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

            if (memberInfo != null)
            {
                var displayAttr = memberInfo.GetCustomAttribute<DisplayAttribute>();
                if (displayAttr != null)
                {
                    return displayAttr.Name;
                }
            }

            return enumValue.ToString(); // fallback
        }
    }
}
