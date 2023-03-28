namespace DeployToIISWizard.Modal;

public class SiteConfig
{
    public List<Site> sites { get; set; }
}

public class Site
{
    public string name { get; set; }
    public string dbName { get; set; }
    public string domainName { get; set; }
    public List<string> installList { get; set; }
    public List<VirtualDirectory> virtualDirectory { get; set; }
    public Config config { get; set; }
    public List<ExtraResource> extraResources { get; set; }
}

public class ExtraResource
{
    public string from { get; set; }
    public string to { get; set; }
}

public class VirtualDirectory
{
    public string name { get; set; }
    public string path { get; set; }
}

public class Config
{
    public string backendConfigPath { get; set; }
    public string frontEndJSDir { get; set; }
    public string type { get; set; }
    public List<Param> @params { get; set; }
}

public class Param
{
    public string key { get; set; }
    public string value { get; set; }
}