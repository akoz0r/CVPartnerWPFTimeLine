using System.Collections.Generic;

namespace CVGuru.Domain
{
    public class User
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string DefaultCVID { get; set; }

        private string user_id,
                      _id,
                      id,
                      email,
                      role,
                      office_id,
                      office_name,
                      country_id,
                      country_code,
                      international_toggle,
                      preferred_download_format,
                      expand_proposals_toggle,
                      default_cv_id;

        public Dictionary<string, object> dictionary; 

        public string[] masterdata_languages;

        public User(object o)
        {
            var d = o as Dictionary<string, object>;
            Name = d["name"].ToString();
            ID = d["user_id"].ToString();
            DefaultCVID = d["default_cv_id"].ToString();
            dictionary = d;
        }

        public void ApplyCV(object[] cv)
        {
            //throw new System.NotImplementedException();
        }
    }
}
