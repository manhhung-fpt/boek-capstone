using Boek.Infrastructure.Attributes;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace Boek.Service.Utils
{
    public static class LinQUtil
    {
        #region Filter
        public static IQueryable<TEntity> DynamicFilter<TEntity>(this IQueryable<TEntity> source, TEntity entity)
        {
            foreach (PropertyInfo property in entity.GetType().GetProperties())
            {
                if (!(entity.GetType().GetProperty(property.Name) == null))
                {
                    object data = entity.GetType().GetProperty(property.Name)
                        ?.GetValue((object)entity, (object[])null);
                    if (data != null &&
                        !property.CustomAttributes.Any(
                            (Func<CustomAttributeData, bool>)(a => a.AttributeType == typeof(SkipAttribute))))
                    {
                        source = DynamicNotRangeFilter(source, property, data);
                        if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(DateRangeAttribute))))
                        {
                            IQueryable<TEntity> source2 = source;
                            string predicate = property.Name + " >= @0 && " + property.Name + " < @1";
                            if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(RangeFieldAttribute))))
                            {
                                var dates = data as List<DateTime?>;
                                if (dates.Any())
                                {
                                    dates.ForEach(d =>
                                    {
                                        var temp = (DateTime)d;
                                        if (temp.TimeOfDay.TotalSeconds == 0)
                                            d = temp.Date;
                                    });
                                    var name = property.Name.Substring(0, property.Name.Length - 1);
                                    if (dates.Count == 1)
                                    {
                                        predicate = name + " >= @0";
                                        source = source2.Where<TEntity>(predicate, (object)dates[0]);
                                    }
                                    if (dates.Count == 2)
                                    {
                                        predicate = name + " >= @0 && " + name + " < @1";
                                        var dateObjects = new object[2]
                                        {
                                            (object) dates[0],
                                            (object) dates[1]
                                        };
                                        source = source2.Where<TEntity>(predicate, dateObjects);
                                    }
                                }
                            }
                            else
                            {
                                DateTime date = (DateTime)data;
                                object[] dateRange = new object[2]
                                {
                                    (object) date.Date,
                                    null
                                };
                                date = date.Date;
                                dateRange[1] = date.AddDays(1.0);
                                source = source2.Where<TEntity>(predicate, dateRange);
                            }
                        }
                        else if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(RangeAttribute))) &&
                                !property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(SkipRangeAttribute))))
                        {
                            source = DynamicRangeFilter(source, property, data);
                        }
                        else if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(ChildRangeAttribute))) &&
                                !property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(SkipRangeAttribute))))
                        {
                            source = DynamicChildRange(source, property, data, null, null, true);
                        }
                    }
                }
            }

            return source;
        }
        public static IQueryable<TEntity> DynamicOtherFilter<TEntity, T>(this IQueryable<TEntity> source, T entity)
        {
            foreach (PropertyInfo property in entity.GetType().GetProperties())
            {
                if (!(entity.GetType().GetProperty(property.Name) == null))
                {
                    object data = entity.GetType().GetProperty(property.Name)
                        ?.GetValue((object)entity, (object[])null);
                    if (data != null &&
                        !property.CustomAttributes.Any(
                            (Func<CustomAttributeData, bool>)(a => a.AttributeType == typeof(SkipAttribute))))
                    {
                        source = DynamicNotRangeFilter(source, property, data);
                        if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(DateRangeAttribute))))
                        {
                            IQueryable<TEntity> source2 = source;
                            string predicate = property.Name + " >= @0 && " + property.Name + " < @1";
                            if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(RangeFieldAttribute))))
                            {
                                var dates = data as List<DateTime?>;
                                if (dates.Any())
                                {
                                    dates.ForEach(d =>
                                    {
                                        var temp = (DateTime)d;
                                        if (temp.TimeOfDay.TotalSeconds == 0)
                                            d = temp.Date;
                                    });
                                    var name = property.Name.Substring(0, property.Name.Length - 1);
                                    if (dates.Count == 1)
                                    {
                                        predicate = name + " >= @0 && ";
                                        source = source2.Where<TEntity>(predicate, (object)dates[0]);
                                    }
                                    if (dates.Count == 2)
                                    {
                                        predicate = name + " >= @0 && " + name + " < @1";
                                        var dateObjects = new object[2]
                                        {
                                            (object) dates[0],
                                            (object) dates[1]
                                        };
                                        source = source2.Where<TEntity>(predicate, dateObjects);
                                    }
                                }
                            }
                            else
                            {
                                DateTime date = (DateTime)data;
                                object[] dateRange = new object[2]
                                {
                                    (object) date.Date,
                                    null
                                };
                                date = date.Date;
                                dateRange[1] = date.AddDays(1.0);
                                source = source2.Where<TEntity>(predicate, dateRange);
                            }
                        }
                        else if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(RangeAttribute))) &&
                                !property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(SkipRangeAttribute))))
                        {
                            source = DynamicRangeFilter(source, property, data);
                        }
                        else if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(ChildRangeAttribute))) &&
                                !property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(SkipRangeAttribute))))
                        {
                            source = DynamicChildRange(source, property, data, null, null, true);
                        }
                    }
                }
            }

            return source;
        }
        #endregion

        #region Not range
        private static IQueryable<TEntity> DynamicNotRangeFilter<TEntity>(this IQueryable<TEntity> source, PropertyInfo property, object data)
        {
            if (data != null &&
                        !property.CustomAttributes.Any(
                            (Func<CustomAttributeData, bool>)(a => a.AttributeType == typeof(SkipAttribute))))
            {
                if (property.CustomAttributes.Any(
                                (Func<CustomAttributeData, bool>)(a => a.AttributeType == typeof(StringAttribute))))
                {
                    source = source.Where<TEntity>(property.Name + ".ToLower().Contains(@0)",
                        (object)data.ToString().ToLower());
                }
                else if (property.CustomAttributes.Any(a => a.AttributeType == typeof(BooleanAttribute)))
                {
                    source = source.Where<TEntity>(property.Name + "== @0", (object)data);
                }
                else if (property.CustomAttributes.Any(a => a.AttributeType == typeof(IntAttribute)))
                {
                    source = source.Where<TEntity>(property.Name + "== @0", (object)data);
                }
                else if (property.CustomAttributes.Any(a => a.AttributeType == typeof(GuidAttribute))
                    && !data.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    source = source.Where<TEntity>(property.Name + "== @0", (object)data);
                }
                else if (property.CustomAttributes.Any(a => a.AttributeType == typeof(ByteAttribute)))
                {
                    source = source.Where<TEntity>(property.Name + "== @0", (object)data);
                }
                else if (property.CustomAttributes.Any(a => a.AttributeType == typeof(DecimalAttribute)))
                {
                    source = source.Where<TEntity>(property.Name + "== @0", (object)data);
                }
                else if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(StartDateAttribute))))
                {
                    DateTime date = (DateTime)data;
                    date = date.TimeOfDay.TotalSeconds == 0 ? date.Date : date;
                    IQueryable<TEntity> source2 = source;
                    source = source2.Where<TEntity>(property.Name + " >= @0", date);
                }
                else if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(EndDateAttribute))))
                {
                    DateTime date = (DateTime)data;
                    date = date.TimeOfDay.TotalSeconds == 0 ? date.Date : date;
                    IQueryable<TEntity> source2 = source;
                    source = source2.Where<TEntity>(property.Name + " <= @0", date);
                }
                else if (property.CustomAttributes.Any(
                             (Func<CustomAttributeData, bool>)(a =>
                                 a.AttributeType == typeof(ChildAttribute))))
                {
                    //DynamicChildFilter(source, property, property.Name);
                    foreach (PropertyInfo propertyChild in property.PropertyType.GetProperties())
                    {
                        object dataChild = data.GetType().GetProperty(propertyChild.Name)
                            ?.GetValue(data, (object[])null);
                        if (dataChild != null)
                        {
                            var AttributeType = propertyChild.CustomAttributes.FirstOrDefault().AttributeType;
                            var entityName = $"{property.Name}.{propertyChild.Name}";
                            if (AttributeType.Equals(typeof(SortAttribute)))
                            {
                                string[] sort = dataChild.ToString().Split(", ");
                                if (sort.Length >= 2)
                                {
                                    string list = "";
                                    for (int i = 0; i < sort.Length; i++)
                                    {
                                        var s = sort[i];
                                        if ((++i) == sort.Length || i == 0)
                                        {
                                            switch (s.ToLower())
                                            {
                                                case "asc":
                                                    list += "";
                                                    break;
                                                case "desc":
                                                    list += " descending";
                                                    break;
                                                default:
                                                    list = $"{property.Name}.{s}";
                                                    break;
                                            }
                                        }
                                        switch (s.ToLower())
                                        {
                                            case "asc":
                                                list += ", ";
                                                break;
                                            case "desc":
                                                list += " descending, ";
                                                break;
                                            default:
                                                list = $"{property.Name}.{s}";
                                                break;
                                        }
                                    }
                                    dataChild = list;
                                }
                            }
                            else if (AttributeType.Equals(typeof(RangeAttribute)))
                            {
                                if (entityName.EndsWith("s") &&
                                !propertyChild.CustomAttributes.Any(
                                (Func<CustomAttributeData, bool>)(a =>
                                 a.AttributeType == typeof(RangeFieldAttribute))))
                                    entityName = entityName.Substring(0, entityName.Length - 1);
                                source = DynamicChildRangeFilter(source, propertyChild, dataChild, entityName);
                            }
                            else
                                source = DynamicChildFilter(source, entityName, dataChild, AttributeType, propertyChild);
                        }
                    }
                }
                else if (property.CustomAttributes.Any(
                             (Func<CustomAttributeData, bool>)(a =>
                                 a.AttributeType == typeof(ExactChildAttribute))))
                {
                    foreach (PropertyInfo propertyChild in property.PropertyType.GetProperties())
                    {
                        object dataChild = data.GetType().GetProperty(propertyChild.Name)
                            ?.GetValue(data, (object[])null);
                        if (dataChild != null)
                        {
                            source = source.Where<TEntity>(string.Format("{0}.{1}=\"{2}\"", property.Name,
                                propertyChild.Name, dataChild));
                        }
                    }
                }
                else if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a => a.AttributeType == typeof(SortAttribute))))
                {
                    string[] sort = data.ToString().Split(", ");
                    if (sort.Length > 2)
                    {
                        string list = "";
                        for (int i = 0; i < sort.Length; i++)
                        {
                            var s = sort[i];
                            int count = i;
                            if ((++count) == sort.Length || i == 0)
                            {
                                switch (s.ToLower())
                                {
                                    case "asc":
                                        list += "";
                                        break;
                                    case "desc":
                                        list += " descending";
                                        break;
                                    default:
                                        list += s;
                                        break;
                                }
                            }
                            else
                            {
                                switch (s.ToLower())
                                {
                                    case "asc":
                                        list += ", ";
                                        break;
                                    case "desc":
                                        list += " descending, ";
                                        break;
                                    default:
                                        list += s;
                                        break;
                                }
                            }
                        }
                        source = source.OrderBy(list.Trim());
                    }
                    else
                        source = source.OrderBy(sort[0]);
                }
            }
            return source;
        }
        private static IQueryable<TEntity> DynamicNotRangeFilter<TEntity>(this IQueryable<TEntity> source, PropertyInfo property, object data, string entityRangeName)
        {
            if (data != null &&
                        !property.CustomAttributes.Any(
                            (Func<CustomAttributeData, bool>)(a => a.AttributeType == typeof(SkipAttribute))))
            {
                if (property.CustomAttributes.Any(
                                (Func<CustomAttributeData, bool>)(a => a.AttributeType == typeof(StringAttribute))))
                {
                    if (entityRangeName.Contains("Equals"))
                        entityRangeName = entityRangeName.Replace("Equals", "ToLower().Contains");
                    source = source.Where<TEntity>(entityRangeName, (object)data.ToString().ToLower());
                }
                else if (property.CustomAttributes.Any(a => a.AttributeType == typeof(BooleanAttribute)))
                {
                    if (!entityRangeName.Contains("Equals"))
                        entityRangeName += "== @0";
                    source = source.Where<TEntity>(entityRangeName, (object)data);
                }
                else if (property.CustomAttributes.Any(a => a.AttributeType == typeof(IntAttribute)))
                {
                    if (!entityRangeName.Contains("Equals"))
                        entityRangeName += "== @0";
                    source = source.Where<TEntity>(entityRangeName, (object)data);
                }
                else if (property.CustomAttributes.Any(a => a.AttributeType == typeof(GuidAttribute))
                    && !data.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    if (!entityRangeName.Contains("Equals"))
                        entityRangeName += "== @0";
                    source = source.Where<TEntity>(entityRangeName, (object)data);
                }
                else if (property.CustomAttributes.Any(a => a.AttributeType == typeof(ByteAttribute)))
                {
                    if (!entityRangeName.Contains("Equals"))
                        entityRangeName += "== @0";
                    source = source.Where<TEntity>(entityRangeName, (object)data);
                }
                else if (property.CustomAttributes.Any(a => a.AttributeType == typeof(DecimalAttribute)))
                {
                    if (!entityRangeName.Contains("Equals"))
                        entityRangeName += "== @0";
                    source = source.Where<TEntity>(entityRangeName, (object)data);
                }
                else if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(StartDateAttribute))))
                {
                    DateTime date = (DateTime)data;
                    date = date.TimeOfDay.TotalSeconds == 0 ? date.Date : date;
                    IQueryable<TEntity> source2 = source;
                    source = source2.Where<TEntity>(property.Name + " >= @0", date);
                }
                else if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(EndDateAttribute))))
                {
                    DateTime date = (DateTime)data;
                    date = date.TimeOfDay.TotalSeconds == 0 ? date.Date : date;
                    IQueryable<TEntity> source2 = source;
                    source = source2.Where<TEntity>(property.Name + " <= @0", date);
                }
                else if (property.CustomAttributes.Any(
                             (Func<CustomAttributeData, bool>)(a =>
                                 a.AttributeType == typeof(ChildAttribute))))
                {
                    //DynamicChildFilter(source, property, property.Name);
                    foreach (PropertyInfo propertyChild in property.PropertyType.GetProperties())
                    {
                        object dataChild = data.GetType().GetProperty(propertyChild.Name)
                            ?.GetValue(data, (object[])null);
                        if (dataChild != null)
                        {
                            var AttributeType = propertyChild.CustomAttributes.FirstOrDefault().AttributeType;
                            var entityName = $"{entityRangeName}.{propertyChild.Name}";
                            if (AttributeType.Equals(typeof(SortAttribute)))
                            {
                                string[] sort = dataChild.ToString().Split(", ");
                                if (sort.Length >= 2)
                                {
                                    string list = "";
                                    for (int i = 0; i < sort.Length; i++)
                                    {
                                        var s = sort[i];
                                        if ((++i) == sort.Length || i == 0)
                                        {
                                            switch (s.ToLower())
                                            {
                                                case "asc":
                                                    list += "";
                                                    break;
                                                case "desc":
                                                    list += " descending";
                                                    break;
                                                default:
                                                    list = $"{entityRangeName}.{s}";
                                                    break;
                                            }
                                        }
                                        switch (s.ToLower())
                                        {
                                            case "asc":
                                                list += ", ";
                                                break;
                                            case "desc":
                                                list += " descending, ";
                                                break;
                                            default:
                                                list = $"{entityRangeName}.{s}";
                                                break;
                                        }
                                    }
                                    dataChild = list;
                                }
                            }
                            else if (AttributeType.Equals(typeof(RangeAttribute)))
                            {
                                entityName = entityName.Substring(0, entityName.Length - 1);
                                source = DynamicChildRangeFilter(source, propertyChild, dataChild, entityName);
                            }
                            else
                                source = DynamicChildFilter(source, entityName, dataChild, AttributeType, propertyChild);
                        }
                    }
                }
                else if (property.CustomAttributes.Any(
                             (Func<CustomAttributeData, bool>)(a =>
                                 a.AttributeType == typeof(ExactChildAttribute))))
                {
                    foreach (PropertyInfo propertyChild in property.PropertyType.GetProperties())
                    {
                        object dataChild = data.GetType().GetProperty(propertyChild.Name)
                            ?.GetValue(data, (object[])null);
                        if (dataChild != null)
                        {
                            source = source.Where<TEntity>(string.Format("{0}.{1}=\"{2}\"", entityRangeName,
                                propertyChild.Name, dataChild));
                        }
                    }
                }
            }
            return source;
        }
        #endregion

        #region Child Filter
        public static IQueryable<TEntity> DynamicChildFilter<TEntity>(this IQueryable<TEntity> source, string entityName, object entityData, Type AttributeType, PropertyInfo property)
        {
            if (!AttributeType.Equals(typeof(SkipAttribute)))
            {
                if (AttributeType.Equals(typeof(StringAttribute)))
                    source = source.Where<TEntity>(entityName + ".ToLower().Contains(@0)", (object)entityData.ToString().ToLower());
                if (AttributeType.Equals(typeof(BooleanAttribute)))
                    source = source.Where<TEntity>(entityName + "== @0", (object)entityData);
                if (AttributeType.Equals(typeof(IntAttribute)))
                    source = source.Where<TEntity>(entityName + "== @0", (object)entityData);
                if (AttributeType.Equals(typeof(GuidAttribute)))
                    source = source.Where<TEntity>(entityName + "== @0", (object)entityData);
                if (AttributeType.Equals(typeof(ByteAttribute)))
                    source = source.Where<TEntity>(entityName + "== @0", (object)entityData);
                if (AttributeType.Equals(typeof(DecimalAttribute)))
                    source = source.Where<TEntity>(entityName + "== @0", (object)entityData);
                if (AttributeType.Equals(typeof(SortAttribute)))
                    source = source.OrderBy((string)entityData);
                if (AttributeType.Equals(typeof(DateRangeAttribute)))
                {
                    DateTime date = (DateTime)entityData;
                    IQueryable<TEntity> source2 = source;
                    string predicate = entityName + " >= @0 && " + entityName + " < @1";
                    object[] dateRange = new object[2]
                    {
                                (object) date.Date,
                                null
                    };
                    date = date.Date;
                    dateRange[1] = date.AddDays(1.0);
                    source = source2.Where<TEntity>(predicate, dateRange);
                }
                else if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(ChildAttribute))))
                {
                    //DynamicChildFilter(source, property, property.Name);
                    foreach (PropertyInfo propertyChild in property.PropertyType.GetProperties())
                    {
                        object dataChild = entityData.GetType().GetProperty(propertyChild.Name)
                            ?.GetValue(entityData, (object[])null);
                        if (dataChild != null)
                        {
                            var AttributeChildType = propertyChild.CustomAttributes.FirstOrDefault().AttributeType;
                            var entityChildName = $"{entityName}.{propertyChild.Name}";
                            if (AttributeChildType.Equals(typeof(SortAttribute)))
                            {
                                string[] sort = dataChild.ToString().Split(", ");
                                if (sort.Length >= 2)
                                {
                                    string list = "";
                                    for (int i = 0; i < sort.Length; i++)
                                    {
                                        var s = sort[i];
                                        if ((++i) == sort.Length || i == 0)
                                        {
                                            switch (s.ToLower())
                                            {
                                                case "asc":
                                                    list += "";
                                                    break;
                                                case "desc":
                                                    list += " descending";
                                                    break;
                                                default:
                                                    list = $"{property.Name}.{s}";
                                                    break;
                                            }
                                        }
                                        switch (s.ToLower())
                                        {
                                            case "asc":
                                                list += ", ";
                                                break;
                                            case "desc":
                                                list += " descending, ";
                                                break;
                                            default:
                                                list = $"{property.Name}.{s}";
                                                break;
                                        }
                                    }
                                    dataChild = list;
                                }
                            }
                            else if (AttributeChildType.Equals(typeof(RangeAttribute)))
                            {
                                entityChildName = entityChildName.Substring(0, entityChildName.Length - 1);
                                source = DynamicChildRangeFilter(source, propertyChild, dataChild, entityChildName);
                            }
                            else
                                source = DynamicChildFilter(source, entityChildName, dataChild, AttributeChildType, propertyChild);
                        }
                    }
                }
                else if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(RangeAttribute))) &&
                                !property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(SkipRangeAttribute))))
                {
                    source = DynamicRangeFilter(source, property, entityData);
                }
            }

            return source;
        }
        #endregion

        #region Range
        public static IQueryable<TEntity> DynamicRangeFilter<TEntity>(this IQueryable<TEntity> source, PropertyInfo property, object data)
        {
            var entityName = property.Name;
            var list = new List<TEntity>();
            var flag = false;
            if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(StringRangeAttribute))))
            {
                var Values = data as List<string>;
                if (Values.Any())
                {
                    flag = true;
                    if (!property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(RangeFieldAttribute))))
                        entityName = entityName.Substring(0, property.Name.Length - 1) + ".ToLower().Contains(@0)";
                    else
                        entityName += ".ToLower().Contains(@0)";
                    foreach (var value in Values)
                        list = GetDynamicRangeData(source, list, value.ToLower().Trim(), entityName);
                }
            }
            if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(IntRangeAttribute))))
            {
                var Values = data as List<int?>;
                if (Values.Any())
                {
                    flag = true;
                    if (!property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(RangeFieldAttribute))))
                        entityName = entityName.Substring(0, property.Name.Length - 1) + " == @0";
                    else
                        entityName += " == @0";
                    foreach (var value in Values)
                        list = GetDynamicRangeData(source, list, value, entityName);
                }
            }
            if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(GuidRangeAttribute))))
            {
                var Values = data as List<Guid?>;
                if (Values.Any())
                {
                    flag = true;
                    if (!property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(RangeFieldAttribute))))
                        entityName = entityName.Substring(0, property.Name.Length - 1) + " == @0";
                    else
                        entityName += " == @0";
                    foreach (var value in Values)
                        list = GetDynamicRangeData(source, list, value, entityName);
                }
            }
            if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(ByteRangeAttribute))))
            {
                var Values = data as List<byte?>;
                if (Values.Any())
                {
                    flag = true;
                    if (!property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(RangeFieldAttribute))))
                        entityName = entityName.Substring(0, property.Name.Length - 1) + " == @0";
                    else
                        entityName += " == @0";
                    foreach (var value in Values)
                        list = GetDynamicRangeData(source, list, value, entityName);
                }
            }
            if (flag)
                source = list.AsQueryable<TEntity>();
            else
            {
                var names = entityName.Split('.');
                if (names.Count() >= 2)
                {
                    entityName = "entity => entity.";
                    var ListCount = 0;
                    for (int i = 0; i < names.Count() - 1; i++)
                    {
                        if (names[i].EndsWith("s"))
                        {
                            ListCount += 1;
                            entityName += $"{names[i]}.Any(l{i} => l{i}.";
                        }
                        else
                            entityName += $"{names[i]}.";
                    }
                    var LastWord = names[names.Count() - 1];
                    entityName += $"{LastWord}.Equals(@0)" + new string(')', ListCount);
                }
            }
            source = DynamicNotRangeFilter(source, property, data, entityName);
            return source;
        }
        public static IQueryable<TEntity> DynamicChildRangeFilter<TEntity>(this IQueryable<TEntity> source, PropertyInfo property, object data, string entityName)
        {
            var result = GetEntityName(entityName);
            var sourceTemp = source;
            if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(SkipEmptyRangeAttribute))))
                sourceTemp = source.Where<TEntity>(result.Last());
            entityName = result.First();
            var list = new List<TEntity>();
            var flag = false;
            if (!(property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(SkipEmptyRangeAttribute))) && !sourceTemp.Any()))
            {
                if (property.CustomAttributes.Any(
                                     (Func<CustomAttributeData, bool>)(a =>
                                         a.AttributeType == typeof(StringRangeAttribute))))
                {
                    var Values = data as List<string>;
                    if (Values.Any())
                    {
                        flag = true;
                        entityName = entityName.Replace("Equals", "ToLower().Contains");
                        foreach (var value in Values)
                            list = GetDynamicRangeData(sourceTemp, list, value.ToLower().Trim(), entityName);
                    }
                }
                if (property.CustomAttributes.Any(
                                         (Func<CustomAttributeData, bool>)(a =>
                                             a.AttributeType == typeof(IntRangeAttribute))))
                {
                    var Values = data as List<int?>;
                    if (Values.Any())
                    {
                        flag = true;
                        foreach (var value in Values)
                            list = GetDynamicRangeData(sourceTemp, list, value, entityName);
                    }
                }
                if (property.CustomAttributes.Any(
                                         (Func<CustomAttributeData, bool>)(a =>
                                             a.AttributeType == typeof(GuidRangeAttribute))))
                {
                    var Values = data as List<Guid?>;
                    if (Values.Any())
                    {
                        flag = true;
                        foreach (var value in Values)
                            list = GetDynamicRangeData(sourceTemp, list, value, entityName);
                    }
                }
                if (property.CustomAttributes.Any(
                                         (Func<CustomAttributeData, bool>)(a =>
                                             a.AttributeType == typeof(ByteRangeAttribute))))
                {
                    var Values = data as List<byte?>;
                    if (Values.Any())
                    {
                        flag = true;
                        foreach (var value in Values)
                            list = GetDynamicRangeData(sourceTemp, list, value, entityName);
                    }
                }
            }
            if (flag)
                source = list.AsQueryable<TEntity>();
            source = DynamicNotRangeFilter(source, property, data, entityName);
            return source;
        }
        public static List<TEntity> GetDynamicRangeData<TEntity>(this IQueryable<TEntity> source, List<TEntity> list, object value, string entityName)
        {
            var temp = source.Where<TEntity>(entityName, value);
            if (temp.Any())
            {
                temp.ToList().ForEach(t =>
                {
                    if (!list.Contains(t))
                        list.Add(t);
                });
            }
            return list;
        }
        #endregion

        #region ChildRange
        public static IQueryable<TEntity> DynamicChildRange<TEntity>(this IQueryable<TEntity> source, PropertyInfo property, object data, string entityName, string entityChildName, bool First = true)
        {
            foreach (PropertyInfo propertyChild in property.PropertyType.GetProperties())
            {
                object dataChild = data.GetType().GetProperty(propertyChild.Name)
                    ?.GetValue(data, (object[])null);
                if (dataChild != null)
                {
                    var AttributeType = propertyChild.CustomAttributes.FirstOrDefault().AttributeType;
                    if (First)
                        entityName = $"{property.Name}.{propertyChild.Name}";
                    else
                    {
                        if (String.IsNullOrEmpty(entityChildName))
                            entityChildName = $"{entityName}.{propertyChild.Name}";
                        else
                            entityChildName = $"{entityChildName}.{propertyChild.Name}";
                    }
                    if (AttributeType.Equals(typeof(SortAttribute)))
                    {
                        string[] sort = dataChild.ToString().Split(", ");
                        if (sort.Length >= 2)
                        {
                            string list = "";
                            for (int i = 0; i < sort.Length; i++)
                            {
                                var s = sort[i];
                                if ((++i) == sort.Length || i == 0)
                                {
                                    switch (s.ToLower())
                                    {
                                        case "asc":
                                            list += "";
                                            break;
                                        case "desc":
                                            list += " descending";
                                            break;
                                        default:
                                            list = $"{property.Name}.{s}";
                                            break;
                                    }
                                }
                                switch (s.ToLower())
                                {
                                    case "asc":
                                        list += ", ";
                                        break;
                                    case "desc":
                                        list += " descending, ";
                                        break;
                                    default:
                                        list = $"{property.Name}.{s}";
                                        break;
                                }
                            }
                            dataChild = list;
                        }
                    }
                    else if (AttributeType.Equals(typeof(RangeAttribute)))
                    {
                        if (First)
                        {
                            if (entityName.EndsWith("s") &&
                            !propertyChild.CustomAttributes.Any(
                            (Func<CustomAttributeData, bool>)(a =>
                            a.AttributeType == typeof(RangeFieldAttribute))))
                                entityName = entityName.Substring(0, entityName.Length - 1);
                        }
                        else
                        {
                            if (entityChildName.EndsWith("s") &&
                           !propertyChild.CustomAttributes.Any(
                           (Func<CustomAttributeData, bool>)(a =>
                           a.AttributeType == typeof(RangeFieldAttribute))))
                                entityChildName = entityChildName.Substring(0, entityChildName.Length - 1);
                        }
                        source = DynamicChildRangeFilter(source, propertyChild, dataChild, First ? entityName : entityChildName);
                        entityChildName = null;
                    }
                    else if (AttributeType.Equals(typeof(ChildRangeAttribute)))
                        source = DynamicChildRange(source, propertyChild, dataChild, entityName, entityChildName, false);
                    else
                    {
                        var result = GetEntityName(First ? entityName : entityChildName);
                        var sourceTemp = source.Where<TEntity>(result.Last());
                        if (First)
                            entityName = result.First();
                        else
                            entityChildName = result.First();
                        source = DynamicNotRangeFilter(sourceTemp, propertyChild, dataChild, First ? entityName : entityChildName);
                        entityChildName = null;
                    }
                }
            }
            return source;
        }

        private static List<string> GetEntityName(string entityName)
        {
            var result = new List<string>();
            var names = entityName.Split('.');
            if (names.Count() >= 2)
            {
                entityName = "entity => entity.";
                var ListCount = 0;
                for (int i = 0; i < names.Count() - 1; i++)
                {
                    if (names[i].EndsWith("s"))
                    {
                        ListCount += 1;
                        entityName += $"{names[i]}.Any(l{i} => l{i}.";
                    }
                    else
                        entityName += $"{names[i]}.";
                }
                var LastWord = names[names.Count() - 1];
                var parentheses = new string(')', ListCount);
                var checkEmpty = $"{entityName}{LastWord} != null{parentheses}";
                entityName += $"{LastWord}.Equals(@0){parentheses}";
                result.Add(entityName);
                result.Add(checkEmpty);
            }
            return result;
        }
        #endregion
    }
}