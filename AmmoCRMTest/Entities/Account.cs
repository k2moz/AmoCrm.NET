using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmmoCRMTest.Entities
{
    public class AccountWrapper
    {

    }

    [DataContract]
    public class Account
    {
        /// <summary>
        /// Уникальный идентификатор аккаунта
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Название аккаунта
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Уникальный субдомен данного аккаунта
        /// </summary>
        public string subdomain { get; set; }

        /// <summary>
        /// Валюта аккаунта (используемая при работе с бюджетом сделок). Не связано с биллинговой информацией самого аккаунта.
        /// </summary>
        public string currency { get; set; }

        /// <summary>
        /// Временная зона
        /// </summary>
        public string timezone { get; set; }

        /// <summary>
        /// Cмещение временной зоны
        /// </summary>
        public string timezone_offset { get; set; }

        /// <summary>
        /// Язык аккаунта (ru - русский, en - английский)
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// //	Формат даты (описание формата см. здесь)
        /// </summary>
        public List<DatePatern> date_pattern { get; set; }

        /// <summary>
        /// id текущего пользователя
        /// </summary>
        public int current_user { get; set; }

        public List<User> users { get; set; }

        public List<Pipline> pipelines { get; set; }


    }



    [DataContract]
    public class DatePatern
    {
        /// <summary>
        /// Дата, формат зависит от выбранного формата в аккаунте
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// Время, формат зависит от выбранного формата в аккаунте
        /// </summary>
        public string time { get; set; }

        /// <summary>
        /// Дата и время, формат зависит от выбранного формата в аккаунте
        /// </summary>
        public string date_time { get; set; }

        /// <summary>
        /// Время с точностью до секунды, формат зависит от выбранного формата в аккаунте
        /// </summary>
        public string time_full { get; set; }

    }


    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string login { get; set; }

        /// <summary>
        /// Настройки языка пользователя
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// id группы, в которой состоит пользователь
        /// </summary>
        public int group_id { get; set; }

        /// <summary>
        /// Активна учётная запись пользователя или нет, если нет, то доступ будет закрыт
        /// </summary>
        public bool is_active { get; set; }

        /// <summary>
        /// Является ли учётная запись пользователя бесплатной
        /// </summary>
        public bool is_free { get; set; }

        /// <summary>
        /// Наличие прав администратора
        /// </summary>
        public bool is_admin { get; set; }

        public List<UserRight> rights { get; set; }
    }

    public class UserRight
    {

        /// <summary>
        ///Доступ к корпоративной почте
        /// </summary>
        public string mail { get; set; }

        /// <summary>
        /// Доступ к "неразобранному"
        /// </summary>
        public string incoming_leads { get; set; }

        /// <summary>
        ///Права пользователя на создание/редактирование каталогов и их элементов
        ///</summary>
        public string catalogs { get; set; }

        /// <summary>
        ///Права пользователя на добавление новых сделок
        ///</summary>
        public string lead_add { get; set; }
        /// <summary>
        ///Права пользователя на просмотра существующих сделок 
        ///</summary>
        public string lead_view { get; set; }

        /// <summary>
        ///Права пользователя на редактирование существующих сделок/// </summary>
        public string lead_edit { get; set; }

        /// <summary>
        ///Права пользователя на удаление существующих сделок///
        ///</summary>
        public string lead_delete { get; set; }

        /// <summary>
        ///Права пользователя на экспорт сделок/// </summary>
        public string lead_export { get; set; }

        /// <summary>
        ///Права пользователя на добавление новых контактов
        ///</summary>
        public string contact_add { get; set; }

        /// <summary>
        ///Права пользователя на просмотр существующих контактов/// 
        ///</summary>
        public string contact_view { get; set; }

        /// <summary>
        /// Права пользователя на редактирование существующих контактов
        /// </summary>
        public string contact_edit { get; set; }

        /// <summary>
        /// Права пользователя на удаление существующих контактов
        /// </summary>
        public string contact_delete { get; set; }

        /// <summary>
        /// Права пользователя на экспорт контактов
        /// </summary>
        public string contact_export { get; set; }

        /// <summary>
        /// Права пользователя на добавление новых компаний
        /// </summary>
        public string company_add { get; set; }

        /// <summary>
        /// Права пользователя на просмотр существующих компаний
        /// </summary>
        public string company_view { get; set; }

        /// <summary>
        /// Права пользователя на редактирование существующих компаний
        /// </summary>
        public string company_edit { get; set; }

        /// <summary>
        /// Права пользователя на удаление существующих компаний
        /// </summary>
        public string company_delete { get; set; }

        /// <summary>
        /// Права пользователя на экспорт существующих компаний
        /// </summary>
        public string company_export { get; set; }
    }


    public class PipleneStatuse
    {
        /// <summary>
        /// Уникальный идентификатор этапа
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Название этапа
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Порядковый номер этапа при отображении
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// Цвет этапа (подробнее см. здесь)
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// Есть ли возможность изменить или удалить этот этап
        /// </summary>
        public bool editable { get; set; }

    }


}
