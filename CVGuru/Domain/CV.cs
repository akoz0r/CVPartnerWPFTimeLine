using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CVGuru.Domain
{
    public class CV
    {
        /*
         {"_id":"51a87ffe6fc6267256000020",
         * "born_day":1,
         * "born_month":1,
         * "born_year":1975,
         * "bruker_id":"51a87ff16fc626ecac00005c","
         * created_at":"2013-05-31T10:48:30Z",
         * "default":true,
         * "default_international":false,
         * "educations":[{"_id":"522744731720e834080000ef","degree":null,"description":null,"disabled":false,"int_degree":null,"int_description":null,"int_school":null,"int_year_from":null,"int_year_to":null,"local_degree":null,"local_description":null,"local_school":null,"local_year_from":null,"local_year_to":null,"modifier_id":null,"order":1,"school":null,"starred":false,"version":null,"year_from":null,"year_to":null},{"_id":"522744801720e8e99f0000d5","degree":null,"description":null,"disabled":false,"int_degree":null,"int_description":null,"int_school":null,"int_year_from":null,"int_year_to":null,"local_degree":null,"local_description":null,"local_school":null,"local_year_from":null,"local_year_to":null,"modifier_id":null,"order":0,"school":null,"starred":false,"version":null,"year_from":null,"year_to":null}],
         * "int_nationality":null,
         * "int_place_of_residence":null,"int_title":null,"language_code":"no","local_nationality":null,"local_place_of_residence":null,"local_title":null,"modifier_id":null,"nationality":null,"navn":"Anders L\u00f8ken","place_of_residence":null,
         * "project_experiences":[],"technologies":[{"_id":"5227412a1720e8646c000138","category":"Databaser","disabled":false,"exclude_tags":[],"int_category":null,"int_tags":[],"local_category":"Databaser","local_tags":[],"modifier_id":null,"order":2,"starred":false,"tags":[],"version":null},{"_id":"522743a11720e803ab000024","category":"Byggesystemer","disabled":false,"exclude_tags":null,"int_category":null,"int_tags":[],"local_category":"Byggesystemer","local_tags":[],"modifier_id":null,"order":1,"starred":false,"tags":null,"version":null},{"_id":"522743bf1720e8d5d5000216","category":"Databaser","disabled":false,"exclude_tags":null,"int_category":null,"int_tags":[],"local_category":"Databaser","local_tags":[],"modifier_id":null,"order":0,"starred":false,"tags":null,"version":null}],"telefon":null,"tilbud_id":null,"title":null,"twitter":null,"updated_at":"2014-03-03T22:36:04Z","version":1,
         * "email":"anders.loken@webstep.no",
         * "country_code":"no",
         * "image":{"url":null,"thumb":{"url":null},
             * "fit_thumb":{"url":null},
             * "small_thumb":{"url":null}},
             * "can_write":true}*/
        public CV(string json)
        {
            //var obj = GetJSONObjects(json);
            //var projects = obj["project_experiences"] as dynamic[];
            //foreach (var project in projects)
            //{
            //    var startYear = project["year_from"];
            //    var startMonth = project["month_from"];
            //    var endYear = project["year_to"];
            //    var endMonth = project["month_to"];
            //    var label = project["customer"] + " - " + project["description"];
            //    var url = "http://artsyeditor.com/wp-content/uploads/2011/03/hello-world.png"; //todo : google search
            //}
        }

        private Dictionary<string, object> GetJSONObjects(string s)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var objectList = (Dictionary<string,object>)jsonSerializer.DeserializeObject(s);
            return objectList;
        }

    }
}
