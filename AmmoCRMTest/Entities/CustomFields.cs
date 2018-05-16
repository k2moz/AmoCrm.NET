using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmmoCRMTest.Entities
{
    [DataContract()]
    public class CustumFieldsView
    {
        public CustumFieldsView()
        {
            values = new List<ViewValues>();
        }
        public int id { get; set; }

        public string name { get; set; }

        [DataMember(Name = "values")]
        public List<ViewValues> values { get; set; }
    }

    [JsonObject()]
    [DataContract()]
    public class CustumFields
    {
        /// <summary>
        /// Уникальный идентификатор заполняемого дополнительного поля
        /// </summary>
        public int id { get; set; }
        public string name { get; set; }

        [JsonProperty("values")]
        public List<Values> customValues { get; set; }
    }

    [DataContract]
    [JsonObject()]
    public class Values
    {
        /// <summary>
        /// Значение дополнительного поля
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// Тип изменяемого элемента дополнительного поля типа "адрес". 
        /// !!! Внимание, все типы, которые не были переданы, будут стёрты
        /// </summary>
        public string subtype { get; set; }

        /// <summary>
        /// Идентификатор раннее предустановленного варианта выбора для списка или мультисписка
        /// </summary>
        [DataMember(Name = "enum")]
        public string Enum { get; set; } 

    }

    [DataContract()]
    public class ViewValues
    {
        /// <summary>
        /// Значение дополнительного поля
        /// </summary>
        public string value { get; set; }
    }
}
