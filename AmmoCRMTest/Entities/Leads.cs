using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmmoCRMTest.Entities
{

    public class LeadsRequest
    {
        /// <summary>
        /// Кол-во выбираемых строк(системное ограничение 500)
        /// </summary>
        public int limit_rows { get; set; }

        /// <summary>
        /// Сдвиг выборки(с какой строки выбирать). Работает, только при условии, что limit_rows тоже указан
        /// </summary>
        public int limit_offset { get; set; }

        /// <summary>
        /// Выбрать элемент с заданным ID(Если указан этот параметр, все остальные игнорируются). Можно передавать в виде массива состоящий из нескольких ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Поисковый запрос(Осуществляет поиск по заполненым полям сущности)
        /// </summary>
        public string query { get; set; }

        /// <summary>
        /// Дополнительный фильтр поиска, по ответственному пользователю (Можно передавать в виде массива)
        /// </summary>
        public int responsible_user_id { get; set; }

        /// <summary>
        /// Фильтр по ID статуса сделки(Как узнать список доступных ID см. здесь)(Можно передавать в виде массива)
        /// </summary>
        public int status { get; set; }
    }

    [DataContract]
    [JsonObject]
    public class LeadsViewModel
    {
        public LeadsViewModel()
        {
            custom_fields = new List<CustumFieldsView>();
        }
        public int id { get; set; }

        /// <summary>
        ///	Время
        /// </summary>
        public long updated_at { get; set; }

        /// <summary>
        ///  Название сделки
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Дата создания текущей сделки
        /// </summary>
        public long created_at { get; set; }

        /// <summary>
        /// Статус сделки (id этапа продаж см. Воронки и этапы продаж) Чтобы перенести сделку в другую воронку, необходимо установить ей статус из нужной воронки
        /// </summary>
        public int status_id { get; set; }

        /// <summary>
        /// ID воронки. Указывается в том случае, если выбраны статусы id 142 или 143, т.к. эти статусы не уникальны и обязательны для всех цифровых воронок.
        /// </summary>
        public PiplineShortViewModel pipeline{ get; set; }

        
        /// <summary>
        /// ID ответственного пользователя
        /// </summary>
        public int responsible_user_id { get; set; }

        /// <summary>
        /// Бюджет сделки
        /// </summary>
        public int sale { get; set; }

        public int company_id { get; set; }

        [DataMember(Name = "contacts")]

        public LeadsViewModelContactsValue contacts { get; set; }

        [DataMember(Name = "custom_fields")]
        public List<CustumFieldsView> custom_fields { get; set; }
    }

    [DataContract]
    [JsonObject]
    public class LeadsViewModelContactsValue
    {
        public LeadsViewModelContactsValue()
        {
            id = new List<int>();
        }
        public List<int> id { get; set; }
    }

    [DataContract]
    public class Leads
    {
        [DataMember(Name = "add")]
        public List<Add> Add { get; set; }

        [DataMember(Name = "update")]
        public List<Update> Update { get; set; }
    }

    [DataContract()]
    public class LeadsForAdd
    {
        [DataMember(Name = "add")]
        public List<Add> add { get; set; }
    }

    [DataContract()]
    public class LeadsForUpdate
    {
        [DataMember(Name = "update")]
        public List<Update> update { get; set; }
    }

    [DataContract()]
    [JsonObject]
    public class Add
    {
        /// <summary>
        ///  Название сделки
        /// </summary>
        /// 
        [JsonProperty]
        [DataMember]
        public string name { get; set; }

        /// <summary>
        /// Дата создания текущей сделки
        /// </summary>
        /// 
        [JsonProperty]
        [DataMember]
        public long created_at { get; set; }

        /// <summary>
        /// Дата изменения текущей сделки
        /// </summary>
        /// 
        [JsonProperty]
        [DataMember]
        public long updated_at { get; set; }

        /// <summary>
        /// Статус сделки (id этапа продаж см. Воронки и этапы продаж) Чтобы перенести сделку в другую воронку, необходимо установить ей статус из нужной воронки
        /// </summary>
        /// 
        [JsonProperty]
        [DataMember]
        public int status_id { get; set; }

        /// <summary>
        /// ID воронки. Указывается в том случае, если выбраны статусы id 142 или 143, т.к. эти статусы не уникальны и обязательны для всех цифровых воронок.
        /// </summary>
        /// 
        [JsonProperty]
        [DataMember]
        public int pipeline_id { get; set; }

        /// <summary>
        /// ID ответственного пользователя
        /// </summary>
        /// 
        [JsonProperty]
        [DataMember]
        public int responsible_user_id { get; set; }

        /// <summary>
        /// Бюджет сделки
        /// </summary>
        /// 
        [JsonProperty]
        [DataMember]
        public int sale { get; set; }

        /// <summary>
        /// Если вы хотите задать новые теги, перечислите их внутри строковой переменной через запятую. В случае если вам необходимо прикрепить существующие теги, передавайте массив числовых значений id существующих тегов.
        /// </summary>
        //public List<string> tags { get; set; }

        /// <summary>
        /// Уникальный идентификатор контакта, для связи с сделкой. Можно передавать несколько id, перечисляя их в массиве через запятую.
        /// </summary>
        /// 
        /// 
        [JsonProperty]
        [DataMember]
        public List<int> contacts_id { get; set; }

        /// <summary>
        /// Уникальный идентификатор компании, для связи с сделкой
        /// </summary>
        public int company_id { get; set; }

        [DataMember(Name = "custom_fields")]
        public Dictionary<int, CustumFields> custom_fields { get; set; }

    }

    [DataContract]
    [JsonObject]
    public class Update : Add
    {

        public Update() { }
        public Update(LeadsViewModel oldLead)
        {
            //id = oldLead.Lead.id;
            //name = oldLead.Lead.name;
            //updated_at = DateTime.Now.Ticks;
            //pipeline_id = oldLead.Lead.pipeline_id;
            //responsible_user_id = oldLead.Lead.responsible_user_id;
            //sale = oldLead.Lead.sale;
            //tags = oldLead.Lead.tags;
            //contacts_id = oldLead.Lead.contacts_id;
            //company_id = company_id;
            //custom_fields = custom_fields;

            id = oldLead.id;
            name = oldLead.name;
            created_at = oldLead.created_at;
            updated_at = DateTime.Now.Ticks;
            pipeline_id = oldLead.pipeline.id;
            responsible_user_id = oldLead.responsible_user_id;
            sale = oldLead.sale;
            //tags = oldLead.tags;
            contacts_id = oldLead.contacts.id;
            company_id = oldLead.company_id;
            
            custom_fields = new Dictionary<int, CustumFields>();

            foreach (var item in oldLead.custom_fields.Where(x=>x.id!=0))
            {
                var _cf = new CustumFields
                {
                    id = item.id,
                    customValues = new List<Values>(){
                        new Values()
                        {
                            Enum = "WORK",
                            value = item.values.First().value
                        }
                    }
                };
                
                custom_fields.Add(item.id, _cf);
            }
        }


        /// <summary>
        /// id сделки, в которую будут вноситься изменения
        /// </summary>
        public int id { get; set; }

        /// <summary>
        ///	Время
        /// </summary>
        public long updated_at { get; set; }


    }





    [DataContract()]
    public class unlink
    {
        /// <summary>
        ///  id открепляемых контактов
        /// </summary>
        public List<int> contacts_id { get; set; }

        /// <summary>
        ///  id открепляемой компании
        /// </summary>
        public List<int> company_id { get; set; }
    }
}
