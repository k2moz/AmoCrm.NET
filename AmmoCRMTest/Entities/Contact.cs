using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace AmmoCRMTest.Entities
{

    public class ContactViewModel
    {
        //public ContactViewModel()
        //{
        //    tags = new List<string>();
        //}

        public int id { get; set; }
        /// <summary>
        /// Название контакта
        /// </summary>
        public string name { get; set; }


        /// <summary>
        /// Дата и время создания контакта
        /// </summary>
        public long created_at { get; set; }

        /// <summary>
        /// Дата и время обновления контакта. Обязательно при обновлении сущности.
        /// </summary>
        //public long updated_at { get; set; }

        /// <summary>
        /// id пользователя ответственного за контакт
        /// </summary>
        public int responsible_user_id { get; set; }

        /// <summary>
        /// id пользователя создавшего контакт
        /// </summary>
        public int created_by { get; set; }

        /// <summary>
        /// Название новой компании. Параметр указывается для создания новой компании и привязке к ней контакта. Для привязки контакта к уже существующей компании, необходимо использовать параметр company_id
        /// </summary>
        public string company_name { get; set; }

        /// <summary>
        /// Теги, привязываемые к контакту. Задаются целостной строковой переменной, внутри строки перечисляются через запятую
        /// </summary>
        /// 

        //public string tags { get; set; }

        /// <summary>
        /// Сделки, привязываемые к контакту. Перечисляются через запятую.
        /// </summary>
        public string[] leads_id { get; set; }

        /// <summary>
        /// Покупатели, привязываемые к контакту. Перечисляются через запятую.
        /// </summary>
        public string[] customers_id { get; set; }

        /// <summary>
        /// Компании, привязываемые к контакту. Перечисляются через запятую.
        /// </summary>
        public string[] company_id { get; set; }

        /// <summary>
        /// Массив дополнительных полей сущности 'Контакт'
        /// </summary>
        //public Dictionary<int, CustumFields> custom_fields { get; set; }

        public List<CustumFieldsView> custom_fields { get; set; }
    }

    [DataContract()]
    [JsonObject]
    public class ContactForAdd
    {
        [JsonProperty("add")]
        public List<ContactAdd> add { get; set; }
    }

    [DataContract()]
    [JsonObject]
    public class ContactForUpdate
    {
        [JsonProperty("update")]
        public List<ContactUpdate> update { get; set; }
    }

    [JsonObject]
    [DataContract()]
    public class ContactAdd
    {
        /// <summary>
        /// Название контакта
        /// </summary>
        /// 
        public string name { get; set; }


        /// <summary>
        /// Дата и время создания контакта
        /// </summary>
        public long created_at { get; set; }

        /// <summary>
        /// Дата и время обновления контакта. Обязательно при обновлении сущности.
        /// </summary>
        public long updated_at { get; set; }

        /// <summary>
        /// id пользователя ответственного за контакт
        /// </summary>
        public int responsible_user_id { get; set; }

        /// <summary>
        /// id пользователя создавшего контакт
        /// </summary>
        public int created_by { get; set; }

        /// <summary>
        /// Название новой компании. Параметр указывается для создания новой компании и привязке к ней контакта. Для привязки контакта к уже существующей компании, необходимо использовать параметр company_id
        /// </summary>
        public string company_name { get; set; }

        /// <summary>
        /// Теги, привязываемые к контакту. Задаются целостной строковой переменной, внутри строки перечисляются через запятую
        /// </summary>
        public string tags { get; set; }

        /// <summary>
        /// Сделки, привязываемые к контакту. Перечисляются через запятую.
        /// </summary>
        public string leads_id { get; set; }

        /// <summary>
        /// Покупатели, привязываемые к контакту. Перечисляются через запятую.
        /// </summary>
        public string customers_id { get; set; }

        /// <summary>
        /// Компании, привязываемые к контакту. Перечисляются через запятую.
        /// </summary>
        public string company_id { get; set; }

        /// <summary>
        /// Массив дополнительных полей сущности 'Контакт'
        /// </summary>
        /// 
        [JsonProperty("custom_fields")]
        public Dictionary<int, CustumFields> custom_fields { get; set; }
    }

    [DataContract()]
    [JsonObject]
    public class ContactUpdate : ContactAdd
    {
        public ContactUpdate() { }

        public ContactUpdate(ContactViewModel oldContact, string leads = "", Dictionary<int, string> dictionary = null)
        {
            id = oldContact.id;
            name = oldContact.name;
            responsible_user_id = oldContact.responsible_user_id;
            created_by = oldContact.created_by;
            created_at = oldContact.created_at;
            updated_at = DateTime.Now.Ticks;

            if (oldContact.leads_id != null)
                leads_id = String.Join(",", oldContact.leads_id);

            if (oldContact.custom_fields != null)
            {
                int count = 0;
                custom_fields = new Dictionary<int, CustumFields>();
                foreach (var item in oldContact.custom_fields)
                {
                    custom_fields.Add(
                        item.id, new CustumFields()
                        {
                            id = item.id,
                            name = item.name,
                            customValues = new List<Values>(){
                                new Values(){value = item.values[0].value,  Enum = "WORK" },
                            }
                        });
                    count++;
                }

            }

            //updated_by

            if (!string.IsNullOrEmpty(leads))
            {
                leads_id = leads;
            }

            if (dictionary != null)
            {
                custom_fields = new Dictionary<int, CustumFields>();
                foreach (var item in dictionary)
                {
                    custom_fields.Add(item.Key,
                            new CustumFields
                            {
                                id = item.Key,
                                customValues = new List<Values>
                                {
                                    new Values
                                    {
                                        value = item.Value.ToString() ,
                                        Enum = "WORK"
                                    }
                                }
                            });
                }
            }
        }

        /// <summary>
        /// id контакта, в которого будут вноситься изменения
        /// </summary>
        /// 
        [JsonProperty("id")]
        public int id { get; set; }

        /// <summary>
        /// Дата и время изменения
        /// </summary>
        public long updated_at { get; set; }

        /// <summary>
        /// Массив, содержащий информацию для открепления контакта от других элементов сущностей.
        /// </summary>
        public List<ContactUnlink> unlink { get; set; }
    }

    [DataContract()]
    public class ContactUnlink
    {
        /// <summary>
        /// Массив id открепляемых сделок
        /// </summary>
        public List<int> leads_id { get; set; }

        /// <summary>
        ///  id открепляемой компании
        /// </summary>
        public int company_id { get; set; }

    }

}
