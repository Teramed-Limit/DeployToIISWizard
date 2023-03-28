using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;
using DeployToIISWizard.Modal;

namespace DeployToIISWizard.Helper
{
    public class SiteConfigManager
    {
        // Singleton模式實現，保證全局只有一個SiteConfigManager實例
        private static SiteConfigManager instance = null;
        private static readonly object padlock = new object();

        private SiteConfig siteConfig;

        // 私有構造函數，保證只有SiteConfigManager類型可以創建實例
        private SiteConfigManager()
        {
            var json = File.ReadAllText("siteconfig.json");
            siteConfig = JsonSerializer.Deserialize<SiteConfig>(json);
        }

        // 公共的靜態屬性，用於獲取SiteConfigManager的唯一實例
        public static SiteConfigManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SiteConfigManager();
                    }

                    return instance;
                }
            }
        }

        // SiteConfig屬性，用於獲取SiteConfigManager讀取的JSON配置文件
        public SiteConfig SiteConfig
        {
            get { return siteConfig; }
        }

        // 用於存儲JSON配置文件中的參數的字典
        private static readonly Dictionary<string, string> ParamDict = new();

        // 判斷指定Key的參數是否存在
        public static bool IsParamExist(string key)
        {
            return ParamDict.ContainsKey(key);
        }

        // 獲取指定Key的參數值
        public static string GetParam(string key)
        {
            ParamDict.TryGetValue(key, out string value);
            return value;
        }

        // 添加或新增一個新的參數
        public static void AddOrUpdateParam(string key, string value)
        {
            if (IsParamExist(key))
            {
                ParamDict[key] = value;
            }
            else
            {
                ParamDict.Add(key, value);
            }
        }

        // 解析字串中包含在大括號{}中的參數值
        public static string Parse(string value)
        {
            string pattern = @"\{(.+?)\}";

            MatchCollection matches = Regex.Matches(value, pattern);
            foreach (Match match in matches)
            {
                string matchKey = match.Groups[1].Value;
                if (IsParamExist(matchKey))
                {
                    value = value.Replace(match.Value, GetParam(matchKey));
                }
            }

            return value;
        }

        // 更新Web.config配置文件中指定Key的參數值
        public static void UpdateWebConfigSettings(string paramsKey, string value, string configFile)
        {
            // 使用XmlDocument類型讀取Web.config配置文件
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(configFile);

            // 遍歷appSettings節點下的所有add節點，找到指定Key的節點，並更新Value值

            XmlNodeList nodeList = xmlDoc.SelectNodes("//appSettings/add");
            foreach (XmlNode node in nodeList)
            {
                if (node.Attributes["key"].Value == paramsKey)
                {
                    node.Attributes["value"].Value = value;
                }
            }

            // 將更新後的XmlDocument保存回Web.config配置文件
            xmlDoc.Save(configFile);
        }
    }
}