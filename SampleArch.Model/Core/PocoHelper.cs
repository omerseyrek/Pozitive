using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Core
{
    public static class PocoHelper
    {
        public static M BasicPocoMapper<T, M>(T Source)
        {
            M target = Activator.CreateInstance<M>();

            PropertyInfo[] fields = target.GetType().GetProperties();

            foreach (PropertyInfo p in fields)
            {
                var sourceField = Source.GetType().GetProperty(p.Name);
                var targetField = target.GetType().GetProperty(p.Name);

                if (sourceField != null)
                {
                    var sourceValue = sourceField.GetValue(Source, null);

                    if (sourceValue == null)
                    {
                        continue;
                    }

                    if (sourceValue.GetType().Namespace == "System.Data.Entity.DynamicProxies")
                    {
                        var sValue = GetRealObjectOfProxy(sourceField.PropertyType, targetField.PropertyType, sourceValue);
                        p.SetValue(target, sValue);
                    }
                    else
                    {
                        p.SetValue(target, sourceValue);
                    }
                }
            }
            return target;
        }

        public static object GetRealObjectOfProxy(Type TSource, Type TTarget, object  Source)
        {
            var target = Activator.CreateInstance(TTarget);

            PropertyInfo[] fields = TTarget.GetProperties();

            foreach (PropertyInfo p in fields)
            {
                var sourceField = Source.GetType().GetProperty(p.Name);
                var targetField = target.GetType().GetProperty(p.Name);

                if (sourceField != null)
                {
                    var sourceValue = sourceField.GetValue(Source, null);

                    if (sourceValue == null)
                    {
                        continue;
                    }

                    if (sourceValue.GetType().Namespace == "System.Data.Entity.DynamicProxies")
                    {
                        var sValue = GetRealObjectOfProxy(sourceField.PropertyType, targetField.PropertyType, sourceValue);
                        p.SetValue(target, sValue);
                    }
                    else
                    {
                        p.SetValue(target, sourceValue);
                    }
                }
            }
            return target;
        }

        public static T BasicSelfPocoMapper<T>(T Source)
        {
            T target = Activator.CreateInstance<T>();

            PropertyInfo[] fields = target.GetType().GetProperties();

            foreach (PropertyInfo p in fields)
            {
                var sourceField = Source.GetType().GetProperty(p.Name);
                if (sourceField != null)
                {
                    var sourceValue =  sourceField.GetValue(Source, null);
                    p.SetValue(target, sourceValue);
                }
            }
            return target;
        }

        public static void UpdatePocoMapper<T, K>(T Source, K Target)
        {
            PropertyInfo[] fields = Target.GetType().GetProperties();

            PropertyInfo[] inheritedFields = Source.GetType().BaseType.GetProperties(BindingFlags.Public  | BindingFlags.Instance);

            foreach (PropertyInfo p in fields)
            {
                var sourceField = Source.GetType().GetProperty(p.Name);

                if (sourceField != null)
                {
                    string fieldName = sourceField.Name;

                    if (inheritedFields.Any(px => px.Name == fieldName))
                    {
                        continue;
                    }
                    if ( sourceField.PropertyType == typeof(System.String) ||
                         sourceField.PropertyType == typeof(System.Int16) ||
                         sourceField.PropertyType == typeof(System.Int16?) ||
                         sourceField.PropertyType == typeof(System.Int32) || 
                         sourceField.PropertyType == typeof(System.Int32?) ||
                         sourceField.PropertyType == typeof(System.Int64) ||
                         sourceField.PropertyType == typeof(System.Int64?) ||
                         sourceField.PropertyType == typeof(System.Char) ||
                         sourceField.PropertyType == typeof(System.DateTime) ||
                         sourceField.PropertyType == typeof(System.DateTime?) ||
                         sourceField.PropertyType == typeof(System.Byte) ||
                         sourceField.PropertyType == typeof(System.Byte?) ||
                         sourceField.PropertyType == typeof(System.Boolean) ||
                         sourceField.PropertyType == typeof(System.Boolean?)) 
                    {
                        p.SetValue(Target, sourceField.GetValue(Source, null));
                    }
                    else
                    {
                        var destintionField = Target.GetType().GetProperty(fieldName);

                        if (destintionField != null)
                        {
                            var sourceItem = sourceField.GetValue(Source, null);
                            if (sourceItem == null)
                            {
                                continue;
                            }

                            try
                            {
                                destintionField.SetValue(Target, sourceItem);
                                continue;
                            }
                            catch(Exception ex)
                            {
                                // to do : do nothing
                            }

                            //UpdatePocoMapper(sourceItem, targetItem);

                        }
                    }
                }
            }
        }

        public static void SetTractionFieldsOfEntitiy<T>(T data, int userId, DateTime theDate) where T : IEntity<int>
        {
            //PropertyInfo[] trackingFields = typeof(BaseViewModel).GetProperties();

            if (data.Id > 0)
            {
                PropertyInfo updateUserField = data.GetType().GetProperty("UpdateUserId");
                PropertyInfo updateDateField = data.GetType().GetProperty("UpdateDate");

                if (updateUserField != null)
                {
                    updateUserField.SetValue(data, userId);
                }

                if (updateDateField != null)
                {
                    updateDateField.SetValue(data, theDate);
                }
        
            }
            else
            {
                PropertyInfo createUserField = data.GetType().GetProperty("CreateUserId");
                PropertyInfo createDateField = data.GetType().GetProperty("CreateDate");

                if (createUserField != null)
                {
                    createUserField.SetValue(data, userId);
                }

                if (createDateField != null)
                {
                    createDateField.SetValue(data, theDate);
                }
        
            }
        }

    }
}
