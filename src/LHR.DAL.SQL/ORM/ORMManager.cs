﻿using LHR.Types.ORM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LHR.DAL.SQL.ORM
{
    public class ORMManager
    {
        public List<T> MapDataToBusinessEntityCollection<T> (IDataReader dr)
        where T : new()
        {
            List<T> entitys = new List<T>();
            while (dr.Read())
            {
                entitys.Add(MapDataToBusinessEntity<T>(dr, false, GetProperties(typeof(T))));
            }
            dr.Close();
            return entitys;
        }
        public T MapDataToBusinessEntity<T>(IDataReader dr)
        where T : new()
        {
            return MapDataToBusinessEntity<T>(dr, true, GetProperties(typeof(T)));
        }
        public T MapDataToBusinessEntity<T>(IDataReader dr, bool readData, Hashtable properties)
        where T : new()
        {
            bool dataReturned = true;
            if (readData)
            {
                dataReturned = dr.Read();
            }
            T newObject;
            if (dataReturned)
            {
                newObject = new T();
                for (int index = 0; index < dr.FieldCount; index++)
                {
                    PropertyInfo info = (PropertyInfo)
                                        properties[dr.GetName(index).ToUpper()];
                    if ((info != null) && info.CanWrite)
                    {
                        if (info.PropertyType.IsEnum)
                            //info.SetValue(newObject, Enum.ToObject(info.PropertyType, (int)dr.GetValue(index)), null);
                            info.SetValue(newObject, Enum.Parse(info.PropertyType, dr.GetValue(index).ToString(), true), null);
                        else
                            info.SetValue(newObject, dr.GetValue(index), null);
                    }
                }
            }
            else
            {
                newObject = default(T);
            }
            if (readData)
            {
                dr.Close();
            }
            return newObject;
        }
        private Hashtable GetProperties(Type businessEntityType)
        {
            Hashtable hashtable = new Hashtable();
            PropertyInfo[] properties = businessEntityType.GetProperties();
            foreach (PropertyInfo info in properties)
            {
                if(Attribute.IsDefined(info, typeof(FieldNameAttribute)))
                {
                    var attr = (FieldNameAttribute[])info.GetCustomAttributes(typeof(FieldNameAttribute), false);
                    hashtable[attr[0].FieldName.ToUpper()] = info;
                }
                else
                    hashtable[info.Name.ToUpper()] = info;
            }
            return hashtable;
        }
    }
}
