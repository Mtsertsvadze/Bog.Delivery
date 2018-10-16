using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace BogMenu.Models
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }

    public enum CompanyTypeEnum
    {
        [Display(Name = "სწრაფი კვება")]
        FAST_FOOD,
        [Display(Name = "პიცა")]
        PIZZA,
        [Display(Name = "ევროპული სამზარეულო")]
        EUROPEAN,
        [Display(Name = "ქართული სამზარეულო")]
        GEORGIAN
    }

    public enum CompanyCostEnum
    {
        [Display(Name = "დაბალი")]
        LOW,
        [Display(Name = "საშუალო")]
        MIDDLE,
        [Display(Name = "მაღალი")]
        HIGH,
    }

    public enum MenuTypeEnum
    {
        [Display(Name = "ცომეული")]
        BACKED_FOOD,
        [Display(Name = "ცხელი კერძები")]
        HOT_FOOD,
        [Display(Name = "სალათები")]
        SALADS,
        [Display(Name = "სასმელები")]
        DRINKS
    }

    public enum OrderStatusEnum
    {
        [Display(Name = "მომლოდინე")]
        PENDING,
        [Display(Name = "ახალი")]
        NEW,
        [Display(Name = "მიტანილი")]
        DELIVERED,
        [Display(Name = "გაუქმებული")]
        CANCELED,
        [Display(Name = "დასრულებული")]
        FINISHED
    }

    public enum CommentCategoryEnum
    {
        [Display(Name = "")]
        NULL,
        [Display(Name = "მომსახურეობა")]
        SERVICE,
        [Display(Name = "მიტანის დრო")]
        DELIVERY_TIME,
        [Display(Name = "ხარისხი")]
        QUALITY
    }

    public enum StarsEnum
    {
        [Display (Name = "შეფასების გარეშე")]
        NULL,
        [Display(Name = "ძალიან ცუდი")]
        VERY_BAD,
        [Display(Name = "ცუდი")]
        BAD,
        [Display(Name = "არც ისე ცუდი")]
        NOT_BAD,
        [Display(Name = "კარგი")]
        GOOD,
        [Display(Name = "ძალიან კარგი")]
        VERY_GOOD
    }
}