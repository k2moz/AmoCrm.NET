using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmmoCRMTest.Entities
{



    [DataContract]
    [JsonObject]
    public class PiplneViewModel
    {
        [JsonProperty("pipelines")]
        public Dictionary<int, PiplneViewModelPipline> piplines {get;set;}
    }

    [JsonObject]
    public class PiplneViewModelPipline
    {
        public int id { get; set; }
        public int value { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public int sort { get; set; }
        public bool is_main { get; set; }

        [JsonProperty("statuses")]
        public Dictionary<int, PiplineStatuses> statuses { get; set; }

       // public int leads { get; set; }
    }

    public class Pipline
    {
        /// <summary>
        /// Список добавляемых воронок, если возникнет ошибка, то этот ключ будет указан в описании к ошибке
        /// </summary>
        public List<PipleneAdd> add { get; set; }
    }

    public class PiplineShortViewModel
    {
        public int id { get; set; }
    }

    [JsonObject]
    public class PiplineForAdd
    {
        public List<PipleneAdd> add { get; set; }
    }
    public class PiplineForUpdate
    {
        public List<PipleneUpdate> update { get; set; }
    }

    [JsonObject]
    public class PipleneAdd
    {
        
        /// <summary>
        /// Имя воронки
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Порядковый номер воронки при отображении
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// Является ли воронка 'главной' (необходимо передать значение 'on', если является)
        /// </summary>
        public string is_main { get; set; }

        public Dictionary<int,PiplineStatuses> statuses { get; set; }
    }

    public class PipleneUpdate : PipleneAdd
    {
        /// <summary>
        /// Уникальный идентификатор этапа, который указывается с целью её обновления
        /// </summary>
        public int id { get; set; }
    }

    public class PiplineStatuses
    {
        //public int  Уникальный идентификатор воронки, который указывается с целью её обновления
        //public int? id { get; set; }
        /// <summary>
        /// Название этапа
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Порядковый номер этапа при отображении (автоматически пересчитывается после добавления)
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// Цвет этапа
        /// </summary>
        public string color { get; set; }
    }

    public class PiplineStatusesUpdate
    {
        /// <summary>
        ///  Уникальный идентификатор воронки, который указывается с целью её обновления
        /// </summary>
        public int id { get; set; }

    }
}
