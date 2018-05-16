# AmmoCRM.NET
SDK for Amo CRM on C#
How Use
## Auth 
doc: https://www.amocrm.ru/developers/content/api/auth

url: POST /private/api/auth.php
var authRes = Service.Auth("email", "api_secret", "company_link");
//authRes.Cookie - required for next requests

## Acount
doc: https://www.amocrm.ru/developers/content/api/account
url: GET /api/v2/account

var account = Service.Account(authRes.Cookie);

## Contact
doc:https://www.amocrm.ru/developers/content/api/contacts

### Add
```url: POST /api/v2/contacts```

```
//Create new contacts list
var newContact = new ContactForAdd
{
	add = new List<ContactAdd>(),
};

//Push contact
newContact.add.Add(
	 new ContactAdd
	 {
		 name = "Contact" + rnd.Next(),
		 created_at = DateTime.Now.Ticks,
		 custom_fields = new Dictionary<int, CustumFields>()
		 {
			 {
				 custumFieldId, new CustumFields()
				 {//Email
					 id=custumFieldId,
					 customValues = new List<Values>()
					 {
						 new Values(){
							 Enum="WORK",
							 value="b@b.com"
						 }
					 }
				 }
			 },
			 ...
		 },
		 responsible_user_id = responsibleManagerId,
		 created_by = cretedUserId,
	 }
	);

//Coll service method
var newContRes = Service.AddContact(newContact, authRes.Cookie);
```
### Get
```url:GET /api/v2/contacts/```
```
var contact = Service.GetContact(newContRes.ToString(), authRes.Cookie);
```
### Update
url: ```POST /api/v2/contacts```
```
var updateContact = new ContactForUpdate
{
    update = new List<ContactUpdate>()
};

var dic = new Dictionary<int, string>();
dic.Add(custumFieldId, "new_custum_value");

updateContact.update.Add(new ContactUpdate(_contact[0], dictionary: _dic)
{
    responsible_user_id = responsibleManagerId,
    created_by = cretedUserId
});

var _updContRes = Service.UpdateContact(_pdateContact, authRes.Cookie);
```

## Piplines
doc:https://www.amocrm.ru/developers/content/api/pipelines

### Add
url:```POST /private/api/v2/json/pipelines/set```
```
Dictionary<int, PiplineStatuses> mapItems = new Dictionary<int, PiplineStatuses>();
mapItems.Add(piplineStepId, new PiplineStatuses() { name = "First", sort = 10, color = "#d6eaff" });
mapItems.Add(piplineStepId, new PiplineStatuses() { name = "Second", sort = 20, color = "#d6eaff" });
mapItems.Add(piplineStepId, new PiplineStatuses() { name = "Last", sort = 30, color = "#d6eaff" });

var _newPipline = new PiplineForAdd()
{
    add = new List<PipleneAdd>()
    {
        new PipleneAdd()
        {
            name = "PiplineName",
            statuses = mapItems,
            is_main = "false",
            sort = 0
        }
    },
};

Dictionary<int, PiplneViewModelPipline> newPiplineResult = Service.CreatePipline(newPipline, authRes.Cookie);
```

### Update

url:```POST /private/api/v2/json/pipelines/set```

```
var updatePipline = new PiplineForUpdate()
{
    update = new List<PipleneUpdate>()
};
updatePipline.update.Add(new PipleneUpdate
{
    name = "Pipline" + rnd.Next(),
    statuses = new List<List<PiplineStatuses>>()
});
updatePipline.update.First().statuses[0].Add(new PiplineStatuses { name = "First", sort = 10, color = "#d6eaff" });
updatePipline.update.First().statuses[1].Add(new PiplineStatuses { name = "Second", sort = 20, color = "#d6eaff" });
updatePipline.update.First().statuses[2].Add(new PiplineStatuses { name = "Last", sort = 30, color = "#d6eaff" });


var updatePiplineResult = Service.UpdatePipline(updatePipline, authRes.Cookie);
```
### Get
url: ```GET /api/v2/pipelines```

```
var _getPipline = Service.GetPiplines(_authRes.Cookie).FirstOrDefault(x => x.Key == piplineId);
```

## Leads
doc: https://www.amocrm.ru/developers/content/api/pipelines

### Add
```
var newLead1 = new LeadsForAdd()
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
				   custumFieldId,new AmmoCRMTest.Entities.CustumFields()
				   {
					   id=custumFieldId,
					   name="custumFieldName",
					   customValues = new List<AmmoCRMTest.Entities.Values>()
					   {
						   new AmmoCRMTest.Entities.Values() {value = custumFieldValue}
					   }
				   }
			   }
		   }
	}
	}
};
var newLeadSave1 = Service.CreateLead(newLead1, authRes.Cookie);
```

### Update
url:
```
var updateLead = new LeadsForUpdate()
{
	update = new List<Update>()
	{
		new Update(leads.FirstOrDefault(x=>x.id ==newLeadSave1)){
			
			contacts_id = new List<int>(leads.FirstOrDefault(x=>x.id ==newLeadSave1).contacts.id),
			sale=500,
			pipeline_id = _getPipline.Value.id,
			status_id=_getPipline.Value.statuses.FirstOrDefault(x=>x.Value.name=="Успешно реализовано").Key,//PiplineStageId
			custom_fields = new Dictionary<int, CustumFields>()
	   {
		   {custumFieldId,new CustumFields()
			   {
				   id=14custumFieldId2531,
				   name="custumFieldName",
				   customValues = new List<Values>()
				   {
					   new Values() {value = custumFieldValue}
				   }
			   }
		   },
		   ...
		   }
	   }
		}
	}

};

var updateLeadResult = Service.UpdateLead(updateLead, authRes.Cookie);
```
### Get ```TODO:Need Update```
url: 
```
var leads = Service.GetLeads(null, _authRes2.Cookie);//Need rewrite method for requested leadIdValue
```
