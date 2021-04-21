using System;

namespace DrawColumnsComboBox
{
    public class ConfigRepository
    {
        public string SchemeType { get; set; }
        public Uri RepositoryUri { get; set; }
        public override string ToString()
        {
            return RepositoryUri.ToString();
            //return "";
        }
    }
}
