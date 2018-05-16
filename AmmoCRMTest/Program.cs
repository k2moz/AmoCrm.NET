using AmmoCRMTest.BL;
using AmmoCRMTest.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AmmoCRMTest
{
    class Program
    {

        static void Main(string[] args)
        {
            //var a = TransformDocumentationToCode(@"
            //");

            //var _authRes1 = Service.Auth("email", "secret");
            //var _account1 = Service.Account(_authRes1.Cookie);



            ///#Auth
            ///
            
            var _authRes2 = Service.Auth("email", "secret", "link");
            var _account2 = Service.Account(_authRes2.Cookie);
            //var _pipeline = Service.Pipline(1038580, _authRes2.Cookie);

            Random rnd = new Random();

            ///#Contacts
            var _newContact = new ContactForAdd
            {
                add = new List<ContactAdd>(),

            };
            _newContact.add.Add(
                 new ContactAdd
                 {
                     name = "Contact" + rnd.Next(),
                     created_at = DateTime.Now.Ticks,
                     custom_fields = new Dictionary<int, CustumFields>()
                     {
                         {
                             136807, new CustumFields()
                             {//Email
                                 id=136807,
                                 customValues = new List<Values>()
                                 {
                                     new Values(){
                                         Enum="WORK",
                                         value="b@b.com"
                                     }
                                 }
                             }
                         },
                         {
                             142537, new CustumFields()
                             {//UserId
                                 id=142537,
                                 customValues = new List<Values>()
                                 {
                                     new Values(){
                                         Enum="WORK",
                                         value="4"
                                     }
                                 }
                             }
                         }
                     },
                     responsible_user_id = 3204941,
                     created_by = 3204941,
                 }
                );

            var _newContRes = Service.AddContact(_newContact, _authRes2.Cookie);


            var _contact = Service.GetContact(_newContRes.ToString(), _authRes2.Cookie);
            //var _contact = Service.GetContact(3215401.ToString(), _authRes2.Cookie);

            //var _updateContact = new ContactForUpdate
            //{
            //    update = new List<ContactUpdate>()
            //};

            //var _dic = new Dictionary<int, string>();
            //_dic.Add(136805, "8914752739");//Tel из Account
            //_dic.Add(136807, "a@a.com");//Email из Account

            //_dic.Add(142537, 4.ToString());//UserId из Account
            //_dic.Add(142583, "vk.com");//DictionaryLink  из Account
            //_dic.Add(142585, "fb.com");//sds

            //_updateContact.update.Add(new ContactUpdate(_contact[0], dictionary: _dic)
            //{
            //    responsible_user_id = 3204941,
            //    created_by = 3204941
            //});

            //var _updContRes = Service.UpdateContact(_updateContact, _authRes2.Cookie);

            ///#Piplines
            //Dictionary<int, PiplineStatuses> mapItems = new Dictionary<int, PiplineStatuses>();
            //mapItems.Add(1, new PiplineStatuses() { name = "First", sort = 10, color = "#d6eaff" });
            //mapItems.Add(2, new PiplineStatuses() { name = "Second", sort = 20, color = "#d6eaff" });
            //mapItems.Add(32, new PiplineStatuses() { name = "Last", sort = 30, color = "#d6eaff" });

            //var _newPipline = new PiplineForAdd()
            //{
            //    add = new List<PipleneAdd>()
            //    {
            //        new PipleneAdd()
            //        {
            //            name = "Pipline" + rnd.Next(),
            //            statuses = mapItems,
            //            is_main = "false",
            //            sort = 0
            //        }
            //    },
            //};

            //Dictionary<int, PiplneViewModelPipline> _newPiplineResult = Service.CreatePipline(_newPipline, _authRes2.Cookie);

            
            var _getPipline = Service.GetPiplines(_authRes2.Cookie).FirstOrDefault(x => x.Key == 1076005);
            //var _getPipline = Service.GetPiplines(_authRes2.Cookie).FirstOrDefault(x => x.Key == _newPiplineResult.First().Key);

            //var _getPipline = Service.GetPipline(1067701, _authRes2.Cookie);

            //var _updatePipline = new PiplineForUpdate()
            //{
            //    update = new List<PipleneUpdate>()
            //};
            //_updatePipline.update.Add(new PipleneUpdate
            //{
            //    name = "Pipline" + rnd.Next(),
            //    statuses = new List<List<PiplineStatuses>>()
            //});
            //_updatePipline.update.First().statuses[0].Add(new PiplineStatuses { name = "First", sort = 10, color = "#d6eaff" });
            //_updatePipline.update.First().statuses[1].Add(new PiplineStatuses { name = "Second", sort = 20, color = "#d6eaff" });
            //_updatePipline.update.First().statuses[2].Add(new PiplineStatuses { name = "Last", sort = 30, color = "#d6eaff" });


            //var _updatePiplineResult = Service.UpdatePipline(_updatePipline, _authRes2.Cookie);


            var _newLead1 = new LeadsForAdd()
            {
                add = new List<Add>()
                {
                new Add
                {
                   name="Сделка "+rnd.Next(),
                   contacts_id = new List<int>(){_newContRes},
                   created_at = DateTime.Now.Ticks,
                   pipeline_id = _getPipline.Key,
                   responsible_user_id = 3204941,//PiplineId
                   status_id = _getPipline.Value.statuses.First().Key,//PiplineStageId
                   custom_fields = new Dictionary<int, AmmoCRMTest.Entities.CustumFields>()
                       {
                           {
                               142531,new AmmoCRMTest.Entities.CustumFields()
                               {
                                   id=142531,
                                   name="InviteId Value",
                                   customValues = new List<AmmoCRMTest.Entities.Values>()
                                   {
                                       new AmmoCRMTest.Entities.Values() {value = 4.ToString()}
                                   }
                               }
                           }
                       }
                }
                }
            };
            var _newLeadSave1 = Service.CreateLead(_newLead1, _authRes2.Cookie);

            //var _newLead2 = new LeadsForAdd()
            //{
            //    add = new List<Add>()
            //    {
            //    new Add
            //    {
            //       name="Сделка "+rnd.Next(),
            //       contacts_id = new List<int>(){_updContRes},
            //       created_at = DateTime.Now.Ticks,
            //       pipeline_id = _getPipline.Key,
            //       responsible_user_id = 3204941,//PiplineId
            //       status_id = _getPipline.Value.statuses.First().Key,//PiplineStageId
            //    }
            //    }
            //};
            //var _newLeadSave2 = Service.CreateLead(_newLead2, _authRes2.Cookie);

            var leads = Service.GetLeads(null, _authRes2.Cookie);


            var _updateLead = new LeadsForUpdate()
            {
                update = new List<Update>()
                {
                    new Update(leads.FirstOrDefault(x=>x.id ==_newLeadSave1)){
                        
                        contacts_id = new List<int>(leads.FirstOrDefault(x=>x.id ==_newLeadSave1).contacts.id),
                        sale=500,
                        pipeline_id = _getPipline.Value.id,
                        status_id=_getPipline.Value.statuses.FirstOrDefault(x=>x.Value.name=="Успешно реализовано").Key,
                        custom_fields = new Dictionary<int, CustumFields>()
                   {
                       {142531,new CustumFields()
                           {
                               id=142531,
                               name="InviteId Value",
                               customValues = new List<Values>()
                               {
                                   new Values() {value = 55.ToString()}
                               }
                           }
                       },
                       {142527,new CustumFields()
                           {
                               id=142527,
                               name="PlanId Value",
                               customValues = new List<Values>()
                               {
                                   new Values() {value = 555.ToString()}
                               }
                           }
                       },
                       {142579,new CustumFields()
                           {
                               id=142579,
                               name="DirectoryId Value",
                               customValues = new List<Values>()
                               {
                                   new Values() {value = 5556.ToString()}
                               }
                           }
                       },
                       {142581,new CustumFields()
                           {
                               id=142581,
                               name="DirectoryLink Value",
                               customValues = new List<Values>()
                               {
                                   new Values() {value = "directoryLink"}
                               }
                           }
                       }
                   }
                    }
                }

            };
            //var _updateLeadModel = new LeadsForUpdate()
            //{
            //    update = 
            //};
            var _updateLeadResult = Service.UpdateLead(_updateLead, _authRes2.Cookie);
            //1160779
            Console.ReadKey();
        }

        public static string TransformDocumentationToCode(string docStr)
        {
            string resultStr = "";
            for (int i = 0; i < docStr.Split('\n').Length; i++)
            {

                var _name = docStr.Split('\n')[i].Split('\t')[0];
                var _type = docStr.Split('\n')[i].Split('\t')[1];
                var _comment = docStr.Split('\n')[i].Split('\t')[2];
                resultStr += @"
                /// <summary>
                /// " + _comment.Replace("\n", "") + @"
                /// </summary>
                public " + _type + @" " + _name + @" {get;set;}";
            }
            return resultStr;
        }


    }
}
