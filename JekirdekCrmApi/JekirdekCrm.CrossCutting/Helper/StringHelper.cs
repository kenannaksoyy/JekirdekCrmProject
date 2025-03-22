using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.CrossCutting.Helper
{
    //Striglerin İşlemleri İçin Yardımcı Olacaktır
    public static class StringHelper
    {
        //Gelen Objedeki String Propsları Trimliyecek Reflectiondan Kaynaklı Maliyetli Bir İşlem 
        public static void TrimStringProperties<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            // GetType ile Tip GetPropeties ile Propsu Al Reflection ile Tüm Public String Özellikleri Al
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => p.PropertyType == typeof(string) && p.CanRead && p.CanWrite);

            foreach (var property in properties)
            {
                // Özelliğin Değerini al
                string value = (string)property.GetValue(obj);

                // Değer Null Değilse Trimleyerek Setle
                if (value != null)
                {
                    property.SetValue(obj, value.Trim());
                }
            }
        }
    }
}
