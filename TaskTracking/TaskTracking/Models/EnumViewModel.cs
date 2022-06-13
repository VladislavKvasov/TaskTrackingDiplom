using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracking.Models
{
    public class EnumViewModel
    {
        public int Value { get; set; }

        public string DisplayMember { get; set; }

        static public List<EnumViewModel> convertToEnumViewModel<T>(List<T> list)
        {

            return list.Select(x => new EnumViewModel { Value = Convert.ToInt32(x), DisplayMember = EnumDisplayName(x as Enum) } ).ToList();
        }

        static private string EnumDisplayName(Enum item)
        {
            var type = item.GetType();
            var member = type.GetMember(item.ToString());
            DisplayAttribute displayName = (DisplayAttribute)member[0].GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

            if (displayName != null)
            {
                return displayName.Name;
            }

            return item.ToString();
        }
    }
}
